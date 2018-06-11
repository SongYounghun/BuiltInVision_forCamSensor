using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using devDept.Geometry;
using devDept.Eyeshot.Entities;
using OpenGL;
using devDept.Eyeshot;
using devDept.Graphics;

namespace BuiltInVision
{
    /// <summary>
    /// Methods required to edit different entities interactively like offset, mirror, extend, trim, rotate etc.
    /// </summary>
    partial class DraftingViewportLayout
    {
        /// <summary>
        /// Tries to extend entity upto the selected boundary entity. For a short boundary line, it tries to extend selected
        /// entity upto elongated line.
        /// </summary>
        private void ExtendEntity()
        {
            if (firstSelectedEntity == null)
            {
                if (selEntityIndex != -1)
                {
                    firstSelectedEntity = Entities[selEntityIndex];
                    selEntityIndex = -1;
                    return;
                }
            }
            else if (secondSelectedEntity == null)
            {
                DrawSelectionMark(mouseLocation);
                
                renderContext.EnableXOR(false);

                DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "Select entity to extend",
                        new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
            }

            if (secondSelectedEntity == null)
            {
                if (selEntityIndex != -1)
                {
                    secondSelectedEntity = Entities[selEntityIndex];
                }
            }

            if (firstSelectedEntity != null && secondSelectedEntity != null)
            {
                if (firstSelectedEntity is ICurve && secondSelectedEntity is ICurve)
                {
                    ICurve boundary = firstSelectedEntity as ICurve;
                    ICurve curve = secondSelectedEntity as ICurve;

                    // Check which end of curve is near to boundary
                    double t1, t2;
                    boundary.ClosestPointTo(curve.StartPoint, out t1);
                    boundary.ClosestPointTo(curve.EndPoint, out t2);

                    Point3D projStartPt = boundary.PointAt(t1);
                    Point3D projEndPt = boundary.PointAt(t2);

                    double curveStartDistance = curve.StartPoint.DistanceTo(projStartPt);
                    double curveEndDistance = curve.EndPoint.DistanceTo(projEndPt);

                    bool success = false;
                    if (curveStartDistance < curveEndDistance)
                    {
                        if (curve is Line)
                        {
                            success = ExtendLine(curve, boundary, true);
                        }
                        else if (curve is LinearPath)
                        {
                            success = ExtendPolyLine(curve, boundary, true);
                        }
                        else if (curve is Arc)
                        {
                            success = ExtendCircularArc(curve, boundary, true);
                        }
                        else if (curve is EllipticalArc)
                        {
                            success = ExtendEllipticalArc(curve, boundary, true);
                        }
#if NURBS
                        else if (curve is Curve)
                        {
                            success = ExtendSpline(curve, boundary, true);
                        }
#endif
                    }
                    else
                    {
                        if (curve is Line)
                        {
                            success = ExtendLine(curve, boundary, false);
                        }
                        else if (curve is LinearPath)
                        {
                            success = ExtendPolyLine(curve, boundary, false);
                        }
                        else if (curve is Arc)
                        {
                            success = ExtendCircularArc(curve, boundary, false);
                        }
                        else if (curve is EllipticalArc)
                        {
                            success = ExtendEllipticalArc(curve, boundary, false);
                        }
#if NURBS
                        else if (curve is Curve)
                        {
                            success = ExtendSpline(curve, boundary, false);
                        }
#endif
                    }
                    if (success)
                    {
                        Entities.Remove(secondSelectedEntity);
                        Entities.Regen();
                    }
                }
                ClearAllPreviousCommandData();
            }
        }

        /// <summary>
        /// Creates an elongated boundary when it is line.
        /// </summary>
        private ICurve GetExtendedBoundary(ICurve boundary)
        {
            if (boundary is Line)
            {
                Line tempLine = new Line(boundary.StartPoint, boundary.EndPoint);
                Vector3D dir1 = new Vector3D(tempLine.StartPoint, tempLine.EndPoint);
                dir1.Normalize();
                tempLine.EndPoint = tempLine.EndPoint + dir1 * extensionLength;

                Vector3D dir2 = new Vector3D(tempLine.EndPoint, tempLine.StartPoint);
                dir2.Normalize();
                tempLine.StartPoint = tempLine.StartPoint + dir2 * extensionLength;

                boundary = tempLine;
            }
            return boundary;
        }

        /// <summary>
        /// Returns closes point from given input point for provided point list.
        /// </summary>
        private Point3D GetClosestPoint(Point3D point3D, Point3D[] intersetionPoints)
        {
            double minsquaredDist = Double.MaxValue;
            Point3D result = null;

            foreach (Point3D pt in intersetionPoints)
            {
                double distSquared = Point3D.DistanceSquared(point3D, pt);
                if (distSquared < minsquaredDist && !point3D.Equals(pt))
                {
                    minsquaredDist = distSquared;
                    result = pt;
                }
            }
            return result;
        }

        #region Extend Methods
        // Extends input line upto the provided boundary.
        private bool ExtendLine(ICurve lineCurve, ICurve boundary, bool nearStart)
        {
            Line line = lineCurve as Line;

            // Create temp line which will intersect boundary curve depending on which end to extend
            Line tempLine = null;
            Vector3D direction = null;
            if (nearStart)
            {
                tempLine = new Line(line.StartPoint, line.StartPoint);
                direction = new Vector3D(line.EndPoint, line.StartPoint);
            }
            else
            {
                tempLine = new Line(line.EndPoint, line.EndPoint);
                direction = new Vector3D(line.StartPoint, line.EndPoint);
            }
            direction.Normalize();
            tempLine.EndPoint = tempLine.EndPoint + direction * extensionLength;
#if NURBS
            // Get intersection points for input line and boundary
            // If not intersecting and boundary is line, we can try with extended boundary
            Point3D[] intersetionPoints = Curve.Intersection(boundary, tempLine);
            if (intersetionPoints.Length == 0)
                intersetionPoints = Curve.Intersection(GetExtendedBoundary(boundary), tempLine);

            // Modify line start/end point as closest intersection point
            if (intersetionPoints.Length > 0)
            {
                if (nearStart)
                    line.StartPoint = GetClosestPoint(line.StartPoint, intersetionPoints);
                else
                    line.EndPoint = GetClosestPoint(line.EndPoint, intersetionPoints);
                AddAndRefresh((Entity)line.Clone(), ((Entity)lineCurve).LayerIndex);
                return true;
            }
#endif
            return false;
        }

        // Method for polyline extension
        private bool ExtendPolyLine(ICurve lineCurve, ICurve boundary, bool nearStart)
        {
            LinearPath line = secondSelectedEntity as LinearPath;
            Point3D[] tempVertices = line.Vertices;

            // create temp line with proper direction
            Line tempLine = new Line(line.StartPoint, line.StartPoint);
            Vector3D direction = new Vector3D(line.Vertices[1], line.StartPoint);

            if (!nearStart)
            {
                tempLine = new Line(line.EndPoint, line.EndPoint);
                direction = new Vector3D(line.Vertices[line.Vertices.Length - 2], line.EndPoint);
            }

            direction.Normalize();
            tempLine.EndPoint = tempLine.EndPoint + direction * extensionLength;
#if NURBS
            Point3D[] intersetionPoints = Curve.Intersection(boundary, tempLine);
            if (intersetionPoints.Length == 0)
                intersetionPoints = Curve.Intersection(GetExtendedBoundary(boundary), tempLine);

            if (intersetionPoints.Length > 0)
            {
                if (nearStart)
                    tempVertices[0] = GetClosestPoint(line.StartPoint, intersetionPoints);
                else
                    tempVertices[tempVertices.Length - 1] = GetClosestPoint(line.EndPoint, intersetionPoints);

                line.Vertices = tempVertices;
                AddAndRefresh((Entity)line.Clone(), ((Entity)lineCurve).LayerIndex);
                return true;
            }
#endif
            return false;
        }

        // Method for arc extension
        private bool ExtendCircularArc(ICurve arcCurve, ICurve boundary, bool nearStart)
        {
            Arc selCircularArc = arcCurve as Arc;
            Circle tempCircle = new Circle(selCircularArc.Plane, selCircularArc.Center, selCircularArc.Radius);
#if NURBS
            Point3D[] intersetionPoints = Curve.Intersection(boundary, tempCircle);
            if (intersetionPoints.Length == 0)
                intersetionPoints = Curve.Intersection(GetExtendedBoundary(boundary), tempCircle);

            if (intersetionPoints.Length > 0)
            {
                if (nearStart)
                {
                    Point3D intPoint = GetClosestPoint(selCircularArc.StartPoint, intersetionPoints);
                    Vector3D xAxis = new Vector3D(selCircularArc.Center, selCircularArc.EndPoint);
                    xAxis.Normalize();
                    Vector3D yAxis = Vector3D.Cross(Vector3D.AxisZ, xAxis);
                    yAxis.Normalize();
                    Plane arcPlane = new Plane(selCircularArc.Center, xAxis, yAxis);

                    Vector2D v1 = new Vector2D(selCircularArc.Center, selCircularArc.EndPoint);
                    v1.Normalize();
                    Vector2D v2 = new Vector2D(selCircularArc.Center, intPoint);
                    v2.Normalize();

                    double arcSpan = Vector2D.SignedAngleBetween(v1, v2);
                    Arc newArc = new Arc(arcPlane, arcPlane.Origin, selCircularArc.Radius, 0, arcSpan);
                    AddAndRefresh(newArc, ((Entity)arcCurve).LayerIndex);
                }
                else
                {
                    Point3D intPoint = GetClosestPoint(selCircularArc.EndPoint, intersetionPoints);

                    //plane
                    Vector3D xAxis = new Vector3D(selCircularArc.Center, selCircularArc.StartPoint);
                    xAxis.Normalize();
                    Vector3D yAxis = Vector3D.Cross(Vector3D.AxisZ, xAxis);
                    yAxis.Normalize();
                    Plane arcPlane = new Plane(selCircularArc.Center, xAxis, yAxis);

                    Vector2D v1 = new Vector2D(selCircularArc.Center, selCircularArc.StartPoint);
                    v1.Normalize();
                    Vector2D v2 = new Vector2D(selCircularArc.Center, intPoint);
                    v2.Normalize();

                    double arcSpan = Vector2D.SignedAngleBetween(v1, v2);
                    Arc newArc = new Arc(arcPlane, arcPlane.Origin, selCircularArc.Radius, 0, arcSpan);
                    AddAndRefresh(newArc, ((Entity)arcCurve).LayerIndex);
                }
                return true;
            }
#endif
            return false;
        }

        // Method for elliptical arc extension
        private bool ExtendEllipticalArc(ICurve ellipticalArcCurve, ICurve boundary, bool start)
        {
            EllipticalArc selEllipseArc = ellipticalArcCurve as EllipticalArc;
            Ellipse tempEllipse = new Ellipse(selEllipseArc.Plane, selEllipseArc.Center, selEllipseArc.RadiusX, selEllipseArc.RadiusY);
#if NURBS
            Point3D[] intersetionPoints = Curve.Intersection(boundary, tempEllipse);
            if (intersetionPoints.Length == 0)
                intersetionPoints = Curve.Intersection(GetExtendedBoundary(boundary), tempEllipse);

            EllipticalArc newArc = null;

            if (intersetionPoints.Length > 0)
            {
                Plane arcPlane = selEllipseArc.Plane;
                if (start)
                {
                    Point3D intPoint = GetClosestPoint(selEllipseArc.StartPoint, intersetionPoints);

                    newArc = new EllipticalArc(arcPlane, selEllipseArc.Center, selEllipseArc.RadiusX,
                                                    selEllipseArc.RadiusY, selEllipseArc.EndPoint, intPoint, false);
                    // If start point is not on the new arc, flip needed
                    double t;
                    newArc.ClosestPointTo(selEllipseArc.StartPoint, out t);
                    Point3D projPt = newArc.PointAt(t);
                    if (projPt.DistanceTo(selEllipseArc.StartPoint) > 0.1)
                    {
                        newArc = new EllipticalArc(arcPlane, selEllipseArc.Center, selEllipseArc.RadiusX,
                                                selEllipseArc.RadiusY, selEllipseArc.EndPoint, intPoint, true);
                    }
                    AddAndRefresh(newArc, ((Entity)ellipticalArcCurve).LayerIndex);
                }
                else
                {
                    Point3D intPoint = GetClosestPoint(selEllipseArc.EndPoint, intersetionPoints);
                    newArc = new EllipticalArc(arcPlane, selEllipseArc.Center, selEllipseArc.RadiusX,
                                                selEllipseArc.RadiusY, selEllipseArc.StartPoint, intPoint, false);

                    // If end point is not on the new arc, flip needed
                    double t;
                    newArc.ClosestPointTo(selEllipseArc.EndPoint, out t);
                    Point3D projPt = newArc.PointAt(t);
                    if (projPt.DistanceTo(selEllipseArc.EndPoint) > 0.1)
                    {
                        newArc = new EllipticalArc(arcPlane, selEllipseArc.Center, selEllipseArc.RadiusX,
                                               selEllipseArc.RadiusY, selEllipseArc.StartPoint, intPoint, true);
                    }
                }
                if (newArc != null)
                {
                    AddAndRefresh(newArc, ((Entity)ellipticalArcCurve).LayerIndex);
                    return true;
                }
            }
#endif
            return false;
        }

        // Method for spline extension
        private bool ExtendSpline(ICurve curve, ICurve boundary, bool nearStart)
        {
#if NURBS
            Curve originalSpline = curve as Curve;

            Line tempLine = null;
            Vector3D direction = null;
            if (nearStart)
            {
                tempLine = new Line(curve.StartPoint, curve.StartPoint);
                direction = curve.StartTangent; direction.Normalize(); direction.Negate();
                tempLine.EndPoint = tempLine.EndPoint + direction * extensionLength;
            }
            else
            {
                tempLine = new Line(curve.EndPoint, curve.EndPoint);
                direction = curve.EndTangent; direction.Normalize();
                tempLine.EndPoint = tempLine.EndPoint + direction * extensionLength;
            }

            Point3D[] intersetionPoints = Curve.Intersection(boundary, tempLine);
            if (intersetionPoints.Length == 0)
                intersetionPoints = Curve.Intersection(GetExtendedBoundary(boundary), tempLine);

            if (intersetionPoints.Length > 0)
            {
                List<Point4D> ctrlPoints = originalSpline.ControlPoints.ToList();
                List<Point3D> newCtrlPoints = new List<Point3D>();
                if (nearStart)
                {
                    newCtrlPoints.Add(GetClosestPoint(curve.StartPoint, intersetionPoints));
                    foreach (Point4D ctrlPt in ctrlPoints)
                    {
                        Point3D point = new Point3D(ctrlPt.X, ctrlPt.Y, ctrlPt.Z);
                        if (!point.Equals(originalSpline.StartPoint))
                            newCtrlPoints.Add(point);
                    }
                }
                else
                {
                    foreach (Point4D ctrlPt in ctrlPoints)
                    {
                        Point3D point = new Point3D(ctrlPt.X, ctrlPt.Y, ctrlPt.Z);
                        if (!point.Equals(originalSpline.EndPoint))
                            newCtrlPoints.Add(point);
                    }
                    newCtrlPoints.Add(GetClosestPoint(curve.EndPoint, intersetionPoints));
                }

                Curve newCurve = new Curve(originalSpline.Degree, newCtrlPoints);
                if (newCurve != null)
                {
                    AddAndRefresh(newCurve, ((Entity)curve).LayerIndex);
                    return true;
                }
            }
#endif
            return false;
        }
        #endregion

        /// <summary>
        /// Trims selected entity by the cutting entity. Removes portion of the curve near mouse click.
        /// </summary>
        private void TrimEntity()
        {
            if (firstSelectedEntity == null)
            {
                if (selEntityIndex != -1)
                {
                    firstSelectedEntity = Entities[selEntityIndex];
                    selEntityIndex = -1;
                    return;
                }
            }
            else if (secondSelectedEntity == null)
            {
                DrawSelectionMark(mouseLocation);
                renderContext.EnableXOR(false);
                DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "Select entity to be trimmed",
                        new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
            }

            if (secondSelectedEntity == null)
            {
                if (selEntityIndex != -1)
                {
                    secondSelectedEntity = Entities[selEntityIndex];
                }
            }
            if (firstSelectedEntity != null && secondSelectedEntity != null)
            {
                if (firstSelectedEntity is ICurve && secondSelectedEntity is ICurve)
                {
                    ICurve trimmingCurve = firstSelectedEntity as ICurve;
                    ICurve curve = secondSelectedEntity as ICurve;
#if NURBS                    
                    Point3D[] intersetionPoints = Curve.Intersection(trimmingCurve, curve);
                    if (intersetionPoints.Length > 0 && points.Count > 0)
                    {
                        List<double> parameters = new List<double>();
                        for (int i = 0; i < intersetionPoints.Length; i++)
                        {
                            var intersetionPoint = intersetionPoints[i];
                            double t = ((InterPoint)intersetionPoint).s;
                            parameters.Add(t);
                        }

                        double distSelected = 1;

                        ICurve[] trimmedCurves = null;
                        if (parameters != null)
                        {
                            parameters.Sort();
                            double u;
                            curve.ClosestPointTo(points[0], out u);
                            distSelected = Point3D.Distance(points[0], curve.PointAt(u));
                            distSelected += distSelected / 1e3;

                            if (u <= parameters[0])
                            {
                                curve.SplitBy(new Point3D[] { curve.PointAt(parameters[0]) }, out trimmedCurves);
                            }
                            else if (u > parameters[parameters.Count - 1])
                            {
                                curve.SplitBy(new Point3D[] { curve.PointAt(parameters[parameters.Count - 1]) },
                                              out trimmedCurves);
                            }
                            else
                            {
                                for (int i = 0; i < parameters.Count - 1; i++)
                                {
                                    if (u > parameters[i] && u <= parameters[i + 1])
                                    {
                                        curve.SplitBy(
                                            new Point3D[] { curve.PointAt(parameters[i]), curve.PointAt(parameters[i + 1]) },
                                            out trimmedCurves);
                                    }
                                }
                            }
                        }

                        bool success = false;
                        //Decide which portion of curve to be deleted
                        for (int i = 0; i < trimmedCurves.Length; i++)
                        {
                            ICurve trimmedCurve = trimmedCurves[i];
                            double t;

                            trimmedCurve.ClosestPointTo(points[0], out t);
                            {

                                if ((t < trimmedCurve.Domain.t0 || t > trimmedCurve.Domain.t1)
                                    || Point3D.Distance(points[0], trimmedCurve.PointAt(t)) > distSelected)
                                {
                                    AddAndRefresh((Entity)trimmedCurve, secondSelectedEntity.LayerIndex);
                                    success = true;
                                }
                            }
                        }

                        // Delete original entity to be trimmed
                        if (success)
                            Entities.Remove(secondSelectedEntity);
                    }
                    ClearAllPreviousCommandData();
#endif
                }
            }
        }

        /// <summary>
        /// Tries to fit chamfer line between selected curves. Chamfer distance is provided through user input box.
        /// </summary>
        private void CreateChamferEntity()
        {
            if (firstSelectedEntity == null)
            {
                if (selEntityIndex != -1)
                {
                    firstSelectedEntity = Entities[selEntityIndex];
                    selEntityIndex = -1;
                    return;
                }
            }
            else if (secondSelectedEntity == null)
            {
                DrawSelectionMark(mouseLocation);
                renderContext.EnableXOR(false);
                DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "Select second curve",
                    new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
            }

            if (secondSelectedEntity == null)
            {
                if (selEntityIndex != -1)
                {
                    secondSelectedEntity = Entities[selEntityIndex];
                }
            }

            if (firstSelectedEntity is ICurve && secondSelectedEntity is ICurve)
            {
                Line chamferLine = null;
                double distance = this.filletRadius;
#if NURBS
                if (Curve.Chamfer((ICurve)firstSelectedEntity, (ICurve)secondSelectedEntity, distance, false, false, true, true, out chamferLine))
                    AddAndRefresh(chamferLine, ActiveLayerIndex);
                else if (Curve.Chamfer((ICurve)firstSelectedEntity, (ICurve)secondSelectedEntity, distance, false, true, true, true, out chamferLine))
                    AddAndRefresh(chamferLine, ActiveLayerIndex);
                else if (Curve.Chamfer((ICurve)firstSelectedEntity, (ICurve)secondSelectedEntity, distance, true, false, true, true, out chamferLine))
                    AddAndRefresh(chamferLine, ActiveLayerIndex);
                else if (Curve.Chamfer((ICurve)firstSelectedEntity, (ICurve)secondSelectedEntity, distance, true, true, true, true, out chamferLine))
                    AddAndRefresh(chamferLine, ActiveLayerIndex);

                ClearAllPreviousCommandData();
#endif
            }
        }

        /// <summary>
        /// Tries to fit fillet arc between two selected curves. Fillet radius is given from user input box.
        /// </summary>
        private void CreateFilletEntity()
        {
            if (firstSelectedEntity == null)
            {
                if (selEntityIndex != -1)
                {
                    firstSelectedEntity = Entities[selEntityIndex];
                    selEntityIndex = -1;
                    return;
                }
            }
            else if (secondSelectedEntity == null)
            {
                DrawSelectionMark(mouseLocation);
                renderContext.EnableXOR(false);
                DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "Select second curve",
                    new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
            }

            if (secondSelectedEntity == null)
            {
                if (selEntityIndex != -1)
                {
                    secondSelectedEntity = Entities[selEntityIndex];
                }
            }

            if (firstSelectedEntity is ICurve && secondSelectedEntity is ICurve)
            {
                if (firstSelectedEntity is Line && secondSelectedEntity is Line)
                {
                    Line l1 = firstSelectedEntity as Line;
                    Line l2 = secondSelectedEntity as Line;

                    if (Vector3D.AreParallel(l1.Direction, l2.Direction))
                    {
                        ClearAllPreviousCommandData();
                        return;
                    }
                }
#if NURBS
                Arc filletArc = null;
                try
                {
                    if (Curve.Fillet((ICurve)firstSelectedEntity, (ICurve)secondSelectedEntity, filletRadius, false, false, true, true, out filletArc))
                        AddAndRefresh(filletArc, ActiveLayerIndex);
                    else if (Curve.Fillet((ICurve)firstSelectedEntity, (ICurve)secondSelectedEntity, filletRadius, false, true, true, true, out filletArc))
                        AddAndRefresh(filletArc, ActiveLayerIndex);
                    else if (Curve.Fillet((ICurve)firstSelectedEntity, (ICurve)secondSelectedEntity, filletRadius, true, false, true, true, out filletArc))
                        AddAndRefresh(filletArc, ActiveLayerIndex);
                    else if (Curve.Fillet((ICurve)firstSelectedEntity, (ICurve)secondSelectedEntity, filletRadius, true, true, true, true, out filletArc))
                        AddAndRefresh(filletArc, ActiveLayerIndex);
                }
                catch (Exception ex)
                { 
                }
#endif
                ClearAllPreviousCommandData();
            }
        }

        /// <summary>
        /// Creates mirror image of the selected entity for given mirror axis. Mirror axis is formed by selection two points.
        /// </summary>
        private void CreateMirrorEntity()
        {
            // We need to have two reference points selected, might be snapped vertices
            if (points.Count < 2)
            {
                //If entity is selected, ask user to select mirror line
                renderContext.EnableXOR(false);
                if (points.Count == 0 && !waitingForSelection)
                {
                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "Start of mirror plane",
                        new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
                }
                else if (points.Count == 1)
                {
                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "End of mirror plane",
                        new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
                }
                DrawInteractiveLines();
            }
            else
            {
                if (points[1].X < points[0].X || points[1].Y < points[0].Y)
                {
                    Point3D p0 = points[0];
                    Point3D p1 = points[1];

                    Utility.Swap(ref p0, ref p1);

                    points[0] = p0;
                    points[1] = p1;
                }

                Vector3D axisX = new Vector3D(points[0], points[1]);
                Plane mirrorPlane = new Plane(points[0], axisX, Vector3D.AxisZ);

                Entity mirrorEntity = (Entity)selEntity.Clone();
                Mirror mirror = new Mirror(mirrorPlane);
                mirrorEntity.TransformBy(mirror);
                AddAndRefresh(mirrorEntity, ActiveLayerIndex);

                ClearAllPreviousCommandData();
            }
        }

        /// <summary>
        /// Tries to create offset entity for selected entity at the selected location (offset distance) and side.
        /// </summary>
        private void CreateOffsetEntity()
        {
            if (selEntity != null && selEntity is ICurve)
            {
#if !NURBS
                if (selEntity is Ellipse || selEntity is EllipticalArc)
                    return;
#endif
                if (points.Count == 0)
                {
                    renderContext.EnableXOR(false);
                    DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "Side to offset",
                            new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
                    return;
                }

                ICurve selCurve = selEntity as ICurve;
                double t;
                bool success = selCurve.Project(points[0], out t);
                Point3D projectedPt = selCurve.PointAt(t);
                double offsetDist = projectedPt.DistanceTo(points[0]);

                ICurve offsetCurve = selCurve.Offset(offsetDist, Vector3D.AxisZ, 0.01, true);
                success = offsetCurve.Project(points[0], out t);
                projectedPt = offsetCurve.PointAt(t);
                if (projectedPt.DistanceTo(points[0]) > 1e-3)
                    offsetCurve = selCurve.Offset(-offsetDist, Vector3D.AxisZ, 0.01, true);

                AddAndRefresh((Entity)offsetCurve, ActiveLayerIndex);
            }
        }

        /// <summary>
        /// Transforms selected entity using ObjectManipulator. User needs to select center point of transformation.
        /// </summary>
        private void TransformEntity()
        {
            if (points.Count == 0)
            {
                renderContext.EnableXOR(false);
                DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "Select center of transformation",
                        new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
            }
            else if (points.Count == 1 && !ObjectManipulator.Visible)
            {
                Transformation center = new Translation(points[0].X, points[0].Y, points[0].Z);

                ObjectManipulator.Enable(center, false);
                ObjectManipulator.ShowOriginalWhileEditing = false;
            }
            else if (ObjectManipulator.Visible)
            {
                renderContext.EnableXOR(false);
                DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "Press the right button to apply the changes",
                        new Font("Tahoma", 8.25f), DrawingColor, ContentAlignment.BottomLeft);
            }
        }

        Entity secondSelectedEntity = null;
        Entity firstSelectedEntity = null;

        public double filletRadius = 10.0;
        public double rotationAngle = 45.0;
        public double scaleFactor = 1.5;
        private double extensionLength = 500;

        #region Flags indicating current editing mode

        public bool doingMirror;
        public bool doingOffset;
        public bool doingFillet;
        public bool doingChamfer;
        public bool doingTrim;
        public bool doingExtend;
        public bool doingTransform;
        public bool editingMode;

        #endregion
    }

}