using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.LaserVision.OpenCV
{
    public class ProcessFitCircle
    {
        OpenCVData m_parent = null;

        PointDouble[] _searchDirections;
        CircleInfo m_Circle;
        OverlayUnitParameter _param = new OverlayUnitParameter();

        OpenCvSharp.CPlusPlus.Mat m_image; // 원본 이미지
        OpenCvSharp.CPlusPlus.Mat m_inInspImage;  // 검사 이미지

        public CircleInfo GetCircleInfo()
        {
            return m_Circle;
        }
        public void SetCircleInfo(CircleInfo cirInfo)
        {
            m_Circle = cirInfo;
        }

        public OverlayUnitParameter GetOverayUnitParameter()
        {
            return _param;
        }
        public void SetOverlayUnitParameter(OverlayUnitParameter overUnitPara)
        {
            _param = overUnitPara;
        }

        public ProcessFitCircle(OpenCVData parent)
        {
            m_parent = parent;

            m_Circle = new CircleInfo();
            MakeSearchDirection();
        }

        private void MakeSearchDirection()
        {
            _searchDirections = new PointDouble[Define.OverlayRays];

            double radian = 0;
            double gap = Define.OverlayInspectionAngle * Math.PI / 180;

            for (int i = 0; i < Define.OverlayRays; i++)
            {
                _searchDirections[i] = new PointDouble();
                _searchDirections[i].X = Math.Cos(radian);
                _searchDirections[i].Y = Math.Sin(radian);

                radian += gap;
            }
        }

        public void Inspect()
        {
            try
            {
                IplImage procImg = m_parent.GetIPLImageProcessing();

                if (procImg == null)
                    return;

                m_Circle.CenterPoint = new System.Drawing.Point(m_parent.GetResultPoint().X, m_parent.GetResultPoint().Y);

                m_image = Cv2.CvArrToMat(procImg, true);
                MakeInspectionImage();

                FindCircle();              // edge point 획득
                ScoreCircularity();        // Circularity 측정
                FitCircle();               // 중간값에서 크게 벗어나는 값을 제외하고 fitting
                
                ReleaseInspectionImage();

                m_parent.GetListResultPoints().Clear();
                m_parent.SetResultPoint(new CvPoint(m_Circle.CenterPoint.X, m_Circle.CenterPoint.Y));
                if (m_Circle.PerimeterPoints.Length > 0)
                {
                    for (int i = 0; i < m_Circle.PerimeterPoints.Length; i++)
                    {
                        m_parent.GetListResultPoints().Add(new CvPoint(m_Circle.PerimeterPoints[i].X, m_Circle.PerimeterPoints[i].Y));
                    }
                }
            }
            catch (Exception E)
            {                
                throw E;
            }

        }

        // 사용자가 선택한 컬러 채널로 이미지 만듦
        private void MakeInspectionImage()
        {
            OpenCvSharp.CPlusPlus.Mat inTmp;

            // 검사 이미지
            inTmp = ProcessUtils.RGBTo(m_image, _param.TargetColor);
            m_inInspImage = inTmp.MedianBlur(_param.MedianFilterAperture);
            inTmp.Dispose();
        }

        private void FitCircle()
        {
            ProcessUtils.FitCircle(_param, m_Circle);
        }

        private void ScoreCircularity()
        {
            ProcessUtils.FitEllipse(m_Circle);            
        }

        private void FindCircle()
        {
            FindEdgePoint(m_inInspImage, _param, m_Circle);
        }

        private void FindEdgePoint(OpenCvSharp.CPlusPlus.Mat image, OverlayUnitParameter unitParam, CircleInfo circleInfo)
        {
            if (circleInfo.CenterPoint.X <= 0 || circleInfo.CenterPoint.Y <= 0)
                return;

            PointDouble startPoint = new PointDouble();

            for (int i = 0; i < Define.OverlayRays; i++)
            {
                startPoint.X = circleInfo.CenterPoint.X + unitParam.SearchStart * _searchDirections[i].X;
                startPoint.Y = circleInfo.CenterPoint.Y + unitParam.SearchStart * _searchDirections[i].Y;
                CheckAgain(startPoint);

                ProcessUtils.GetEdgePoint(
                    image,
                    startPoint,
                    _searchDirections[i],
                    unitParam,
                    out circleInfo.PerimeterPoints[i]);
            }
        }

        private void CheckAgain(PointDouble p)
        {
            if (p.X < 0) p.X = 0;
            if (p.Y < 0) p.Y = 0;
            if (p.X + 0.5 >= m_image.Width) p.X = m_image.Width - 1;
            if (p.Y + 0.5 >= m_image.Height) p.Y = m_image.Height - 1;
        }

        private void ReleaseInspectionImage()
        {
            if (m_image != null)
            {
                m_image.Dispose();
                m_image = null;
            }                

            if (m_inInspImage != null)
            {
                m_inInspImage.Dispose();
                m_inInspImage = null;
            }                
        }

        public void SaveRecipeINI(string path, IniFile ini)
        {
            try
            {
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_ENABLE, 
                                    _param.OperationEnabled.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_ABSGVENABLE,
                                    _param.AbsoluteGVEnabled.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHDIR,
                                    Convert.ToInt32(_param.SearchDirection).ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_CIRFITENABLE,
                                    _param.CircleFittingEnabled.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_TARGETCOLOR,
                                    Convert.ToInt32(_param.TargetColor).ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHSTART,
                                    _param.SearchStart.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHLENGTH,
                                    _param.SearchLength.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHGV,
                                    _param.SearchGV.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_THRESHOLD,
                                    _param.Threshold.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_MEDIANFILTERAPERTURE,
                                    _param.MedianFilterAperture.ToString(), path);

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
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_ENABLE,
                                    _param.OperationEnabled.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_ABSGVENABLE,
                                    _param.AbsoluteGVEnabled.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHDIR,
                                    Convert.ToInt32(_param.SearchDirection).ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_CIRFITENABLE,
                                    _param.CircleFittingEnabled.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_TARGETCOLOR,
                                    Convert.ToInt32(_param.TargetColor).ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHSTART,
                                    _param.SearchStart.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHLENGTH,
                                    _param.SearchLength.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHGV,
                                    _param.SearchGV.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_THRESHOLD,
                                    _param.Threshold.ToString(), path);
                ini.IniWriteValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_MEDIANFILTERAPERTURE,
                                    _param.MedianFilterAperture.ToString(), path);
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
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_ENABLE, path);
                    if (iniValue != "") { _param.OperationEnabled = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_ABSGVENABLE, path);
                    if (iniValue != "") { _param.AbsoluteGVEnabled = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHDIR, path);
                    if (iniValue != "") { _param.SearchDirection = (OverlaySearchDirection)Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_CIRFITENABLE, path);
                    if (iniValue != "") { _param.CircleFittingEnabled = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_TARGETCOLOR, path);
                    if (iniValue != "") { _param.TargetColor = (ColorType)Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHSTART, path);
                    if (iniValue != "") { _param.SearchStart = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHLENGTH, path);
                    if (iniValue != "") { _param.SearchLength = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHGV, path);
                    if (iniValue != "") { _param.SearchGV = Convert.ToByte(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_THRESHOLD, path);
                    if (iniValue != "") { _param.Threshold = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_MEDIANFILTERAPERTURE, path);
                    if (iniValue != "") { _param.MedianFilterAperture = Convert.ToInt32(iniValue); }

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
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_ENABLE, path);
                    if (iniValue != "") { _param.OperationEnabled = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_ABSGVENABLE, path);
                    if (iniValue != "") { _param.AbsoluteGVEnabled = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHDIR, path);
                    if (iniValue != "") { _param.SearchDirection = (OverlaySearchDirection)Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_CIRFITENABLE, path);
                    if (iniValue != "") { _param.CircleFittingEnabled = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_TARGETCOLOR, path);
                    if (iniValue != "") { _param.TargetColor = (ColorType)Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHSTART, path);
                    if (iniValue != "") { _param.SearchStart = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHLENGTH, path);
                    if (iniValue != "") { _param.SearchLength = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_SEARCHGV, path);
                    if (iniValue != "") { _param.SearchGV = Convert.ToByte(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_THRESHOLD, path);
                    if (iniValue != "") { _param.Threshold = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(OverlayUnitParameter.VISIONRCP_FITCIRCLE_SECTION, OverlayUnitParameter.VISIONRCP_FITCIRCLE_KEY_MEDIANFILTERAPERTURE, path);
                    if (iniValue != "") { _param.MedianFilterAperture = Convert.ToInt32(iniValue); }

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

    public class PointDouble
    {
        public double X;
        public double Y;

        public PointDouble(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public static PointDouble operator -(PointDouble p1, PointDouble p2)
        {
            return new PointDouble(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static PointDouble operator +(PointDouble p1, PointDouble p2)
        {
            return new PointDouble(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static PointDouble operator *(PointDouble p1, PointDouble p2)
        {
            return new PointDouble(p1.X * p2.X, p1.Y * p2.Y);
        }

        public static PointDouble operator /(PointDouble p1, PointDouble p2)
        {
            double x;
            double y;

            if (p2.X == 0) { x = 0.0f; }
            else { x = p1.X / p2.X; }

            if (p2.Y == 0) { y = 0.0f; }
            else { y = p1.Y / p2.Y; }

            return new PointDouble(x, y);
        }

        public static int CompareX(PointDouble v1, PointDouble v2)
        {
            return v1.X.CompareTo(v2.X);
        }
        public static int CompareY(PointDouble v1, PointDouble v2)
        {
            return v1.Y.CompareTo(v2.Y);
        }
    }

    public class CircleInfo
    {
        public double Score;
        public double MinDiameter;
        public double MaxDiameter;
        public double AvgDiameter;
        public double Radius;
        public System.Drawing.Point[] PerimeterPoints;
        public System.Drawing.Point CenterPoint;
        public PointDouble CP;

        public CircleInfo()
        {
            Score = 0.0f;
            MinDiameter = 0.0f;
            MaxDiameter = 0.0f;
            AvgDiameter = 0.0f;
            Radius = 0;
            CenterPoint = new System.Drawing.Point(-1, -1);
            CP = new PointDouble(-1, -1);
            PerimeterPoints = new System.Drawing.Point[Define.OverlayRays];
            for (int i = 0; i < Define.OverlayRays; i++)
            {
                PerimeterPoints[i] = new System.Drawing.Point(-1, -1);
            }
        }
        public void CopyFrom(CircleInfo circle)
        {
            MinDiameter = circle.MinDiameter;
            MaxDiameter = circle.MaxDiameter;
            AvgDiameter = circle.AvgDiameter;
            Radius = circle.Radius;
            circle.PerimeterPoints.CopyTo(PerimeterPoints, 0);
            CenterPoint = circle.CenterPoint;
            CP = circle.CP;
            Score = circle.Score;
        }
    }

    public enum InspectionTarget { Overlay, LineAndSpace, Nothing };
    public enum OverlaySearchDirection { InToOut, OutToIn };
    public enum LASSearchDirection { Width, Height, Tilt };
    public enum DrawMode { Draw, Select, Template };
    public enum ColorType { Gray, Red, Green, Blue, Hue, Saturation, Value };
    public enum EnableType { True, False };

    public static class Define
    {
        // Overlay
        public const int OverlayParamNum = 3;
        public const double OverlayInspectionAngle = 1;
        public const int OverlayRays = (int)(360 / OverlayInspectionAngle);

        // Line And Space
        public const int LineAndSpaceParamNum = 2;

        public const string BitmapFilter = "bmp files(*.bmp)|*.bmp";
        public const string BitmapInitialDirectory = @"C:\Users\Soldat\Desktop\target";
    }

    // Recipe Data
    public class OverlayUnitParameter
    {
        public const string VISIONRCP_FITCIRCLE_SECTION = "Vision FitCircle Recipe";
        public const string VISIONRCP_FITCIRCLE_KEY_ENABLE = "OPERATION_ENABLE";
        public const string VISIONRCP_FITCIRCLE_KEY_ABSGVENABLE = "ABSOLUTEGV_ENABLE";
        public const string VISIONRCP_FITCIRCLE_KEY_SEARCHDIR = "SEARCH_DIRECTION";
        public const string VISIONRCP_FITCIRCLE_KEY_CIRFITENABLE = "CIRCLE_FIT_ENABLE";
        public const string VISIONRCP_FITCIRCLE_KEY_TARGETCOLOR = "TARGET_COLOR";
        public const string VISIONRCP_FITCIRCLE_KEY_SEARCHSTART = "SEARCH_START";
        public const string VISIONRCP_FITCIRCLE_KEY_SEARCHLENGTH = "SEARCH_LENGTH";
        public const string VISIONRCP_FITCIRCLE_KEY_SEARCHGV = "SEARCH_GV";
        public const string VISIONRCP_FITCIRCLE_KEY_THRESHOLD = "THRESHOLD";
        public const string VISIONRCP_FITCIRCLE_KEY_MEDIANFILTERAPERTURE = "MEDIAN_FILTER_APERTURE";

        // Options
        public bool OperationEnabled;
        public bool AbsoluteGVEnabled;
        public OverlaySearchDirection SearchDirection;
        public bool CircleFittingEnabled;
        public ColorType TargetColor;

        // Parameters
        public int SearchStart;
        public int SearchLength;
        public byte SearchGV;
        public double Threshold;
        public int MedianFilterAperture;
        

        public OverlayUnitParameter()
        {
            // Options
            OperationEnabled = true;
            AbsoluteGVEnabled = false;
            SearchDirection = OverlaySearchDirection.InToOut;
            CircleFittingEnabled = false;
            TargetColor = ColorType.Gray;

            // Parameters
            SearchStart = 15;
            SearchLength = 50;
            SearchGV = 50;
            Threshold = 10;
            MedianFilterAperture = 7;
        }
    }
}
