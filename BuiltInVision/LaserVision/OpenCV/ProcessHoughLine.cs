using GalvoScanner.LaserVision.OpenCV;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltInVision.LaserVision.OpenCV
{
    public class ProcessHoughLine
    {
        public const string VISIONRCP_HOUGHLINE_SECTION = "Vision HoughLine Recipe";

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

        private double m_dBinThreshold = 120;
        public double BinaryThreshold
        {
            get { return m_dBinThreshold; }
            set { m_dBinThreshold = value; }
        }

        private double m_dCanThreshold1 = 50;
        public double CannyThreshold1
        {
            get { return m_dCanThreshold1; }
            set { m_dCanThreshold1 = value; }
        }

        private double m_dCanThreshold2 = 50;
        public double CannyThreshold2
        {
            get { return m_dCanThreshold2; }
            set { m_dCanThreshold2 = value; }
        }

        private double m_dHoughThreshold = 50;
        public double HoughLineThreshold
        {
            get { return m_dHoughThreshold; }
            set { m_dHoughThreshold = value; }
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
    }
}
