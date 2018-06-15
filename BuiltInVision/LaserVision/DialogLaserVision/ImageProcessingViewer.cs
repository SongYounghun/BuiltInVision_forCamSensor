using devDept.Eyeshot.Entities;
using devDept.Geometry;
using GalvoScanner.LaserVision.OpenCV;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalvoScanner.LaserVision.DialogLaserVision
{
    public partial class ImageProcessingViewer : Form
    {
        public enum viewMode { Orignal = 0, Processing, Result, Live };

        OpenCVData m_cvData = null;
        LaserVisionControl m_laserVisionControl = null;
        viewMode m_viewMode = viewMode.Orignal;
        public viewMode ViewerMode
        {
            get { return m_viewMode; }
        }

        private Plane xyPlane;
        private Point3D moveFrom;
        private Point3D moveTo;

        private Point3D m_targetPosition = null;
        private System.Drawing.Point m_cvTargetPostion = new System.Drawing.Point(-99999, -99999);
        public System.Drawing.Point TargetPosition
        {
            get { return m_cvTargetPostion; }
        }

        private Rectangle m_roiRect = new Rectangle(0, 0, 0, 0);
        public Rectangle ROIRegion
        {
            get { return m_roiRect; }
        }

        public ImageProcessingViewer(LaserVisionControl laserVisionControl, viewMode viewMode)
        {
            m_cvData = OpenCVData.GetInstance();
            m_laserVisionControl = laserVisionControl;
            m_viewMode = viewMode;

            InitializeComponent();
        }

        private void ImageProcessingViewer_Load(object sender, EventArgs e)
        {
            try
            {
                viewportLayout1.InitViewer(this);
                viewportLayout1.Layers[0].LineWeight = 2;

                // Entities[0] ---> Image
                IplImage pImage = Cv.CreateImage(new CvSize(m_cvData.CamPixel_Width, m_cvData.CamPixel_Height), BitDepth.U8, 1);
                Point3D pt = new Point3D((m_cvData.CamFOV_Width / 2) * -1, (m_cvData.CamFOV_Height / 2) * -1, -0.01);
                Picture pict = new Picture(Plane.XY, pt, m_cvData.CamFOV_Width, m_cvData.CamFOV_Height, BitmapConverter.ToBitmap(pImage));

                if (m_cvData.VisionMode == OpenCVData.modeType.AreaTile)
                {
                    int width = m_cvData.CamPixel_Width * m_cvData.TileArray.X;
                    int height = m_cvData.CamPixel_Height * m_cvData.TileArray.Y;
                    pImage = Cv.CreateImage(new CvSize(width, height), BitDepth.U8, 1);
                    pt = new Point3D((width / 2) * -1, (height / 2) * -1, -0.01);
                    pict = new Picture(Plane.XY, pt, width, height, BitmapConverter.ToBitmap(pImage));
                }

                pict.Selectable = false;
                pict.Lighted = false;
                pict.DrawEdge = false;
                viewportLayout1.Entities.Add(pict);

                // Entities[1] ----> ROI rect
                LinearPath roiRect = new LinearPath(0, 0, 0, 0);
                roiRect.Color = Color.LightGreen;
                roiRect.ColorMethod = colorMethodType.byEntity;
                viewportLayout1.Entities.Add(roiRect);

                // Entities[2]/[3] ----> Target position line
                //Line ln1 = new Line(new Point3D(0, 0), new Point3D(0, 0));
                //ln1.Color = Color.LightGreen;
                //ln1.ColorMethod = colorMethodType.byEntity;
                //ln1.Visible = false;
                //Line ln2 = new Line(new Point3D(0, 0), new Point3D(0, 0));
                //ln2.Color = Color.LightGreen;
                //ln2.ColorMethod = colorMethodType.byEntity;
                //ln2.Visible = false;

                //ln1.Regen(0.1);
                //ln2.Regen(0.1);

                //viewportLayout1.Entities.Add(ln1);
                //viewportLayout1.Entities.Add(ln2);

                // Entities[4]/[5] ----> Target position line
                //Line ln1_res = new Line(new Point3D(0, 0), new Point3D(0, 0));
                //ln1_res.Color = Color.Red;
                //ln1_res.ColorMethod = colorMethodType.byEntity;
                //ln1_res.Visible = false;
                //Line ln2_res = new Line(new Point3D(0, 0), new Point3D(0, 0));
                //ln2_res.Color = Color.Red;
                //ln2_res.ColorMethod = colorMethodType.byEntity;
                //ln2_res.Visible = false;

                //ln1_res.Regen(0.1);
                //ln2_res.Regen(0.1);

                //viewportLayout1.Entities.Add(ln1_res);
                //viewportLayout1.Entities.Add(ln2_res);

                viewportLayout1.Entities.Regen();
                viewportLayout1.Invalidate();

                viewportLayout1.ZoomFit();
                viewportLayout1.ToolBar.Buttons[3].Pushed = true;
                viewportLayout1.ActionMode = devDept.Eyeshot.actionType.Pan;

                m_laserVisionControl.ChangedVisionIndex += new EventHandler(onChangedVisionIndex);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void ImageProcessingViewer_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshImage();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }

        }

        private void ImageProcessingViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_viewMode == viewMode.Live)
            {
                m_laserVisionControl.StopContinuousGrab();
            }

            e.Cancel = true;
            Hide();
        }

        private void viewportLayout1_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_viewMode == viewMode.Processing)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (xyPlane == null)
                        xyPlane = new Plane();
                    viewportLayout1.ScreenToPlane(e.Location, xyPlane, out moveFrom);
                    moveTo = moveFrom;

                    DrawRect();
                }
                else if (e.Button == MouseButtons.Left && e.Clicks == 2)
                {
                    if (xyPlane == null)
                        xyPlane = new Plane();
                    viewportLayout1.ScreenToPlane(e.Location, xyPlane, out m_targetPosition);

                    DrawTemplateOffsetPosition();
                    SetTargetPosition();
                }
            }
        }

        private void viewportLayout1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_viewMode == viewMode.Processing)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (xyPlane == null)
                        xyPlane = new Plane();
                    viewportLayout1.ScreenToPlane(e.Location, xyPlane, out moveTo);

                    DrawRect();
                }
            }
        }

        private void viewportLayout1_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_viewMode == viewMode.Processing)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (xyPlane == null)
                        xyPlane = new Plane();
                    viewportLayout1.ScreenToPlane(e.Location, xyPlane, out moveTo);

                    SetRoi();

                    DrawRect();
                }
            }
        }

        private void DrawRect()
        {
            if (m_viewMode == viewMode.Processing)
            {
                LinearPath rect = (LinearPath)viewportLayout1.Entities[1];
                rect.Vertices[0] = moveFrom;
                rect.Vertices[1].X = moveTo.X;
                rect.Vertices[1].Y = moveFrom.Y;
                rect.Vertices[2] = moveTo;
                rect.Vertices[3].X = moveFrom.X;
                rect.Vertices[3].Y = moveTo.Y;
                rect.Vertices[4] = moveFrom;

                rect.Regen(0.1);

                viewportLayout1.Entities.Regen();
                viewportLayout1.Invalidate();
            }
            
        }

        private void DrawTemplateOffsetPosition()
        {
            if (m_viewMode == viewMode.Processing)
            {
                if (m_targetPosition != null)
                {
                    if (viewportLayout1.Entities.Count < 3)
                    {
                        Line ln1 = new Line(new Point3D(m_targetPosition.X - 1, m_targetPosition.Y),
                                            new Point3D(m_targetPosition.X + 1, m_targetPosition.Y));
                        ln1.Color = Color.Red;
                        ln1.ColorMethod = colorMethodType.byEntity;
                        Line ln2 = new Line(new Point3D(m_targetPosition.X, m_targetPosition.Y - 1),
                                            new Point3D(m_targetPosition.X, m_targetPosition.Y + 1));
                        ln2.Color = Color.Red;
                        ln2.ColorMethod = colorMethodType.byEntity;

                        ln1.Regen(0.1);
                        ln2.Regen(0.1);

                        viewportLayout1.Entities.Add(ln1);
                        viewportLayout1.Entities.Add(ln2);
                    }
                    else
                    {
                        Line ln1 = (Line)viewportLayout1.Entities[2];
                        ln1.StartPoint.X = m_targetPosition.X - 1;
                        ln1.EndPoint.X = m_targetPosition.X + 1;
                        ln1.EndPoint.Y = ln1.StartPoint.Y = m_targetPosition.Y;
                        ln1.StartPoint.Z = ln1.EndPoint.Z = 0.01;
                        ln1.Visible = true;

                        Line ln2 = (Line)viewportLayout1.Entities[3];
                        ln2.StartPoint.Y = m_targetPosition.Y - 1;
                        ln2.EndPoint.Y = m_targetPosition.Y + 1;
                        ln2.EndPoint.X = ln2.StartPoint.X = m_targetPosition.X;
                        ln2.StartPoint.Z = ln2.EndPoint.Z = 0.01;
                        ln2.Visible = true;

                        ln1.Regen(0.1);
                        ln2.Regen(0.1);
                    }
                }
                else
                {
                    //if (viewportLayout1.Entities.Count > 3)
                    //{
                    //    viewportLayout1.Entities.RemoveAt(3);
                    //    viewportLayout1.Entities.RemoveAt(2);
                    //}
                    if (viewportLayout1.Entities.Count > 3)
                    {
                        Line ln1 = (Line)viewportLayout1.Entities[2];
                        ln1.Visible = false;
                        Line ln2 = (Line)viewportLayout1.Entities[3];
                        ln2.Visible = false;
                        ln1.Regen(0.1);
                        ln2.Regen(0.1);
                    }
                    
                }

                viewportLayout1.Entities.Regen();
                viewportLayout1.Invalidate();
                
            }
        }

        private void SetRoi()
        {
            if (m_viewMode == viewMode.Processing)
            {
                System.Drawing.Point start = m_cvData.ConvertPixelPosition(new CvPoint2D32f(moveFrom.X, moveFrom.Y));
                System.Drawing.Point end = m_cvData.ConvertPixelPosition(new CvPoint2D32f(moveTo.X, moveTo.Y));

                m_roiRect.X = start.X;
                m_roiRect.Y = start.Y;
                m_roiRect.Width = Math.Abs(end.X - start.X);
                m_roiRect.Height = Math.Abs(end.Y - start.Y);
            }
        }

        private void SetTargetPosition()
        {
            if (m_viewMode == viewMode.Processing)
            {
                m_cvTargetPostion = m_cvData.ConvertPixelPosition(new CvPoint2D32f(m_targetPosition.X, m_targetPosition.Y));
            }
        }

        public void SetTargetPosition(int pixX, int pixY)
        {
            if (m_viewMode == viewMode.Processing)
            {
                CvPoint2D32f cvPoint = m_cvData.ConvertMMPosition(new CvPoint(pixX, pixY));
                m_targetPosition.X = cvPoint.X;
                m_targetPosition.Y = cvPoint.Y;

                DrawTemplateOffsetPosition();
                SetTargetPosition();
            }
        }

        public void ChangeViewMode(viewMode viewMode)
        {
            m_viewMode = viewMode;
        }

        public void RefreshImage()
        {
            if (viewportLayout1.Entities.Count <= 0)
                return;

            if (Visible)
            {
                m_cvData = OpenCVData.GetInstance();

                IplImage img = null;
                Picture picEnt = (Picture)viewportLayout1.Entities[0];
                switch (m_viewMode)
                {
                    case viewMode.Orignal:
                        {
                            img = m_cvData.GetIPLImage();
                            if (img == null)
                            {
                                //Hide();
                                return;
                            }
                            picEnt.Image = BitmapConverter.ToBitmap(img);
                        }
                        break;

                    case viewMode.Processing:
                        {
                            img = m_cvData.GetIPLImageProcessing();
                            if (img == null)
                            {
                                if (m_cvData.GetIPLImage() == null)
                                {
                                    Hide();
                                    return;
                                }
                                else
                                {
                                    m_cvData.ResetResultImage();
                                    m_cvData.ResetProcessImage();
                                    img = m_cvData.GetIPLImageProcessing();
                                }

                            }

                            //if (m_cvData.GetResultPoint().X != -1 && m_cvData.GetResultPoint().Y != -1)
                            //{
                            ////    ResetROI();
                            ////    ResetTemplateOffsetPosition();
                            //    Line ln1 = (Line)viewportLayout1.Entities[4];
                            //    ln1.StartPoint.X = m_cvData.GetResultPosition().X - 1;
                            //    ln1.EndPoint.X = m_cvData.GetResultPosition().X + 1;
                            //    ln1.EndPoint.Y = ln1.StartPoint.Y = m_cvData.GetResultPosition().Y;
                            //    ln1.StartPoint.Z = ln1.EndPoint.Z = 0.01;
                            //    ln1.Visible = true;

                            //    Line ln2 = (Line)viewportLayout1.Entities[5];
                            //    ln2.StartPoint.Y = m_cvData.GetResultPosition().Y - 1;
                            //    ln2.EndPoint.Y = m_cvData.GetResultPosition().Y + 1;
                            //    ln2.EndPoint.X = ln2.StartPoint.X = m_cvData.GetResultPosition().X;
                            //    ln2.StartPoint.Z = ln2.EndPoint.Z = 0.01;
                            //    ln2.Visible = true;

                            //    ln1.Regen(0.1);
                            //    ln2.Regen(0.1);
                            //}
                            //else
                            //{
                            //    Line ln1 = (Line)viewportLayout1.Entities[4];
                            //    ln1.Visible = false;

                            //    Line ln2 = (Line)viewportLayout1.Entities[5];
                            //    ln2.Visible = false;

                            //    ln1.Regen(0.1);
                            //    ln2.Regen(0.1);
                            //}

                            //if (viewportLayout1.Entities.Count > 6)
                            //{
                            //    viewportLayout1.Entities.RemoveRange(6, viewportLayout1.Entities.Count - 6);                                
                            //}

                            //List<CvPoint> resutlpoints = m_cvData.GetListResultPoints();
                            //if (resutlpoints.Count > 0)
                            //{
                            //    List<devDept.Eyeshot.Entities.Point> pointEntities = new List<devDept.Eyeshot.Entities.Point>();
                            //    for (int i = 0; i < resutlpoints.Count; i++)
                            //    {
                            //        CvPoint2D32f resPosition = m_cvData.ConvertMMPosition(resutlpoints[i]);
                            //        devDept.Eyeshot.Entities.Point resPt = new devDept.Eyeshot.Entities.Point(resPosition.X, resPosition.Y);
                            //        resPt.Color = Color.Red;
                            //        resPt.ColorMethod = colorMethodType.byEntity;
                            //        resPt.LineWeight = 3;
                            //        resPt.LineWeightMethod = colorMethodType.byEntity;

                            //        pointEntities.Add(resPt);
                            //        //viewportLayout1.Entities.Add(resPt);
                            //    }
                            //    viewportLayout1.Entities.AddRange(pointEntities);
                            //}

                            picEnt.Image = BitmapConverter.ToBitmap(img);
                            
                        }
                        break;

                    case viewMode.Result:
                        {
                            img = m_cvData.GetIPLImageResult();
                            if (img == null)
                            {
                                Hide();
                                return;
                            }

                            //if (m_cvData.GetResultPosition().X != -1 && m_cvData.GetResultPosition().Y != -1)
                            //{
                            //    //    ResetROI();
                            //    //    ResetTemplateOffsetPosition();
                            //    Line ln1 = (Line)viewportLayout1.Entities[4];
                            //    ln1.StartPoint.X = m_cvData.GetResultPosition().X - 1;
                            //    ln1.EndPoint.X = m_cvData.GetResultPosition().X + 1;
                            //    ln1.EndPoint.Y = ln1.StartPoint.Y = m_cvData.GetResultPosition().Y;
                            //    ln1.StartPoint.Z = ln1.EndPoint.Z = 0.01;
                            //    ln1.Visible = true;

                            //    Line ln2 = (Line)viewportLayout1.Entities[5];
                            //    ln2.StartPoint.Y = m_cvData.GetResultPosition().Y - 1;
                            //    ln2.EndPoint.Y = m_cvData.GetResultPosition().Y + 1;
                            //    ln2.EndPoint.X = ln2.StartPoint.X = m_cvData.GetResultPosition().X;
                            //    ln2.StartPoint.Z = ln2.EndPoint.Z = 0.01;
                            //    ln2.Visible = true;

                            //    ln1.Regen(0.1);
                            //    ln2.Regen(0.1);
                            //}

                            //if (viewportLayout1.Entities.Count > 6)
                            //{
                            //    viewportLayout1.Entities.RemoveRange(6, viewportLayout1.Entities.Count - 6);
                            //}

                            //List<CvPoint> resutlpoints = m_cvData.GetListResultPoints();
                            //if (resutlpoints.Count > 0)
                            //{
                            //    List<devDept.Eyeshot.Entities.Point> pointEntities = new List<devDept.Eyeshot.Entities.Point>();
                            //    for (int i = 0; i < resutlpoints.Count; i++)
                            //    {
                            //        CvPoint2D32f resPosition = m_cvData.ConvertMMPosition(resutlpoints[i]);
                            //        devDept.Eyeshot.Entities.Point resPt = new devDept.Eyeshot.Entities.Point(resPosition.X, resPosition.Y);
                            //        resPt.Color = Color.Red;
                            //        resPt.ColorMethod = colorMethodType.byEntity;
                            //        resPt.LineWeight = 3;
                            //        resPt.LineWeightMethod = colorMethodType.byEntity;

                            //        pointEntities.Add(resPt);
                            //        //viewportLayout1.Entities.Add(resPt);
                            //    }

                            //    viewportLayout1.Entities.AddRange(pointEntities);
                            //}

                            picEnt.Image = BitmapConverter.ToBitmap(img);
                        }
                        break;

                    case viewMode.Live:
                        {
                            img = m_cvData.GetIPLImage();
                            if (img == null)
                            {
                                Hide();
                                return;
                            }
                            picEnt.Image = BitmapConverter.ToBitmap(img);
                        }
                        break;
                }

                if (viewportLayout1.InvokeRequired)
                {
                    viewportLayout1.BeginInvoke((MethodInvoker)delegate()
                    {
                        viewportLayout1.Entities.Regen();
                        viewportLayout1.Invalidate();

                        richTextBox_string_result.Clear();
                        richTextBox_string_result.Text = m_cvData.StringResult.ToString();
                    });
                }
                else
                {
                    viewportLayout1.Entities.Regen();
                    viewportLayout1.Invalidate();

                    richTextBox_string_result.Clear();
                    richTextBox_string_result.Text = m_cvData.StringResult.ToString();
                }
            }            
        }

        public void ResetROI()
        {
            if (moveFrom != null && moveTo != null)
            {
                moveFrom.X = 0;
                moveFrom.Y = 0;
                moveFrom.Z = 0;

                moveTo.X = 0;
                moveTo.Y = 0;
                moveTo.Z = 0;

                SetRoi();

                DrawRect();
            }
            
        }

        private void button_ROI_clear_Click(object sender, EventArgs e)
        {
            ResetROI();
        }

        public void ResetTemplateOffsetPosition()
        {
            if (m_targetPosition != null)
            {
                m_targetPosition = null;
            }

            m_cvTargetPostion.X = -99999;
            m_cvTargetPostion.Y = -99999;

            DrawTemplateOffsetPosition();
        }

        private void button_position_offset_clear_Click(object sender, EventArgs e)
        {
            ResetTemplateOffsetPosition();
        }

        public void onChangedVisionIndex(object sender, EventArgs e)
        {
            m_cvData = OpenCVData.GetInstance();
            RefreshImage();
        }
    }
}
