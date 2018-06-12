using devDept.Geometry;
using GalvoScanner.LaserVision.OpenCV;
using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.LaserVision.DialogLaserVision
{
    class ImgViewerViewportLayout : devDept.Eyeshot.ViewportLayout
    {
        OpenCVData m_cvData = null;
        ImageProcessingViewer m_parent = null;

        public ImgViewerViewportLayout()
        {
            m_cvData = OpenCVData.GetInstance();
        }

        public void InitViewer(ImageProcessingViewer parent)
        {
            m_parent = parent;
        }

        protected override void DrawOverlay(devDept.Eyeshot.ViewportLayout.DrawSceneParams data)
        {
            base.DrawOverlay(data);

            try
            {
                if (m_parent.ViewerMode == ImageProcessingViewer.viewMode.Processing || (m_parent.ViewerMode == ImageProcessingViewer.viewMode.Result))
                {
                    if (m_cvData.GetResultPoint() != new CvPoint(-1, -1))
                    {
                        renderContext.SetColorWireframe(Color.Aqua);
                        renderContext.SetLineSize((float)1.5);

                        renderContext.DrawLine(WorldToScreen(new Point3D(m_cvData.GetResultPosition().X - 1, m_cvData.GetResultPosition().Y)),
                                                        WorldToScreen(new Point3D(m_cvData.GetResultPosition().X + 1, m_cvData.GetResultPosition().Y)));
                        renderContext.DrawLine(WorldToScreen(new Point3D(m_cvData.GetResultPosition().X, m_cvData.GetResultPosition().Y - 1)),
                                                        WorldToScreen(new Point3D(m_cvData.GetResultPosition().X, m_cvData.GetResultPosition().Y + 1)));
                    }

                    if (m_cvData.GetResultRect() != new CvRect(-1, -1, 0, 0))
                    {
                        renderContext.SetColorWireframe(Color.Aqua);
                        renderContext.SetLineSize((float)1.5);

                        CvPoint2D32f firPt = m_cvData.ConvertMMPosition(new CvPoint(m_cvData.GetResultRect().X, m_cvData.GetResultRect().Y));
                        CvPoint2D32f secPt = m_cvData.ConvertMMPosition(new CvPoint(m_cvData.GetResultRect().X + m_cvData.GetResultRect().Width, m_cvData.GetResultRect().Y));
                        renderContext.DrawLine(WorldToScreen(new Point3D(firPt.X, firPt.Y)), WorldToScreen(new Point3D(secPt.X, secPt.Y)));

                        firPt = m_cvData.ConvertMMPosition(new CvPoint(m_cvData.GetResultRect().X + m_cvData.GetResultRect().Width, m_cvData.GetResultRect().Y));
                        secPt = m_cvData.ConvertMMPosition(new CvPoint(m_cvData.GetResultRect().X + m_cvData.GetResultRect().Width, m_cvData.GetResultRect().Y + m_cvData.GetResultRect().Height));
                        renderContext.DrawLine(WorldToScreen(new Point3D(firPt.X, firPt.Y)), WorldToScreen(new Point3D(secPt.X, secPt.Y)));

                        firPt = m_cvData.ConvertMMPosition(new CvPoint(m_cvData.GetResultRect().X + m_cvData.GetResultRect().Width, m_cvData.GetResultRect().Y + m_cvData.GetResultRect().Height));
                        secPt = m_cvData.ConvertMMPosition(new CvPoint(m_cvData.GetResultRect().X, m_cvData.GetResultRect().Y + m_cvData.GetResultRect().Height));
                        renderContext.DrawLine(WorldToScreen(new Point3D(firPt.X, firPt.Y)), WorldToScreen(new Point3D(secPt.X, secPt.Y)));

                        firPt = m_cvData.ConvertMMPosition(new CvPoint(m_cvData.GetResultRect().X, m_cvData.GetResultRect().Y + m_cvData.GetResultRect().Height));
                        secPt = m_cvData.ConvertMMPosition(new CvPoint(m_cvData.GetResultRect().X, m_cvData.GetResultRect().Y));
                        renderContext.DrawLine(WorldToScreen(new Point3D(firPt.X, firPt.Y)), WorldToScreen(new Point3D(secPt.X, secPt.Y)));
                    }

                    //if (m_parent.TargetPosition_3D != null)
                    //{
                    //    renderContext.SetColorWireframe(Color.LawnGreen);
                    //    renderContext.DrawLine(WorldToScreen(new Point3D(m_parent.TargetPosition_3D.X - 1, m_parent.TargetPosition_3D.Y)),
                    //                                    WorldToScreen(new Point3D(m_parent.TargetPosition_3D.X + 1, m_parent.TargetPosition_3D.Y)));
                    //    renderContext.DrawLine(WorldToScreen(new Point3D(m_parent.TargetPosition_3D.X, m_parent.TargetPosition_3D.Y - 1)),
                    //                                    WorldToScreen(new Point3D(m_parent.TargetPosition_3D.X, m_parent.TargetPosition_3D.Y + 1)));
                    //}

                    List<CvPoint> resutlpoints = m_cvData.GetListResultPoints();
                    if (resutlpoints.Count > 0)
                    {
                        Point3D[] resDispPoints = new Point3D[resutlpoints.Count];

                        renderContext.SetColorWireframe(Color.Red);
                        renderContext.SetLineSize((float)3);

                        int index = 0;
                        IEnumerator enPoints = resutlpoints.GetEnumerator();
                        while (enPoints.MoveNext())
                        {
                            CvPoint pot = (CvPoint)enPoints.Current;
                            CvPoint2D32f resPosition = m_cvData.ConvertMMPosition(pot);
                            resDispPoints[index] = WorldToScreen(new Point3D(resPosition.X, resPosition.Y));

                            index++;
                        }

                        renderContext.DrawPoints(resDispPoints);
                    }

                    List<CvLine2D> resultlines = m_cvData.GetListResultLines();
                    if (resultlines.Count > 0)
                    {
                        renderContext.SetColorWireframe(Color.Red);
                        renderContext.SetLineSize((float)0.5);

                        IEnumerator enPoints = resultlines.GetEnumerator();
                        while (enPoints.MoveNext())
                        {
                            CvLine2D ln = (CvLine2D)enPoints.Current;
                            CvPoint2D32f fir = m_cvData.ConvertMMPosition(new CvPoint((int)ln.Vx, (int)ln.Vy));
                            CvPoint2D32f sec = m_cvData.ConvertMMPosition(new CvPoint((int)ln.X1, (int)ln.Y1));
                            renderContext.DrawLine(WorldToScreen(new Point3D(fir.X, fir.Y)), WorldToScreen(new Point3D(sec.X, sec.Y)));
                        }
                    }
                }

            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
            }
        }
    }
}
