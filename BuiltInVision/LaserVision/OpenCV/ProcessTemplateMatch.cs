using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.LaserVision.OpenCV
{
    public class ProcessTemplateMatch
    {
        public const string VISIONRCP_TEMPLATE_SECTION = "Vision Template Recipe";
        public const string VISIONRCP_TEMPLATE_KEY_TEMPIMGPATH = "TEMPLATE_IMAGEPATH";
        public const string VISIONRCP_TEMPLATE_KEY_TEMPROIX = "TEMPLATE_ROIX";
        public const string VISIONRCP_TEMPLATE_KEY_TEMPROIY = "TEMPLATE_ROIY";
        public const string VISIONRCP_TEMPLATE_KEY_TEMPROIW = "TEMPLATE_ROIW";
        public const string VISIONRCP_TEMPLATE_KEY_TEMPROIH = "TEMPLATE_ROIH";
        public const string VISIONRCP_TEMPLATE_KEY_TEMPOFFSX = "TEMPLATE_OFFSETX";
        public const string VISIONRCP_TEMPLATE_KEY_TEMPOFFSY = "TEMPLATE_OFFSETY";
        public const string VISIONRCP_TEMPLATE_KEY_SOCRETHRESHOLD = "SOCRE_THRESHOLD";
        public const string VISIONRCP_TEMPLATE_KEY_INSPX = "INSP_X";
        public const string VISIONRCP_TEMPLATE_KEY_INSPY = "INSP_Y";
        public const string VISIONRCP_TEMPLATE_KEY_INSPW = "INSP_W";
        public const string VISIONRCP_TEMPLATE_KEY_INSPH = "INSP_H";
        public const string VISIONRCP_TEMPLATE_KEY_TILT = "TILT"; 

        OpenCVData m_parent = null;
        public ProcessTemplateMatch(OpenCVData parent)
        {
            m_parent = parent;
        }

        ~ProcessTemplateMatch()
        {
            if (m_pImageTemplate != null)
            {
                m_pImageTemplate.Dispose();
                m_pImageTemplate = null;
            }
        }

        private int m_nProcessNum = -1;
        public int ProcessNum
        {
            get { return m_nProcessNum; }
            set { m_nProcessNum = value; }
        }

        // Recipe ----------------------------- 
        private Rectangle m_rectTemplateROI = new Rectangle(0, 0, 0, 0);
        public Rectangle TemplateROI
        {
            get { return m_rectTemplateROI; }
            set { m_rectTemplateROI = value; }
        }

        private CvPoint m_ptTartgetOffsetPosition = new CvPoint(0, 0);
        public CvPoint TartgetOffsetPosition
        {
            get { return m_ptTartgetOffsetPosition; }
            set { m_ptTartgetOffsetPosition = value; }
        }

        private IplImage m_pImageTemplate;
        public IplImage GetIPLImageTemplate()
        {
            return m_pImageTemplate;
        }
        public void SetIPLImageTemplate(IplImage img)
        {
            m_pImageTemplate = img;
        }

        private string m_strTemplateImagePath = "";
        public string TemplateImagePath
        {
            get { return m_strTemplateImagePath; }
            set { m_strTemplateImagePath = value; }
        }

        private double m_dScoreThreshold = 0.9;
        public double ScoreThreshold
        {
            get { return m_dScoreThreshold; }
            set { m_dScoreThreshold = value; }
        }

        private CvRect m_rtInspRect = new CvRect(-1, -1, 0, 0);
        public CvRect InspectionRect
        {
            get { return m_rtInspRect; }
            set { m_rtInspRect = value; }
        }

        //private double m_nMinShift = 100;
        //private double m_nMaxShift = 300;
        //private double m_fTilt = 1;
        //public double MinShift
        //{
        //    get { return m_nMinShift; }
        //    set { m_nMinShift = value; }
        //}
        //public double MaxShift
        //{
        //    get { return m_nMaxShift; }
        //    set { m_nMaxShift = value; }
        //}
        //public double Tilt
        //{
        //    get { return m_fTilt; }
        //    set { m_fTilt = value; }
        //}
        // ------------------------------------

        // Result -----------------------------
        private Rectangle m_rectMatchResultROI = new Rectangle(0, 0, 0, 0);
        public Rectangle MatchResultROI
        {
            get { return m_rectMatchResultROI; }
        }

        public CvPoint TargetResultPosition
        {
            get { return new CvPoint(m_rectMatchResultROI.X + TartgetOffsetPosition.X, m_rectMatchResultROI.Y + m_ptTartgetOffsetPosition.Y); }
        }

        private matchResultType m_executeMatchResult = matchResultType.None;
        public matchResultType ExcuteMatchResult
        {
            get { return m_executeMatchResult; }
        }

        private IplImage m_pImageTemplateResult;
        public IplImage GetIPLImageTemplateResult()
        {
            return m_pImageTemplateResult;
        }
        public void SetIPLImageTemplateResult(IplImage img)
        {
            m_pImageTemplateResult = img;
        }

        private double m_dScore = 0;
        public double MatchScore
        {
            get { return m_dScore; }
        }

        public double ResultTilt
        {
            get { return m_fResultTilt; }
        }
        // ------------------------------------ 

        public bool LoadTemplateImage(string path)
        {
            IplImage img = Cv.LoadImage(path);
            if (img != null)
            {
                SetIPLImageTemplate(img);
                m_strTemplateImagePath = path;
                m_rectTemplateROI = new Rectangle(0, 0, img.Width, img.Height);
                return true;
            }            
            return false;
        }

        public bool SaveTemplateImage(string path, Rectangle roi)
        {
            if (roi.Width != 0 && roi.Height != 0)
            {
                m_rectTemplateROI = roi;

                IplImage img = m_parent.GetIPLImageProcessing();
                if (img != null)
                {
                    IplImage roiImg = img.Clone(new CvRect(roi.X, roi.Y, roi.Width, roi.Height));
                    m_parent.GetProcessTeplateMatch().SetIPLImageTemplate(roiImg.Clone());
                    m_parent.GetProcessTeplateMatch().TemplateImagePath = path;

                    roiImg.SaveImage(path);

                    return true;
                }
            }
            return false;
        }

        public enum matchResultType { None = 0, OK, NoTemplateImage, NoProcessingImage, LowScore, Shift, Tilt };

        public matchResultType ExcuteTemplateMatch()
        {
            try
            {
                /*
                m_parent.SetResultPoint(new CvPoint(-1, -1));
                m_parent.GetListResultPoints().Clear();
                m_parent.SetResultRect(new CvRect(-1, -1, 0, 0));

                if (m_pImageTemplate == null)
                {
                    return matchResultType.NoTemplateImage;
                }

                if (m_parent.GetIPLImageProcessing() == null)
                {
                    return matchResultType.NoTemplateImage;
                }

                IplImage targetImg = m_parent.GetIPLImageProcessing();
                IplImage resImg = Cv.CreateImage(Cv.Size((targetImg.Width - m_pImageTemplate.Width + 1), (targetImg.Height - m_pImageTemplate.Height + 1)), BitDepth.F32, 1);

                Cv.MatchTemplate(targetImg, m_pImageTemplate, resImg, MatchTemplateMethod.CCorrNormed);


                CvPoint minloc, maxloc;
                double minval, maxval;
                Cv.MinMaxLoc(resImg, out minval, out maxval, out minloc, out maxloc, null);

                HoughLines(targetImg);

                m_dScore = maxval;

                if (m_dScoreThreshold < m_dScore)
                {
                    m_rectMatchResultROI
.X = maxloc.X;
                    m_rectMatchResultROI.Y = maxloc.Y;
                    m_rectMatchResultROI.Width = m_pImageTemplate.Width;
                    m_rectMatchResultROI.Height = m_pImageTemplate.Height;
                    IplImage resultImage = targetImg.Clone();
                    m_pImageTemplateResult = targetImg.Clone();
                    m_parent.SetIPLImageProcessing(resultImage);
                    m_parent.SetResultPoint(new CvPoint(TargetResultPosition.X, TargetResultPosition.Y));
                    m_parent.SetResultRect(new CvRect(maxloc.X, maxloc.Y, m_pImageTemplate.Width, m_pImageTemplate.Height));                    

                    m_parent.StringResult.Append("Score : "); m_parent.StringResult.AppendLine(m_dScore.ToString("0.00"));
                    m_parent.StringResult.Append("Position : "); m_parent.StringResult.Append(TargetResultPosition.X); m_parent.StringResult.Append(", "); m_parent.StringResult.AppendLine(TargetResultPosition.Y.ToString());
                    m_parent.StringResult.Append("Tilt : "); m_parent.StringResult.AppendLine(m_fResultTilt.ToString("0.00"));

                    m_executeMatchResult = matchResultType.OK;

                    if (TargetResultPosition.X < m_nMinShift || TargetResultPosition.X > m_nMaxShift)
                    {
                        m_executeMatchResult = matchResultType.Shift;
                    }
                    if (TargetResultPosition.Y < m_nMinShift || TargetResultPosition.Y > m_nMaxShift)
                    {
                        m_executeMatchResult = matchResultType.Shift;
                    }
                    if (m_fResultTilt > m_fTilt)
                    {
                        m_executeMatchResult = matchResultType.Tilt;
                    }
                }
                else
                {
                    m_parent.SetResultPoint(new CvPoint(-1, -1));
                    m_executeMatchResult = matchResultType.LowScore;
                }

                resImg.Dispose();
                Dispose();
                */

                m_parent.SetResultPoint(new CvPoint(-1, -1));
                m_parent.GetListResultPoints().Clear();
                m_parent.SetResultRect(new CvRect(-1, -1, 0, 0));

                if (m_pImageTemplate == null)
                {
                    return matchResultType.NoTemplateImage;
                }

                if (m_parent.GetIPLImageProcessing() == null)
                {
                    return matchResultType.NoTemplateImage;
                }

                IplImage targetImg = m_parent.GetIPLImageProcessing();
                IplImage resImg = Cv.CreateImage(Cv.Size((targetImg.Width - m_pImageTemplate.Width + 1), (targetImg.Height - m_pImageTemplate.Height + 1)), BitDepth.F32, 1);

                Cv.MatchTemplate(targetImg, m_pImageTemplate, resImg, MatchTemplateMethod.CCorrNormed);

                CvPoint minloc, maxloc;
                double minval, maxval;
                Cv.MinMaxLoc(resImg, out minval, out maxval, out minloc, out maxloc, null);

                m_dScore = maxval;

                m_rectMatchResultROI.X = maxloc.X;
                m_rectMatchResultROI.Y = maxloc.Y;
                m_rectMatchResultROI.Width = m_pImageTemplate.Width;
                m_rectMatchResultROI.Height = m_pImageTemplate.Height;

                m_parent.StringResult.AppendLine("<Template match>");
                m_parent.StringResult.Append("Score : "); m_parent.StringResult.AppendLine(m_dScore.ToString("0.00"));
                m_parent.StringResult.Append("Position : "); m_parent.StringResult.Append(TargetResultPosition.X); m_parent.StringResult.Append(", "); m_parent.StringResult.AppendLine(TargetResultPosition.Y.ToString());

                if (m_dScoreThreshold < m_dScore)
                {                    

                    IplImage resultImage = targetImg.Clone();
                    m_pImageTemplateResult = targetImg.Clone();
                    m_parent.SetIPLImageProcessing(resultImage);
                    m_parent.SetResultPoint(new CvPoint(TargetResultPosition.X, TargetResultPosition.Y));
                    m_parent.SetResultRect(new CvRect(maxloc.X, maxloc.Y, m_pImageTemplate.Width, m_pImageTemplate.Height));

                    Cv.Rectangle(m_pImageTemplateResult, Cv.Point(maxloc.X, maxloc.Y), Cv.Point(maxloc.X + m_pImageTemplate.Width, maxloc.Y + m_pImageTemplate.Height), CvColor.Red, 3, 0, 0);
                    Cv.Line(m_pImageTemplateResult, Cv.Point(TargetResultPosition.X - 30, TargetResultPosition.Y), Cv.Point(TargetResultPosition.X + 30, TargetResultPosition.Y), CvColor.OrangeRed, 2);
                    Cv.Line(m_pImageTemplateResult, Cv.Point(TargetResultPosition.X, TargetResultPosition.Y - 30), Cv.Point(TargetResultPosition.X, TargetResultPosition.Y + 30), CvColor.OrangeRed, 2);

                    m_executeMatchResult = matchResultType.OK;

                    if (m_rtInspRect.X != -1 && m_rtInspRect.Y != -1)
                    {
                        if (TargetResultPosition.X < m_rtInspRect.X || TargetResultPosition.X > m_rtInspRect.X + m_rtInspRect.Width)
                        {
                            m_executeMatchResult = matchResultType.Shift;
                        }

                        if (TargetResultPosition.Y < m_rtInspRect.Y || TargetResultPosition.Y > m_rtInspRect.Y + m_rtInspRect.Height)
                        {
                            m_executeMatchResult = matchResultType.Shift;
                        }
                    }
                }
                else
                {
                    m_parent.SetResultPoint(new CvPoint(-1, -1));
                    m_executeMatchResult = matchResultType.LowScore;
                }

                resImg.Dispose();

                return m_executeMatchResult;
            }
            catch (Exception E)
            {                
                throw E;
            }
        }

        private double m_fResultTilt = 0;

        public matchResultType ExcuteHoughLine()
        {
            try
            {
                m_parent.SetResultPoint(new CvPoint(-1, -1));
                m_parent.GetListResultPoints().Clear();
                m_parent.SetResultRect(new CvRect(-1, -1, 0, 0));

                if (m_pImageTemplate == null)
                {
                    return matchResultType.NoTemplateImage;
                }

                if (m_parent.GetIPLImageProcessing() == null)
                {
                    return matchResultType.NoTemplateImage;
                }

                IplImage targetImg = m_parent.GetIPLImageProcessing();
                IplImage resImg = Cv.CreateImage(Cv.Size((targetImg.Width - m_pImageTemplate.Width + 1), (targetImg.Height - m_pImageTemplate.Height + 1)), BitDepth.F32, 1);

                Cv.MatchTemplate(targetImg, m_pImageTemplate, resImg, MatchTemplateMethod.CCorrNormed);

                CvPoint minloc, maxloc;
                double minval, maxval;
                Cv.MinMaxLoc(resImg, out minval, out maxval, out minloc, out maxloc, null);

                HoughLines(targetImg);

                m_dScore = maxval;

                m_rectMatchResultROI.X = maxloc.X;
                m_rectMatchResultROI.Y = maxloc.Y;
                m_rectMatchResultROI.Width = m_pImageTemplate.Width;
                m_rectMatchResultROI.Height = m_pImageTemplate.Height;

                IplImage resultImage = houline.Clone();
                m_pImageTemplateResult = houline.Clone();
                m_parent.SetIPLImageProcessing(resultImage);
                m_parent.SetResultPoint(new CvPoint(TargetResultPosition.X, TargetResultPosition.Y));
                
                m_parent.StringResult.Append("Score : "); m_parent.StringResult.AppendLine(m_dScore.ToString("0.00"));
                m_parent.StringResult.Append("Position : "); m_parent.StringResult.Append(TargetResultPosition.X); m_parent.StringResult.Append(", "); m_parent.StringResult.AppendLine(TargetResultPosition.Y.ToString());
                m_parent.StringResult.Append("Tilt : "); m_parent.StringResult.AppendLine(m_fResultTilt.ToString("0.00"));

                m_executeMatchResult = matchResultType.OK;

                //if (m_fResultTilt > m_fTilt)
                //{
                //    m_executeMatchResult = matchResultType.Tilt;
                //}

                resImg.Dispose();
                Dispose();

                return m_executeMatchResult;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public void SaveRecipeINI(string path, IniFile ini)
        {
            try
            {
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPIMGPATH, m_strTemplateImagePath, path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIX, m_rectTemplateROI.X.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIY, m_rectTemplateROI.Y.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIW, m_rectTemplateROI.Width.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIH, m_rectTemplateROI.Height.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPOFFSX, m_ptTartgetOffsetPosition.X.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPOFFSY, m_ptTartgetOffsetPosition.Y.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_SOCRETHRESHOLD, m_dScoreThreshold.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPX, m_rtInspRect.X.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPY, m_rtInspRect.Y.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPW, m_rtInspRect.Width.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPH, m_rtInspRect.Height.ToString(), path);
                //ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TILT, m_fTilt.ToString(), path); 
            }
            catch (Exception E)
            {                
                throw E;
            }
        }

        public void SaveRecipeINI(string path)
        {
            try
            {
                IniFile ini = new IniFile();
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPIMGPATH, m_strTemplateImagePath, path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIX, m_rectTemplateROI.X.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIY, m_rectTemplateROI.Y.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIW, m_rectTemplateROI.Width.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIH, m_rectTemplateROI.Height.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPOFFSX, m_ptTartgetOffsetPosition.X.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPOFFSY, m_ptTartgetOffsetPosition.Y.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_SOCRETHRESHOLD, m_dScoreThreshold.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPX, m_rtInspRect.X.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPY, m_rtInspRect.Y.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPW, m_rtInspRect.Width.ToString(), path);
                ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPH, m_rtInspRect.Height.ToString(), path);
                //ini.IniWriteValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TILT, m_fTilt.ToString(), path); 
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public bool LoadRecipeINI(string path, IniFile ini)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    String iniValue = "";
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPIMGPATH, path);
                    if (iniValue != "") 
                    {
                        m_strTemplateImagePath = iniValue;
                        LoadTemplateImage(m_strTemplateImagePath);
                    }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIX, path);
                    if (iniValue != "") { m_rectTemplateROI.X = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIY, path);
                    if (iniValue != "") { m_rectTemplateROI.Y = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIW, path);
                    if (iniValue != "") { m_rectTemplateROI.Width = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIH, path);
                    if (iniValue != "") { m_rectTemplateROI.Height = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPOFFSX, path);
                    if (iniValue != "") { m_ptTartgetOffsetPosition.X = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPOFFSY, path);
                    if (iniValue != "") { m_ptTartgetOffsetPosition.Y = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_SOCRETHRESHOLD, path);
                    if (iniValue != "") { m_dScoreThreshold = Convert.ToDouble(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPX, path);
                    if (iniValue != "") { m_rtInspRect.X = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPY, path);
                    if (iniValue != "") { m_rtInspRect.Y = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPW, path);
                    if (iniValue != "") { m_rtInspRect.Width = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPH, path);
                    if (iniValue != "") { m_rtInspRect.Height = Convert.ToInt32(iniValue); }
                    //iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TILT, path);
                    //if (iniValue != "") { m_fTilt = Convert.ToDouble(iniValue); }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public bool LoadRecipeINI(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    IniFile ini = new IniFile();
                    String iniValue = "";
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPIMGPATH, path);
                    if (iniValue != "") { m_strTemplateImagePath = iniValue; }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIX, path);
                    if (iniValue != "") { m_rectTemplateROI.X = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIY, path);
                    if (iniValue != "") { m_rectTemplateROI.Y = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIW, path);
                    if (iniValue != "") { m_rectTemplateROI.Width = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPROIH, path);
                    if (iniValue != "") { m_rectTemplateROI.Height = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPOFFSX, path);
                    if (iniValue != "") { m_ptTartgetOffsetPosition.X = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TEMPOFFSY, path);
                    if (iniValue != "") { m_ptTartgetOffsetPosition.Y = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_SOCRETHRESHOLD, path);
                    if (iniValue != "") { m_dScoreThreshold = Convert.ToDouble(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPX, path);
                    if (iniValue != "") { m_rtInspRect.X = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPY, path);
                    if (iniValue != "") { m_rtInspRect.Y = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPW, path);
                    if (iniValue != "") { m_rtInspRect.Width = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_INSPH, path);
                    if (iniValue != "") { m_rtInspRect.Height = Convert.ToInt32(iniValue); }
                    //iniValue = ini.IniReadValue(VISIONRCP_TEMPLATE_SECTION, VISIONRCP_TEMPLATE_KEY_TILT, path);
                    //if (iniValue != "") { m_fTilt = Convert.ToDouble(iniValue); }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        IplImage bin;
        IplImage canny;
        IplImage houline;

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, 120, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage CannyEdge(IplImage src)
        {
            canny = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.Canny(src, canny, 50, 100);
            return canny;
        }

        public IplImage HoughLines(IplImage src)
        {
            houline = new IplImage(src.Size, BitDepth.U8, 3);
            canny = new IplImage(src.Size, BitDepth.U8, 1);

            canny = this.CannyEdge(this.Binary(src));
            Cv.CvtColor(canny, houline, ColorConversion.GrayToBgr);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq lines = canny.HoughLines2(Storage, HoughLinesMethod.Standard, 1, Math.PI / 180, 50, 0, 0);

            for (int i = 0; i < Math.Min(lines.Total, 3); i++)
            {
                CvLineSegmentPolar element = lines.GetSeqElem<CvLineSegmentPolar>(i).Value;

                double r = element.Rho;
                double theta = element.Theta;

                m_fResultTilt = theta * 180 / Math.PI;
                if (m_fResultTilt >= 90) m_fResultTilt -= 90;
                if (m_fResultTilt >= 180) m_fResultTilt -= 180;
                if (m_fResultTilt >= 270) m_fResultTilt -= 270;
                if (m_fResultTilt >= 360) m_fResultTilt -= 360;

                if (m_fResultTilt > 45) m_fResultTilt = Math.Abs(m_fResultTilt - 90); 

                double a = Math.Cos(theta);
                double b = Math.Sin(theta);
                double x0 = r * a;
                double y0 = r * b;
                int scale = src.Size.Width + src.Size.Height;

                CvPoint pt1 = new CvPoint(Convert.ToInt32(x0 - scale * b), Convert.ToInt32(y0 + scale * a));
                CvPoint pt2 = new CvPoint(Convert.ToInt32(x0 + scale * b), Convert.ToInt32(y0 - scale * a));

                houline.Circle(new CvPoint((int)x0, (int)y0), 5, CvColor.Yellow, -1);
                houline.Line(pt1, pt2, CvColor.Red, 1, LineType.AntiAlias);
            }
            return houline;
        }

        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (canny != null) Cv.ReleaseImage(canny);
            if (houline != null) Cv.ReleaseImage(houline);
        }


    }
}
