﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Collections;

using OpenGL;
using devDept.Eyeshot;
using devDept.Graphics;
using devDept.Geometry;
using devDept.Eyeshot.Entities;
using System.Windows.Forms;

namespace BuiltInVision
{
    /// <summary>
    /// This is ViewportLayout which will extend behaviour required for a drafting application.
    /// </summary>
    public partial class DraftingViewportLayout : devDept.Eyeshot.ViewportLayout
    {
        private bool firstClick = true;

        // Active layer index
        public int ActiveLayerIndex = 0;

        // Always draw on XY plane, view is alwyas topview
        private Plane plane = Plane.XY;

        // Current selection/position
        private Point3D current;

        // List of selected or picked points with left mouse button 
        private List<Point3D> points = new List<Point3D>();

        public List<Entity> selEntities = new List<Entity>();

        // Current mouse position
        private System.Drawing.Point mouseLocation;

        // Selected entity, store on LMB click
        private int selEntityIndex;
        private Entity selEntity = null;

        // Current drawing plane and extension points required while dimensioning
        private Plane drawingPlane;
        private Point3D extPt1;
        private Point3D extPt2;
        private Point3D planePoint;

        // Current arc radius
        private double radius, radiusY;
        // Current arc span angle
        private double arcSpanAngle;

        // Entities for angularDim with lines
        public Line firstLine = null;
        public Line secondLine = null;
        public Point3D quadrantPoint = null;

        //Threshold to unerstand if polyline or curve has to be closed or not
        private const int magnetRange = 3;

        //Label to show wich operation is currently selected
        private String activeOperationLabel = "";
        
        //label to show how to exit from a command (visibile just in case an operation is currently selected)
        private string rmb = "  RMB to exit.";
        
        public static Color DrawingColor = Color.Black;

        public Plane GetPlane()
        {
            return plane;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.selEntityIndex = GetEntityUnderMouseCursor(e.Location);

            if (waitingForSelection)
            {
                if (this.selEntityIndex != -1)
                {
                    if (selEntity == null || drawingAngularDim)
                    {
                        this.selEntity = this.Entities[selEntityIndex];
                        if (activeOperationLabel != "")
                            this.selEntity.Selected = true;
                    }

                    // drawingAngularDim from lines needs more than one selection
                    if (!drawingAngularDim || this.Entities[selEntityIndex] is Arc) 
                        waitingForSelection = false;
                }

            }
            Cursor.Show();

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (ToolBar.Contains(e.Location))
            {
                base.OnMouseUp(e);

                return;
            }

            #region Handle LMB Clicks 
            if (ActionMode == actionType.None && e.Button == MouseButtons.Left)
            {
                // we need to skip adding points for entity selection click
                editingMode = doingOffset || doingMirror || doingExtend || doingTrim || doingFillet || doingChamfer; 

                ScreenToPlane(e.Location, plane, out current);

                if (objectSnapEnabled && snapPoint != null)
                {
                    if (!(editingMode && firstClick))
                            points.Add(snapPoint);
                }
                else if(IsPolygonClosed())//control needed to close curve and polyline when cursor is near the starting point of polyline or curve
                {
                    //if the distance from current point and first point stored is less than given threshold
                    points.Add((Point3D)points[0].Clone()); //the point to add to points is the first point stored.
                    current = (Point3D)points[0].Clone();
                }
                else if (gridSnapEnabled)
                {
                    if (!(editingMode && firstClick))
                    {
                        SnapToGrid(ref current);
                        points.Add(current);
                    }
                }
                else
                {
                    if(!(editingMode && firstClick))
                        points.Add(current);
                }
                firstClick = false;

                // If drawing points, create and add new point entity on each LMB click
                if (drawingPoints)
                {
                    devDept.Eyeshot.Entities.Point point;

                    if (objectSnapEnabled && snapPoint != null)
                        point = new devDept.Eyeshot.Entities.Point(snap);
                    else
                        point = new devDept.Eyeshot.Entities.Point(current);

                    AddAndRefresh(point, ActiveLayerIndex);
                }
                else if (drawingText)
                {
                    devDept.Eyeshot.Entities.Text text = new Text(current, "Sample Text", 5);
                    AddAndRefresh(text, ActiveLayerIndex);
                }
                else if (drawingLeader)
                {                    
                    if (points.Count == 3)
                    {
                        Leader leader = new Leader(Plane.XY, points);
                        leader.ArrowheadSize = 3;
                        AddAndRefresh(leader, ActiveLayerIndex);
                        devDept.Eyeshot.Entities.Text text = new Text(current, "Sample Text", leader.ArrowheadSize);
                        AddAndRefresh(text, ActiveLayerIndex);

                        drawingLeader = false;
                    }
                }
                // If LINE drawing is finished, create and add line entity to model
                else if (drawingLine && points.Count == 2)
                {
                    Line line = new Line(points[0], points[1]);
                    AddAndRefresh(line, ActiveLayerIndex);
                    drawingLine = false;
                }
                // If CIRCLE drawing is finished, create and add a circle entity to model
                else if (drawingCircle && points.Count == 2)
                {
                    Circle circle = new Circle(drawingPlane, drawingPlane.Origin, radius);
                    AddAndRefresh(circle, ActiveLayerIndex);
                    
                    drawingCircle = false;
                }
                // If ARC drawing is finished, create and add an arc entity to model
                // Input - Center and two end points
                else if (drawingArc && points.Count == 3)
                {
                    Arc arc = new Arc(drawingPlane, drawingPlane.Origin, radius, 0, arcSpanAngle);
                    AddAndRefresh(arc, ActiveLayerIndex);
                    
                    drawingArc = false;
                }
                // If drawing ellipse, create and add ellipse entity to model
                // Inputs - Ellipse center, End of first axis, End of second axis
                else if (drawingEllipse && points.Count == 3)
                {
                    Ellipse ellipse = new Ellipse(drawingPlane, drawingPlane.Origin, radius, radiusY);
                    AddAndRefresh(ellipse, ActiveLayerIndex);

                    drawingEllipse = false;
                }
                // If EllipticalArc drawing is finished, create and add EllipticalArc entity to model
                // Input - Ellipse center, End of first axis, End of second axis, end point
                else if (drawingEllipticalArc && points.Count == 4)
                {
                    EllipticalArc ellipticalArc = new EllipticalArc(drawingPlane, drawingPlane.Origin, radius, radiusY, 0, arcSpanAngle, true);
                    AddAndRefresh(ellipticalArc, ActiveLayerIndex);

                    drawingEllipticalArc = false;
                }
                else if (drawingLinearDim && points.Count == 3)
                {
                    LinearDim linearDim = new LinearDim(drawingPlane, points[0], points[1], current, dimTextHeight);
                    linearDim.ArrowheadSize = 1;                    
                    AddAndRefresh(linearDim, ActiveLayerIndex);

                    drawingLinearDim = false;
                }
                else if (drawingAlignedDim && points.Count == 3)
                {
                    LinearDim alignedDim = new LinearDim(drawingPlane, points[0], points[1], current, dimTextHeight);
                    alignedDim.ArrowheadSize = 1;
                    AddAndRefresh(alignedDim, ActiveLayerIndex);

                    drawingAlignedDim = false;
                }
                else if (drawingOrdinateDim && points.Count == 2)
                {                                                            
                    OrdinateDim ordinateDim = new OrdinateDim(Plane.XY, points[0], points[1], drawingOrdinateDimVertical, dimTextHeight);                    
                    AddAndRefresh(ordinateDim, ActiveLayerIndex);

                    drawingOrdinateDim = false;
                }
                else if ((drawingRadialDim || drawingDiametricDim) && points.Count == 2)
                {

                    if (selEntity is Circle)
                    {
                        Circle circle = selEntity as Circle;

                        if (drawingRadialDim)
                        {
                            RadialDim radialDim = new RadialDim(circle, points[points.Count - 1], dimTextHeight);
                            AddAndRefresh(radialDim, ActiveLayerIndex);
                            drawingRadialDim = false;
                        }
                        else
                        {
                            DiametricDim diametricDim = new DiametricDim(circle, points[points.Count - 1], dimTextHeight);
                            AddAndRefresh(diametricDim, ActiveLayerIndex);
                            drawingDiametricDim = false;
                        }
                    }
                }
                else if (drawingAngularDim)
                {
                    if (!drawingAngularDimFromLines)
                    {
                        if (selEntity is Arc && points.Count == 2 && !drawingQuadrantPoint)
                        {
                            Arc arc = selEntity as Arc;
                            Plane myPlane = (Plane)arc.Plane.Clone();
                            Point3D startPoint = arc.StartPoint;
                            Point3D endPoint = arc.EndPoint;

                            // checks if the Arc is clockwise                            
                            if (Utility.IsOrientedClockwise(arc.Vertices))
                            {
                                myPlane.Flip();
                                startPoint = arc.EndPoint;
                                endPoint = arc.StartPoint;
                            }                                                     

                            AngularDim angularDim = new AngularDim(myPlane, myPlane.Origin, startPoint, endPoint, points[points.Count - 1], dimTextHeight);
                            angularDim.TextSuffix = "°";  
                          
                            AddAndRefresh(angularDim, ActiveLayerIndex);
                            drawingAngularDim = false;
                        }
                    }

                    // If it's not time to set quadrantPoint, adds the lines for angular dim
                    if (selEntity is Line && !drawingQuadrantPoint && quadrantPoint == null)
                    {
                        Line selectedLine = (Line)selEntity;

                        if (firstLine == null)
                            firstLine = selectedLine;
                        else if (secondLine == null && !ReferenceEquals(firstLine, selectedLine))
                        {
                            secondLine = selectedLine;
                            drawingQuadrantPoint = true;
                            // resets points to get only the quadrant point and text position point
                            points.Clear();
                        }

                        drawingAngularDimFromLines = true;
                    }
                    else if(drawingQuadrantPoint)
                    {
                        ScreenToPlane(e.Location, plane, out  quadrantPoint);
                        drawingQuadrantPoint = false;
                    }
                    //if all parameters are present, gets angular dim
                    else if (points.Count == 2 && quadrantPoint != null) 
                    {
                        AngularDim angularDim = new AngularDim(plane, firstLine, secondLine, quadrantPoint, points[points.Count - 1], dimTextHeight);

                        angularDim.TextSuffix = "°";

                        AddAndRefresh(angularDim, ActiveLayerIndex);

                        drawingAngularDim = false;
                        drawingAngularDimFromLines = false;
                    }
                }
                else if (doingOffset && points.Count == 1)
                {
                    CreateOffsetEntity();
                    ClearAllPreviousCommandData();
                }
                else if (doingMirror && points.Count == 2 && selEntity != null)
                {
                    CreateMirrorEntity();
                    ClearAllPreviousCommandData();
                }
                else if (doingExtend && firstSelectedEntity != null && secondSelectedEntity != null)
                {
                    ExtendEntity();
                    ClearAllPreviousCommandData();
                }
                else if (doingTrim && firstSelectedEntity != null && secondSelectedEntity != null)
                {
                    TrimEntity();
                    ClearAllPreviousCommandData();
                }
                else if (doingFillet && firstSelectedEntity != null && secondSelectedEntity != null)
                {
                    CreateFilletEntity();
                    ClearAllPreviousCommandData();
                }
                else if (doingChamfer && firstSelectedEntity != null && secondSelectedEntity != null)
                {
                    CreateChamferEntity();
                    ClearAllPreviousCommandData();
                }
                }
            #endregion

            #region Handle RMB Clicks
            else if (e.Button == MouseButtons.Right)
            {
                ScreenToPlane(e.Location, plane, out current);

                if (drawingPoints)
                {
                    points.Clear();
                    drawingPoints = false;
                }
                else if (drawingText)
                {
                    drawingText = false;
                }
                else if (drawingLeader)
                {
                    drawingLeader = false;
                }
                // If drawing polyline, create and add LinearPath entity to model
                else if (drawingPolyLine)
                {
                    LinearPath lp = new LinearPath(points);
                    AddAndRefresh(lp, ActiveLayerIndex);

                    drawingPolyLine = false;
                }
                // If drawing spline, create and add curve entity to model
                else if (drawingCurve)
                {
#if NURBS
                    Curve curve = Curve.CubicSplineInterpolation(points);
                    AddAndRefresh(curve, ActiveLayerIndex);
#endif
                    drawingCurve = false;
                }
                // doing trasformation apply the objectManiopulator changes
                else if (doingTransform)
                {
                    ObjectManipulator.Apply();
                    ObjectManipulator.Cancel();
                    Entities.Regen();

                    doingTransform = false;
                }
                else
                {
                    ClearAllPreviousCommandData();
                }
            }
            #endregion
            
            base.OnMouseUp(e);
        }

        bool currentlySnapping = false;
        SnapPoint[] snapPoints;

        private bool cursorOutside;

        protected override void OnMouseLeave(EventArgs e)
        {
            cursorOutside = true;
            base.OnMouseLeave(e);

            Invalidate();

        }

        protected override void OnMouseEnter(EventArgs e)
        {
            cursorOutside = false;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            // save the current mouse position
            mouseLocation = e.Location;                 

            // If ObjectSnap is ON, we need to find closest vertex (if any)
            if (objectSnapEnabled)
            {
                this.snapPoint = null;      
                snapPoints = GetSnapPoints(mouseLocation);
            }
            
            // if start is valid and actionMode is None and it's not in the toolbar area
            if (current == null || ActionMode != actionType.None || ToolBar.Contains(e.Location))
            {
                base.OnMouseMove(e);

                return;
            }

            // paint the viewport surface
            PaintBackBuffer();

            // consolidates the drawing
            SwapBuffers();

            if (drawingPoints)
                activeOperationLabel = "Points: ";
            else if (drawingText)
                activeOperationLabel = "Text: ";
            else if (drawingLeader)
                activeOperationLabel = "Leader: ";
            else if (drawingLine)
                activeOperationLabel = "Line: ";
            else if (drawingEllipse)
                activeOperationLabel = "Ellipse: ";
            else if (drawingEllipticalArc)
                activeOperationLabel = "EllipticalArc: ";
            else if (drawingCircle)
                activeOperationLabel = "Circle: ";
            else if (drawingArc)
                activeOperationLabel = "Arc: ";
            else if (drawingPolyLine)
                activeOperationLabel = "Polyline: ";
            else if (drawingCurve)
                activeOperationLabel = "Spline: ";
            else if (doingMirror)
                activeOperationLabel = "Mirror: ";
            else if (doingOffset)
                activeOperationLabel = "Offset: ";
            else if (doingTrim)
                activeOperationLabel = "Trim: ";
            else if (doingExtend)
                activeOperationLabel = "Extend: ";
            else if (doingFillet)
                activeOperationLabel = "Fillet: ";
            else if (doingChamfer)
                activeOperationLabel = "Chamfer: ";
            else if (doingTransform)
                activeOperationLabel = "Transform: ";
            else
                activeOperationLabel = "";

            base.OnMouseMove(e);

        }
        
        SnapPoint snap;      
        
        protected override void DrawOverlay(ViewportLayout.DrawSceneParams myParams)
        {
            ScreenToPlane(mouseLocation, plane, out current);

            currentlySnapping = false;

            // If ObjectSnap is ON, we need to find closest vertex (if any)
            if (objectSnapEnabled && snapPoints != null && snapPoints.Length > 0)
            {
                snap = FindClosestPoint(snapPoints);
                current = snap;
                currentlySnapping = true;
            }

            // set GL for interactive draw or elastic line 
            renderContext.SetLineSize(1);

            renderContext.EnableXOR(true);

            renderContext.SetState(depthStencilStateType.DepthTestOff);           

            if (!(currentlySnapping) && !(waitingForSelection) && ActionMode ==actionType.None &&
                !(doingExtend || doingTrim || doingFillet || doingChamfer || drawingOrdinateDim) && !ObjectManipulator.Visible)
            {
                if (!cursorOutside)
                    DrawPositionMark(current);
            } 
                      
            if (drawingLine || drawingPolyLine)
            {
                DrawInteractiveLines();
            }
            else if(drawingCircle && points.Count > 0)
            {
                if (ActionMode == actionType.None && !ToolBar.Contains(mouseLocation))
                {
                    DrawInteractiveCircle();
                } 
            }
            else if (drawingArc && points.Count > 0)
            {
                if (ActionMode == actionType.None && !ToolBar.Contains(mouseLocation))
                {
                    DrawInteractiveArc();
                }
            }
            else if (drawingEllipse && points.Count > 0)
            {
                DrawInteractiveEllipse();
            }
            else if (drawingEllipticalArc && points.Count > 0)
            {
                DrawInteractiveEllipticalArc();
            }
            else if (drawingCurve)
            {               
                DrawInteractiveCurve();
            }
            else if (drawingLeader)
            {
                DrawInteractiveLeader();
            }
            else if (drawingLinearDim || drawingAlignedDim)
            {
                if (points.Count < 2)
                {
                    if (!cursorOutside)
                    {
                        DrawSelectionMark(mouseLocation);

                        renderContext.EnableXOR(false);
                        string text = "Select the first point";
                        if (!firstClick)
                            text = "Select the second point";

                        DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                            text, new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                        renderContext.EnableXOR(true);
                    }
                }
                else
                {
                    if (drawingLinearDim)
                        DrawInteractiveLinearDim();
                    else if (drawingAlignedDim)
                        DrawInteractiveAlignedDim();
                }
            }
            else if (drawingOrdinateDim)
            {                                
                if (!cursorOutside)
                {
                    if (points.Count == 1)
                    {
                        DrawPositionMark(current, 5);
                        DrawInteractiveOrdinateDim();
            }
            else
            {
                        DrawPositionMark(current);
                        renderContext.EnableXOR(false);
                        string text = "Select the definition point";
                        if (!firstClick)
                            text = "Select the leader end point";

                        DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                            text, new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                        renderContext.EnableXOR(true);
                    }
                }                                
            }
            else if (drawingRadialDim || drawingDiametricDim)
            {
                if (waitingForSelection)
                {
                    DrawSelectionMark(mouseLocation);

                    renderContext.EnableXOR(false);

                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                        "Select Arc or Circle", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                    renderContext.EnableXOR(true);

                }
                DrawInteractiveDiametricDim();
            }
            else if (drawingAngularDim)
            {
                if (waitingForSelection)
                {
                    if (!drawingAngularDimFromLines)
                    {
                        DrawSelectionMark(mouseLocation);

                        renderContext.EnableXOR(false);

                        DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                            "Select Arc or Line", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                        renderContext.EnableXOR(true);
                    }
                    else if (quadrantPoint == null && !drawingQuadrantPoint)
                    {
                        DrawSelectionMark(mouseLocation);

                        renderContext.EnableXOR(false);

                        DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                            "Select second Line", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                        renderContext.EnableXOR(true);
                    }
                    else if (drawingQuadrantPoint)
                    {
                        DrawSelectionMark(mouseLocation);

                        renderContext.EnableXOR(false);

                        DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                            "Select a quadrant", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                        renderContext.EnableXOR(true);
                    }
                    else if (quadrantPoint != null)
                    {
                        DrawSelectionMark(mouseLocation);

                        renderContext.EnableXOR(false);

                        DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                            "Select text position", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                        renderContext.EnableXOR(true);
                    }
                }
                DrawInteractiveAngularDim();
            }
            else if (doingMirror)
            {
                if (waitingForSelection)
                {
                    DrawSelectionMark(mouseLocation);

                    renderContext.EnableXOR(false);

                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                        "Select entity to mirror", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                    renderContext.EnableXOR(true);
                }

                CreateMirrorEntity();
            }
            else if (doingOffset)
            {
                if (waitingForSelection)
                {
                    DrawSelectionMark(mouseLocation);

                    renderContext.EnableXOR(false);

                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                        "Select entity to offset", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                    renderContext.EnableXOR(true);                    
                }
                CreateOffsetEntity();
            }
            else if (doingTransform)
            {
                TransformEntity();
            }
            else if (doingFillet)
            {
                if (waitingForSelection)
                {
                    DrawSelectionMark(mouseLocation);

                    renderContext.EnableXOR(false);

                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                        "Select first curve", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                    renderContext.EnableXOR(true);
                }
                CreateFilletEntity();
            }
            else if (doingChamfer)
            {
                if (waitingForSelection)
                {
                    DrawSelectionMark(mouseLocation);
                    renderContext.EnableXOR(false);

                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                        "Select first curve", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                    renderContext.EnableXOR(true);
                }
                CreateChamferEntity();
            }
            else if (doingExtend)
            {
                if (waitingForSelection)
                {
                    DrawSelectionMark(mouseLocation);
                    renderContext.EnableXOR(false);

                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                        "Select boundary entity", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);

                    renderContext.EnableXOR(true);
                }
                ExtendEntity();
            }
            else if (doingTrim)
            {
                if (waitingForSelection)
                {
                    DrawSelectionMark(mouseLocation);
                    renderContext.EnableXOR(false);

                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                        "Select trimming entity", new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
                    renderContext.EnableXOR(true);
                }
                TrimEntity();
            }

            // disables draw inverted
            renderContext.EnableXOR(false);


            // text drawing
            if (!(drawingDiametricDim || drawingAlignedDim || drawingLinearDim || drawingOrdinateDim || drawingLeader || drawingRadialDim || drawingAngularDim || 
                doingMirror || doingOffset || doingExtend || doingTrim || doingFillet || doingChamfer || 
                doingTransform) && ActionMode == actionType.None)
            {
                if (!(drawingEllipticalArc && points.Count >= 3) && !cursorOutside)
                {
                    //label on mouse
                    string exitCommand = "";
                    if(drawingCurve || drawingPolyLine || drawingPoints)
                        exitCommand = rmb;
                    else
                        exitCommand = "";

                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10,
                        activeOperationLabel +
                        "X = " + current.X.ToString("f2") + ", " +
                        "Y = " + current.Y.ToString("f2") +
                        exitCommand,
                        new Font("Tahoma", 8.25f),
                        DrawingColor, ContentAlignment.BottomLeft);
                }
            }
            
            base.DrawOverlay(myParams);
        }

        /// <summary>
        /// This function gets all the curve entities selected on the screen and create a composite curve as single entity
        /// </summary>
        public void CreateCompositeCurve()
        {
            //list of selected curve
            List<ICurve> selectedCurveList = new List<ICurve>();

            //for goes backward: in this way we can remove enties at the same time we found it selected
            for (int i = this.Entities.Count - 1; i > -1; i--)
            {
                Entity ent = this.Entities[i];

                if (ent.Selected && ent is ICurve && ent is CompositeCurve == false)
                {
                    selectedCurveList.Add((ICurve)ent);
                    //remove the entity we use to create composite curve, in this way we can display only composite curve and not single curves
                    this.Entities.RemoveAt(i);
                }
            }

            if (selectedCurveList.Count > 0)
            {
                CompositeCurve compositeCurve = new CompositeCurve(selectedCurveList);

                AddAndRefresh(compositeCurve, ActiveLayerIndex);
            }
        }

        /// <summary>
        /// Clears all previous selections, snapping information etc.
        /// </summary>
        internal void ClearAllPreviousCommandData()
        {
            points.Clear();
            selEntity = null;
            selEntityIndex = -1;
            snapPoint = null;
            drawingArc = false;
            drawingCircle = false;
            drawingCurve = false;
            drawingEllipse = false;
            drawingEllipticalArc = false;
            drawingLine = false;
            drawingLinearDim = false;
            drawingOrdinateDim = false;
            drawingPoints = false;
            drawingText = false;
            drawingLeader = false;
            drawingPolyLine = false;
            drawingRadialDim = false;
            drawingAlignedDim = false;
            drawingQuadrantPoint = false;
            drawingAngularDim = false;
            drawingAngularDimFromLines = false;

            firstClick = true;
            doingMirror = false;
            doingOffset = false;
            doingTrim = false;
            doingExtend = false;
            doingChamfer = false;
            doingFillet = false;
            doingTransform = false;
            firstSelectedEntity = null;
            secondSelectedEntity = null;

            firstLine = null;
            secondLine = null;
            quadrantPoint = null;

            activeOperationLabel = "";
            ActionMode = actionType.None;
            Entities.ClearSelection();
            ObjectManipulator.Cancel();
        }
    }

}