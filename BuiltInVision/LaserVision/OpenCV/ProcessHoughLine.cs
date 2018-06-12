using GalvoScanner;
using GalvoScanner.LaserVision.OpenCV;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltInVision.LaserVision.OpenCV
{
    public class ProcessHoughLine
    {
        public const string VISIONRCP_HOUGHLINE_SECTION = "Vision HoughLine Recipe";
        public const string VISIONRCP_HOUGHLINE_KEY_TILT = "HOUGHLINE_TILT";
        public const string VISIONRCP_HOUGHLINE_KEY_TILTRANGE = "HOUGHLINE_TILTRANGE";
        public const string VISIONRCP_HOUGHLINE_KEY_BINTHRESOLD = "HOUGHLINE_BINTHRESOLD";
        public const string VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD1 = "HOUGHLINE_CANNYTHRESOLD1";
        public const string VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD2 = "HOUGHLINE_CANNYTHRESOLD2";
        public const string VISIONRCP_HOUGHLINE_KEY_HOUGHTHRESOLD = "HOUGHLINE_HOUGHTHRESOLD";
        public const string VISIONRCP_HOUGHLINE_KEY_INRESULTROI = "HOUGHLINE_INRESULTROI";

        private double m_fResultTilt = 0;

        private double m_fTilt = 1;
        public double Tilt
        {
            get { return m_fTilt; }
            set { m_fTilt = value; }
        }

        private double m_dTiltRange = 1;
        public double TiltRange
        {
            get { return m_dTiltRange; }
            set { m_dTiltRange = value; }
        }

        private int m_dBinThreshold = 120;
        public int BinaryThreshold
        {
            get { return m_dBinThreshold; }
            set { m_dBinThreshold = value; }
        }

        private int m_dCanThreshold1 = 50;
        public int CannyThreshold1
        {
            get { return m_dCanThreshold1; }
            set { m_dCanThreshold1 = value; }
        }

        private int m_dCanThreshold2 = 50;
        public int CannyThreshold2
        {
            get { return m_dCanThreshold2; }
            set { m_dCanThreshold2 = value; }
        }

        private int m_dHoughThreshold = 50;
        public int HoughLineThreshold
        {
            get { return m_dHoughThreshold; }
            set { m_dHoughThreshold = value; }
        }

        private bool m_bResultInROI = false;
        public bool ResultInROI
        {
            get { return m_bResultInROI; }
            set { m_bResultInROI = value; }
        }

        public enum houghResultType { None = 0, OK, NoProcessImage, Tilt };

        private houghResultType m_executeHoughResult = houghResultType.None;
        public houghResultType ExcuteMatchResult
        {
            get { return m_executeHoughResult; }
        }

        IplImage bin;
        IplImage canny;
        IplImage houline;
        public IplImage GetIPLImageHoughResult()
        {
            return houline;
        }

        OpenCVData m_parent = null;
        public ProcessHoughLine(OpenCVData parent)
        {
            m_parent = parent;
        }

        ~ProcessHoughLine()
        {
            if (bin != null)
            {
                bin.Dispose();
            }

            if (canny != null)
            {
                canny.Dispose();
            }

            if (houline != null)
            {
                houline.Dispose();
            }
        }

        private IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, m_dBinThreshold, 255, ThresholdType.Binary);
            return bin;
        }

        private IplImage CannyEdge(IplImage src)
        {
            canny = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.Canny(src, canny, m_dCanThreshold1, m_dCanThreshold2);
            return canny;
        }

        private IplImage HoughLines(IplImage src)
        {
            bool isApplyRoi = false;
            IplImage roiSrc = null;
            CvRect roiRect = m_parent.GetResultRect();
            if (m_bResultInROI)
            {                
                if (roiRect.X != -1 && roiRect.Y != -1)
                {
                    roiSrc = src.Clone(roiRect);
                    isApplyRoi = true;
                }                
            }

            if (roiSrc == null)
            {
                roiSrc = src;
            }

            houline = new IplImage(roiSrc.Size, BitDepth.U8, 3);
            canny = new IplImage(roiSrc.Size, BitDepth.U8, 1);

            canny = this.CannyEdge(this.Binary(roiSrc));
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

                if (isApplyRoi)
                {
                    pt1.X += roiRect.X; pt1.Y += roiRect.Y;
                    pt2.X += roiRect.X; pt2.Y += roiRect.Y;
                }

                CvLine2D line = new CvLine2D(pt1.X, pt1.Y, pt2.X, pt2.Y);
                m_parent.GetListResultLines().Add(line);
            }

            return houline;
        }

        public houghResultType ExecuteHoughLines()
        {
            if (m_parent.GetIPLImageProcessing() == null)
            {
                m_executeHoughResult = houghResultType.NoProcessImage;
                return m_executeHoughResult;
            }

            HoughLines(m_parent.GetIPLImageProcessing());

            m_parent.StringResult.AppendLine("<Hough line>");
            m_parent.StringResult.Append("Tilt : "); m_parent.StringResult.AppendLine(m_fResultTilt.ToString("0.##"));

            if (m_fResultTilt > m_fTilt)
            {
                m_executeHoughResult = houghResultType.Tilt;
            }
            else
            {
                m_executeHoughResult = houghResultType.OK;
            }

            return m_executeHoughResult;
        }

        public void SaveRecipeINI(string path, IniFile ini)
        {
            try
            {
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_TILT, m_fTilt.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_TILTRANGE, m_dTiltRange.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_BINTHRESOLD, m_dBinThreshold.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD1, m_dCanThreshold1.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD2, m_dCanThreshold2.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_HOUGHTHRESOLD, m_dHoughThreshold.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_INRESULTROI, m_bResultInROI.ToString(), path);
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
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_TILT, m_fTilt.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_TILTRANGE, m_dTiltRange.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_BINTHRESOLD, m_dBinThreshold.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD1, m_dCanThreshold1.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD2, m_dCanThreshold2.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_HOUGHTHRESOLD, m_dHoughThreshold.ToString(), path);
                ini.IniWriteValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_INRESULTROI, m_bResultInROI.ToString(), path);
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
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_TILT, path);
                    if (iniValue != "") { m_fTilt = Convert.ToDouble(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_TILTRANGE, path);
                    if (iniValue != "") { m_dTiltRange = Convert.ToDouble(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_BINTHRESOLD, path);
                    if (iniValue != "") { m_dBinThreshold = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD1, path);
                    if (iniValue != "") { m_dCanThreshold1 = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD2, path);
                    if (iniValue != "") { m_dCanThreshold2 = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_HOUGHTHRESOLD, path);
                    if (iniValue != "") { m_dHoughThreshold = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_INRESULTROI, path);
                    if (iniValue != "") { m_bResultInROI = Convert.ToBoolean(iniValue); }

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
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_TILT, path);
                    if (iniValue != "") { m_fTilt = Convert.ToDouble(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_TILTRANGE, path);
                    if (iniValue != "") { m_dTiltRange = Convert.ToDouble(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_BINTHRESOLD, path);
                    if (iniValue != "") { m_dBinThreshold = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD1, path);
                    if (iniValue != "") { m_dCanThreshold1 = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_CANNYTHRESOLD2, path);
                    if (iniValue != "") { m_dCanThreshold2 = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_HOUGHTHRESOLD, path);
                    if (iniValue != "") { m_dHoughThreshold = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_HOUGHLINE_SECTION, VISIONRCP_HOUGHLINE_KEY_INRESULTROI, path);
                    if (iniValue != "") { m_bResultInROI = Convert.ToBoolean(iniValue); }

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
    }
}
