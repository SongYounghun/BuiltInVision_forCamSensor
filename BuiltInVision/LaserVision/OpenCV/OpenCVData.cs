using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using BuiltInVision.LaserVision.OpenCV;

namespace GalvoScanner.LaserVision.OpenCV
{
    public class OpenCVData
    {
        private static OpenCVData m_cvData = null;
        public static OpenCVData GetInstance()
        {
            if (m_cvData == null)
            {
                m_cvData = new OpenCVData();
            }

            return m_cvData;
        }

        public enum modeType { BuiltInVision = 0, GeneralArea, LineScan, AreaTile };
        public enum processType { GrabToProcess = 0, ProcessToResult, Threshold, TemplateMatch, FitCircle, HoughLine };

        public const string VISIONS_SECTION = "Vision";
        public const string VISIONS_KEY_CAMNUM = "CameraNumber";
        public const string VISIONS_KEY_USE = "UseVision";

        public const string VISIONSETTING_SECTION = "Vision Setting";
        public const string VISIONSETTING_KEY_CAMFOVW = "CamFOV_Width";
        public const string VISIONSETTING_KEY_CAMFOVH = "CamFOV_Height";
        public const string VISIONSETTING_KEY_CAMPIXW = "CamPixel_Width";
        public const string VISIONSETTING_KEY_CAMPIXH = "CamPixel_Height";
        public const string VISIONSETTING_KEY_CAMANGLE = "CamAngle";
        public const string VISIONSETTING_KEY_CAPBRIGHTNESS = "Capture_Brightness";
        public const string VISIONSETTING_KEY_CAPEXPOSURE = "Capture_Exposure";
        public const string VISIONSETTING_KEY_CAPGAIN = "Capture_Gain";
        public const string VISIONSETTING_KEY_CAPCONTRAST = "Capture_Contrast";
        public const string VISIONSETTING_KEY_CAPFPS = "Capture_FPS";
        public const string VISIONSETTING_KEY_MODETYPE = "VisionMode";
        public const string VISIONSETTING_KEY_AXISDISTX = "AxisDistanceX";
        public const string VISIONSETTING_KEY_AXISDISTY = "AxisDistanceY";
        public const string VISIONSETTING_KEY_FLIPX = "FlipX";
        public const string VISIONSETTING_KEY_FLIPY = "FlipY";
        public const string VISIONSETTING_KEY_TILEDARRY_X = "TILED_X";
        public const string VISIONSETTING_KEY_TILEDARRY_Y = "TILED_Y";
        public const string VISIONSETTING_KEY_FOVFORTILE_W = "FOVforTile_Width";
        public const string VISIONSETTING_KEY_FOVFORTILE_H = "FOVforTile_Height";
        public const string VISIONSETTING_KEY_AXISXNUM = "AXISNUMX_forTile";
        public const string VISIONSETTING_KEY_AXISYNUM = "AXISNUMY_forTile";
        public const string VISIONSETTING_KEY_AXISXNAGATIVE = "AXISNAGATIVEX_forTile";
        public const string VISIONSETTING_KEY_AXISYNAGATIVE = "AXISNAGATIVEY_forTile";
        public const string VISIONSETTING_KEY_CAMERATILT = "Camera_Tilt";

        public const string VISIONRCP_SECTION = "Vision Recipe";
        public const string VISIONRCP_KEY_LISTPROCESS = "PROCESS_LIST";
        public const string VISIONRCP_KEY_TARGETMODE = "TARGET_MODE";
        public const string VISIONRCP_KEY_TARGETOBJINDEX = "TARGET_OBJECT_INDEX";
        public const string VISIONRCP_KEY_TARGETGROUPINDEX = "TARGET_GROUP_INDEX";
        public const string VISIONRCP_KEY_USETARGETOBJINDEX = "TARGET_GROUP_INDEX_USE";

        CvCapture m_cvCapture;

        // ------------- 이미지 처리를 위한 Ipl이미지 ----------------//
        #region IplImage for image process
        private IplImage m_pImageFromCam;
        public IplImage GetIPLImage()
        {
            return m_pImageFromCam;
        }
        public void SetImageFromCam(IplImage image)
        {
            m_pImageFromCam = image;
        }

        private IplImage m_pImageProcessing;
        public IplImage GetIPLImageProcessing()
        {
            return m_pImageProcessing;
        }
        public void SetIPLImageProcessing(IplImage image)
        {
            m_pImageProcessing = image;
        }

        private IplImage m_pImageResult;
        public IplImage GetIPLImageResult()
        {
            return m_pImageResult;
        }
        public void SetIPLImageResult(IplImage image)
        {
            m_pImageResult = image;
        } 
        #endregion
        // ------------------------------------------------------------ //

        // ------------- Camera 관련 세팅 및 초기화 ----------------//
        #region Camera setting
        private int m_nCameraNum = -1;
        public int GetCameraNum()
        {
            return m_nCameraNum;
        }
        public void SetCameraNum(int num)
        {
            if (num < 0)
                return;
            m_nCameraNum = num;
            if (m_cvCapture != null)
            {
                m_cvCapture.Dispose();
                m_cvCapture = null;
            }
            m_cvCapture = CvCapture.FromCamera(CaptureDevice.Any, m_nCameraNum);
        }

        private bool m_bUseVision = false;
        public bool GetUseVision()
        {
            return m_bUseVision;
        }
        public void SetUseVision(bool use)
        {
            m_bUseVision = use;
        }

        private double m_fCamFOV_W = 30.0;
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Camera FOV width (mm)")]
        public double CamFOV_Width
        {
            get { return m_fCamFOV_W; }
            set { m_fCamFOV_W = value; }
        }
        private double m_fCamFOV_H = 30.0;
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Camera FOV height (mm)")]
        public double CamFOV_Height
        {
            get { return m_fCamFOV_H; }
            set { m_fCamFOV_H = value; }
        }
        private int m_nCamPix_W = 1000;
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Camera pixel width (pixel)")]
        public int CamPixel_Width
        {
            get { return m_nCamPix_W; }
            set { m_nCamPix_W = value; }
        }
        private int m_nCamPix_H = 1000;
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Camera pixel height (pixel)")]
        public int CamPixel_Height
        {
            get { return m_nCamPix_H; }
            set { m_nCamPix_H = value; }
        }
        private double m_fCaptBrightness = 50;
        [Browsable(false)]
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Capture brightness")]
        public double Capture_Brightness
        {
            get { return m_fCaptBrightness; }
            set { m_fCaptBrightness = value; }
        }
        private double m_fCaptExposure = 1000;
        [Browsable(false)]
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Capture exposure")]
        public double Capture_Exposure
        {
            get { return m_fCaptExposure; }
            set { m_fCaptExposure = value; }
        }
        private double m_fCaptGain = 1;
        [Browsable(false)]
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Capture gain")]
        public double Capture_Gain
        {
            get { return m_fCaptGain; }
            set { m_fCaptGain = value; }
        }
        private double m_fCaptContrast = 50;
        [Browsable(false)]
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Capture contrast")]
        public double Capture_Contrast
        {
            get { return m_fCaptContrast; }
            set { m_fCaptContrast = value; }
        }
        private double m_fCaptFPS = 50;
        [Browsable(false)]
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Capture FPS")]
        public double Capture_FPS
        {
            get { return m_fCaptFPS; }
            set { m_fCaptFPS = value; }
        }
        private modeType m_visionModeType = modeType.BuiltInVision;
        [Browsable(false)]
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Vision mode")]
        public modeType VisionMode
        {
            get { return m_visionModeType; }
            set { m_visionModeType = value; }
        }

        private bool m_bFlipX = false;
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Flip X")]
        public bool FlipX
        {
            get { return m_bFlipX; }
            set { m_bFlipX = value; }
        }

        private bool m_bFlipY = false;
        [CategoryAttribute("Vision Setting"), DescriptionAttribute("Flip Y")]
        public bool FlipY
        {
            get { return m_bFlipY; }
            set { m_bFlipY = value; }
        }

        private Point m_ptTileArray = new Point(1, 1);
        [Browsable(false)]
        [CategoryAttribute("Tile function"), DescriptionAttribute("Tile array for area tile mode")]
        public Point TileArray
        {
            get { return m_ptTileArray; }
            set { m_ptTileArray = value; }
        }

        private double m_dFOVforTile_W = 30.0;
        [Browsable(false)]
        [CategoryAttribute("Tile function"), DescriptionAttribute("FOV width for area tile mode")]
        public double FOVforTile_W
        {
            get { return m_dFOVforTile_W; }
            set { m_dFOVforTile_W = value; }
        }

        private double m_dFOVforTile_H = 30.0;
        [Browsable(false)]
        [CategoryAttribute("Tile function"), DescriptionAttribute("FOV height for area tile mode")]
        public double FOVforTile_H
        {
            get { return m_dFOVforTile_H; }
            set { m_dFOVforTile_H = value; }
        }

        private int m_nAxisXnum = 0;
        [Browsable(false)]
        [CategoryAttribute("Tile function"), DescriptionAttribute("X Axis number for area tile mode")]
        public int AxisNumberX
        {
            get { return m_nAxisXnum; }
            set { m_nAxisXnum = value; }
        }

        private int m_nAxisYnum = 1;
        [Browsable(false)]
        [CategoryAttribute("Tile function"), DescriptionAttribute("Y Axis number for area tile mode")]
        public int AxisNumberY
        {
            get { return m_nAxisYnum; }
            set { m_nAxisYnum = value; }
        }

        private bool m_bXAxisNagative = false;
        [Browsable(false)]
        [CategoryAttribute("Tile function"), DescriptionAttribute("X Axis nagative for area tile mode")]
        public bool AxisNagativeX
        {
            get { return m_bXAxisNagative; }
            set { m_bXAxisNagative = value; }
        }

        private bool m_bYAxisNagative = false;
        [Browsable(false)]
        [CategoryAttribute("Tile function"), DescriptionAttribute("Y Axis nagative for area tile mode")]
        public bool AxisNagativeY
        {
            get { return m_bYAxisNagative; }
            set { m_bYAxisNagative = value; }
        }

        private double m_dCameraTilt = 0;
        [Browsable(false)]
        [CategoryAttribute("Tile function"), DescriptionAttribute("CameraTilt")]
        public double CameraTilt
        {
            get { return m_dCameraTilt; }
            set { m_dCameraTilt = value; }
        }

        private int m_nLoadImgWidth = 1000;
        public int GetLoadImgWidth()
        {
            return m_nLoadImgWidth;
        }
        public void SetLoadImgWidth(int width)
        {
            m_nLoadImgWidth = width;
        }

        private int m_nLoadImgHeight = 1000;
        public int GetLoadImgHeight()
        {
            return m_nLoadImgHeight;
        }
        public void SetLoadImgHeight(int height)
        {
            m_nLoadImgHeight = height;
        }
        #endregion
        // ------------------------------------------------------------ //

        // ------------- Process class ----------------//
        #region Process class
        private ProcessTemplateMatch m_processTempMatch = null;
        public ProcessTemplateMatch GetProcessTeplateMatch()
        {
            return m_processTempMatch;
        }

        private ProcessCalFindCenter m_processCalFindCenter = null;
        public ProcessCalFindCenter GetProcessCalFindCenter()
        {
            return m_processCalFindCenter;
        }

        private ProcessThreshold m_processThreshold = null;
        public ProcessThreshold GetProcessThreshold()
        {
            return m_processThreshold;
        }

        private ProcessFitCircle m_processFitCircle = null;
        public ProcessFitCircle GetProcessFitCircle()
        {
            return m_processFitCircle;
        }

        private ProcessHoughLine m_processHoughLine = null;
        public ProcessHoughLine GetProcessHoughLine()
        {
            return m_processHoughLine;
        }
        #endregion
        // ------------------------------------------------------------ //

        // ------------- 이미지 처리 후 포지션에 대한 결과 관련 값 ----------------//
        #region Result value after image process
        private CvPoint m_ptResultPoint = new CvPoint(-1, -1);
        public CvPoint GetResultPoint()
        {
            return m_ptResultPoint;
        }
        public void SetResultPoint(CvPoint pt)
        {
            m_ptResultPoint = pt;
        }

        public CvPoint2D32f GetResultPosition()
        {
            return ConvertMMPosition(GetResultPoint());
        }

        private CvRect m_rtResultRect = new CvRect(-1, -1, 0, 0);
        public CvRect GetResultRect()
        {
            return m_rtResultRect;
        }
        public void SetResultRect(CvRect rt)
        {
            m_rtResultRect = rt;
        }        

        private List<CvPoint> m_listResultPoints = new List<CvPoint>();
        public List<CvPoint> GetListResultPoints()
        {
            return m_listResultPoints;
        }

        private List<CvLine2D> m_listResultLines = new List<CvLine2D>();
        public List<CvLine2D> GetListResultLines()
        {
            return m_listResultLines;
        }

        private StringBuilder m_strResult = new StringBuilder();
        public StringBuilder StringResult
        {
            get { return m_strResult; }
            set { m_strResult = value; }
        }
        #endregion
        // ------------------------------------------------------------ //

        private List<processType> m_listProcess = new List<processType>();
        public List<processType> GetListProcess()
        {
            return m_listProcess;
        }

        public enum targetMode { Object = 0, Group };
        private targetMode m_targetMode = targetMode.Object;
        public targetMode GetTargetPositionMode()
        {   
            return m_targetMode;
        }
        public void SetTargetPositionMode(targetMode tarMode)
        {   
            m_targetMode = tarMode;
        }                

        private int m_nTargetObjIndex = -1;
        public int GetTargetObjIndex()
        {
            return m_nTargetObjIndex;
        }
        public void SetTargetObjIndex(int targetObjIndex)
        {
            m_nTargetObjIndex = targetObjIndex;
        }        

        private int m_nTargetGroupIndex = -1;
        public int GetTargetGroupIndex()
        {
            return m_nTargetGroupIndex;
        }
        public void SetTargetGroupIndex(int targetGroupIndex)
        {
            m_nTargetGroupIndex = targetGroupIndex;
        }

        private bool m_bUseTargetObject = true;
        public bool IsUseTargetObject()
        {
            return m_bUseTargetObject;
        }
        public void SetUseTartgetObject(bool useTargetObject)
        {
            m_bUseTargetObject = useTargetObject;
        }
        
        private string m_strRecipePath = "";
        public string GetRecipePath()
        {
            return m_strRecipePath;
        }
        public void SetRecipePath(string path)
        {
            m_strRecipePath = path;
        }

        private bool m_bIsAutoSave = false;
        [Browsable(false)]
        public bool IsAutoSave
        {
            get { return m_bIsAutoSave; }
            set { m_bIsAutoSave = value; }
        }

        private string m_strAutoSavePath = "";
        [Browsable(false)]
        public string AutoSavePath
        {
            get { return m_strAutoSavePath; }
            set { m_strAutoSavePath = value; }
        }

        private int m_nContinuousInterval = 100;
        [Browsable(false)]
        public int ContinuousInterval
        {
            get { return m_nContinuousInterval; }
            set { m_nContinuousInterval = value; }
        }

        public OpenCVData()
        {
            m_processTempMatch = new ProcessTemplateMatch(this);
            m_processCalFindCenter = new ProcessCalFindCenter(this);
            m_processThreshold = new ProcessThreshold(this);
            m_processFitCircle = new ProcessFitCircle(this);
            m_processHoughLine = new ProcessHoughLine(this);
        }

        ~OpenCVData()
        {
            if (m_cvCapture != null)
            {
                m_cvCapture.Dispose();
                m_cvCapture = null;
            }
            if (m_pImageFromCam != null)
            {
                m_pImageFromCam.Dispose();
                m_pImageFromCam = null;
            }
            if (m_pImageProcessing != null)
            {
                m_pImageProcessing.Dispose();
                m_pImageProcessing = null;
            }
            if (m_pImageResult != null)
            {
                m_pImageResult.Dispose();
                m_pImageResult = null;
            }
        }

        public int GetCameraCount()
        {
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    CvCapture cvCapture = CvCapture.FromCamera(i);
                    if (cvCapture != null)
                    {
                        count++;
                    }
                }
                catch (Exception)
                {

                }
            }
            return count;
        }

        public IplImage GrabFromCamera()
        {
            try
            {
                if (m_nCameraNum < 0)
                    return null;

                if (m_cvCapture != null)
                {                    
                    if (m_pImageFromCam != null)
                    {
                        m_pImageFromCam.Dispose();
                        m_pImageFromCam = null;
                    }

                    if (m_cvCapture.FrameWidth > 0 && m_cvCapture.FrameHeight > 0)
                    {
                        m_pImageFromCam = m_cvCapture.QueryFrame();
                    }
                    else
                    {
                        SetCaptureProperty();
                        if (m_cvCapture.FrameWidth > 0 && m_cvCapture.FrameHeight > 0)
                        {
                            Thread.Sleep(50);
                            m_pImageFromCam = m_cvCapture.QueryFrame();
                            Thread.Sleep(50);
                            m_pImageFromCam = m_cvCapture.QueryFrame();
                        }                    
                    }                    


                    if (m_pImageFromCam != null)
                    {
                        //bool bIsCropImg = false;
                        //if (m_pImageFromCam.Width < m_nCamPix_W)
                        //{
                        //    m_nCamPix_W = m_pImageFromCam.Width;
                        //}
                        //else
                        //{
                        //    bIsCropImg = true;
                        //}

                        //if (m_pImageFromCam.Height < m_nCamPix_H)
                        //{
                        //    m_nCamPix_H = m_pImageFromCam.Height;
                        //}
                        //else
                        //{
                        //    bIsCropImg = true;
                        //}

                        //if (bIsCropImg)
                        //{
                        //    Cv.SetImageROI(m_pImageFromCam, new CvRect(0, 0, m_nCamPix_W, m_nCamPix_H));
                        //}

                        m_nLoadImgWidth = m_nCamPix_W;
                        m_nLoadImgHeight = m_nCamPix_H;

                        if (m_bFlipX && !m_bFlipY)
                        {
                            IplImage flipImg = m_pImageFromCam.Clone();
                            Cv.Flip(m_pImageFromCam, flipImg, FlipMode.X);
                            m_pImageFromCam.Dispose();
                            m_pImageFromCam = flipImg.Clone();
                        }
                        else if (!m_bFlipX && m_bFlipY)
                        {
                            IplImage flipImg = m_pImageFromCam.Clone();
                            Cv.Flip(m_pImageFromCam, flipImg, FlipMode.Y);
                            m_pImageFromCam.Dispose();
                            m_pImageFromCam = flipImg.Clone();
                        }
                        else if (m_bFlipX && m_bFlipY)
                        {
                            IplImage flipImg = m_pImageFromCam.Clone();
                            Cv.Flip(m_pImageFromCam, flipImg, FlipMode.XY);
                            m_pImageFromCam.Dispose();
                            m_pImageFromCam = flipImg.Clone();
                        }

                        return m_pImageFromCam;
                    }
                    
                }
                return null;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public void SetCaptureProperty()
        {
            try
            {
                if (m_nCameraNum < 0)
                    return;

                if (m_cvCapture != null)
                {
                    int delay = 2;
                    if (m_cvCapture.GetCaptureProperty(CaptureProperty.FrameWidth) != -1)
                    {
                        //m_cvCapture.FrameWidth = m_nCamPix_W;
                        m_cvCapture.SetCaptureProperty(CaptureProperty.FrameWidth, m_nCamPix_W);
                        Thread.Sleep(delay);
                    }                    
                    if (m_cvCapture.GetCaptureProperty(CaptureProperty.FrameHeight) != -1)
                    {
                        //m_cvCapture.FrameHeight = m_nCamPix_H;
                        m_cvCapture.SetCaptureProperty(CaptureProperty.FrameHeight, m_nCamPix_H);
                        Thread.Sleep(delay);
                    }                    
                    if (m_cvCapture.GetCaptureProperty(CaptureProperty.Brightness) != -1)
                    {
                        //m_cvCapture.Brightness = m_fCaptBrightness;
                        m_cvCapture.SetCaptureProperty(CaptureProperty.Brightness, m_fCaptBrightness);
                        Thread.Sleep(delay);
                    }
                    if (m_cvCapture.GetCaptureProperty(CaptureProperty.Exposure) != -1)
                    {
                        //m_cvCapture.Exposure = m_fCaptExposure;
                        m_cvCapture.SetCaptureProperty(CaptureProperty.Exposure, m_fCaptExposure);
                        Thread.Sleep(delay);
                    }
                    if (m_cvCapture.GetCaptureProperty(CaptureProperty.Gain) != -1)
                    {
                        //m_cvCapture.Gain = m_fCaptGain;
                        m_cvCapture.SetCaptureProperty(CaptureProperty.Gain, m_fCaptGain);
                        Thread.Sleep(delay);
                    }
                    if (m_cvCapture.GetCaptureProperty(CaptureProperty.Contrast) != -1)
                    {
                        //m_cvCapture.Contrast = m_fCaptContrast;
                        m_cvCapture.SetCaptureProperty(CaptureProperty.Contrast, m_fCaptContrast);
                        Thread.Sleep(delay);
                    }
                    if (m_cvCapture.GetCaptureProperty(CaptureProperty.Fps) != -1)
                    {
                        //m_cvCapture.Fps = m_fCaptFPS;
                        m_cvCapture.SetCaptureProperty(CaptureProperty.Fps, m_fCaptFPS);
                        Thread.Sleep(delay);
                    }
                }
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public void GetCaptureProperty()
        {
            try
            {
                if (m_nCameraNum < 0)
                    return;

                if (m_cvCapture != null)
                {
                    int delay = 2;
                    m_nCamPix_W = (int)m_cvCapture.GetCaptureProperty(CaptureProperty.FrameWidth);
                    Thread.Sleep(delay);
                    m_nCamPix_H = (int)m_cvCapture.GetCaptureProperty(CaptureProperty.FrameHeight);
                    Thread.Sleep(delay);
                    m_fCaptBrightness = m_cvCapture.GetCaptureProperty(CaptureProperty.Brightness);
                    Thread.Sleep(delay);
                    m_fCaptExposure = m_cvCapture.GetCaptureProperty(CaptureProperty.Exposure);
                    Thread.Sleep(delay);
                    m_fCaptGain = m_cvCapture.GetCaptureProperty(CaptureProperty.Gain);
                    Thread.Sleep(delay);
                    m_fCaptContrast = m_cvCapture.GetCaptureProperty(CaptureProperty.Contrast);
                    Thread.Sleep(delay);
                    m_fCaptFPS = m_cvCapture.GetCaptureProperty(CaptureProperty.Fps);
                    Thread.Sleep(delay);
                }
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public bool SaveImage(string path)
        {
            try
            {
                if (m_pImageFromCam != null)
                {
                    m_pImageFromCam.SaveImage(path);
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool LoadImage(string path)
        {
            try
            {
                if (m_pImageFromCam != null)
                {
                    m_pImageFromCam.Dispose();
                    m_pImageFromCam = null;
                }

                m_pImageFromCam = Cv.LoadImage(path);

                m_nLoadImgWidth = m_pImageFromCam.Width;
                m_nLoadImgHeight = m_pImageFromCam.Height;
                return true;                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SaveSettingINI(string path)
        {
            try
            {
                IniFile ini = new IniFile();
                ini.IniWriteValue(VISIONS_SECTION, VISIONS_KEY_CAMNUM, String.Format("{0}", m_nCameraNum), path);
                ini.IniWriteValue(VISIONS_SECTION, VISIONS_KEY_USE, String.Format("{0}", m_bUseVision), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMFOVW, String.Format("{0}", CamFOV_Width), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMFOVH, String.Format("{0}", CamFOV_Height), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMPIXW, String.Format("{0}", CamPixel_Width), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMPIXH, String.Format("{0}", CamPixel_Height), path);
                //ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMANGLE, String.Format("{0}", CamAngle), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPBRIGHTNESS, String.Format("{0}", Capture_Brightness), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPCONTRAST, String.Format("{0}", Capture_Contrast), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPEXPOSURE, String.Format("{0}", Capture_Exposure), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPFPS, String.Format("{0}", Capture_FPS), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPGAIN, String.Format("{0}", Capture_Gain), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_MODETYPE, String.Format("{0}", Convert.ToInt32(VisionMode)), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_FLIPX, String.Format("{0}", FlipX), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_FLIPY, String.Format("{0}", FlipY), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_TILEDARRY_X, String.Format("{0}", m_ptTileArray.X), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_TILEDARRY_Y, String.Format("{0}", m_ptTileArray.Y), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_FOVFORTILE_W, String.Format("{0}", m_dFOVforTile_W), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_FOVFORTILE_H, String.Format("{0}", m_dFOVforTile_H), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_AXISXNUM, String.Format("{0}", m_nAxisXnum), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_AXISYNUM, String.Format("{0}", m_nAxisYnum), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_AXISXNAGATIVE, String.Format("{0}", m_bXAxisNagative), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_AXISYNAGATIVE, String.Format("{0}", m_bYAxisNagative), path);
                ini.IniWriteValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMERATILT, String.Format("{0}", m_dCameraTilt), path);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        public bool LoadSettingINI(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    String iniValue = "";
                    IniFile ini = new IniFile();
                    iniValue = ini.IniReadValue(VISIONS_SECTION, VISIONS_KEY_CAMNUM, path);
                    if (iniValue != "") 
                    {
                        SetCameraNum(m_nCameraNum);
                        GetCaptureProperty(); 
                        m_nCameraNum = int.Parse(iniValue);
                    }
                    iniValue = ini.IniReadValue(VISIONS_SECTION, VISIONS_KEY_USE, path);
                    if (iniValue != "") { m_bUseVision = bool.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMFOVW, path);
                    if (iniValue != "") { CamFOV_Width = double.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMFOVH, path);
                    if (iniValue != "") { CamFOV_Height = double.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMPIXW, path);
                    if (iniValue != "") { CamPixel_Width = int.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMPIXH, path);
                    if (iniValue != "") { CamPixel_Height = int.Parse(iniValue); }
                    //iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMANGLE, path);
                    //if (iniValue != "") { CamAngle = int.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPBRIGHTNESS, path);
                    if (iniValue != "") { Capture_Brightness = double.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPCONTRAST, path);
                    if (iniValue != "") { Capture_Contrast = double.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPEXPOSURE, path);
                    if (iniValue != "") { Capture_Exposure = double.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPFPS, path);
                    if (iniValue != "") { Capture_FPS = double.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAPGAIN, path);
                    if (iniValue != "") { Capture_Gain = double.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_MODETYPE, path);
                    if (iniValue != "") { VisionMode = (modeType)int.Parse(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_FLIPX, path);
                    if (iniValue != "") { FlipX = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_FLIPY, path);
                    if (iniValue != "") { FlipY = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_TILEDARRY_X, path);
                    if (iniValue != "") { m_ptTileArray.X = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_TILEDARRY_Y, path);
                    if (iniValue != "") { m_ptTileArray.Y = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_FOVFORTILE_W, path);
                    if (iniValue != "") { m_dFOVforTile_W = Convert.ToDouble(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_FOVFORTILE_H, path);
                    if (iniValue != "") { m_dFOVforTile_H = Convert.ToDouble(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_AXISXNUM, path);
                    if (iniValue != "") { m_nAxisXnum = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_AXISYNUM, path);
                    if (iniValue != "") { m_nAxisYnum = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_AXISXNAGATIVE, path);
                    if (iniValue != "") { m_bXAxisNagative = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_AXISYNAGATIVE, path);
                    if (iniValue != "") { m_bYAxisNagative = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(VISIONSETTING_SECTION, VISIONSETTING_KEY_CAMERATILT, path);
                    if (iniValue != "") { m_dCameraTilt = Convert.ToDouble(iniValue); }

                    SetCaptureProperty();

                    return true;
                }
                else
                    return false;

                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
                return false;
            }
        }

        public void ResetResultImage()
        {
            try
            {
                if (m_pImageFromCam != null)
                {
                    if (m_pImageResult != null && !m_pImageResult.IsDisposed)
                    {
                        m_pImageResult.Dispose();
                        m_pImageResult = null;
                        if (m_pImageFromCam != null)
                            m_pImageResult = m_pImageFromCam.Clone();
                    }
                    else
                    {
                        if (m_pImageFromCam != null)
                            m_pImageResult = m_pImageFromCam.Clone();
                    }
                }
            }
            catch (Exception E)
            {                
                throw E;
            }
        }

        public void ResetProcessImage()
        {
            try
            {
                if (m_pImageFromCam != null)
                {
                    if (m_pImageProcessing != null && !m_pImageProcessing.IsDisposed)
                    {
                        m_pImageProcessing.Dispose();
                        m_pImageProcessing = null;
                        if (m_pImageFromCam != null)
                            m_pImageProcessing = m_pImageFromCam.Clone();
                    }
                    else
                    {
                        if (m_pImageFromCam != null)
                            m_pImageProcessing = m_pImageFromCam.Clone();
                    }
                }

                m_ptResultPoint.X = -1;
                m_ptResultPoint.Y = -1;
                m_listResultPoints.Clear();
                m_listResultLines.Clear();
                m_rtResultRect.X = -1; m_rtResultRect.Y = -1; m_rtResultRect.Width = 0; m_rtResultRect.Height = 0;
                m_strResult.Clear();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public CvPoint2D32f ConvertMMPosition(CvPoint pixelPosition)
        {
            CvPoint2D32f mmPosition = new CvPoint2D32f(-99999, -99999);

            double mm_per_pixel_width = 0;
            double mm_per_pixel_height = 0;

            //if (VisionMode == modeType.AreaTile)
            //{
            //    mm_per_pixel_width = (CamFOV_Width * TileArray.X) / GetLoadImgWidth();
            //    mm_per_pixel_height = (CamFOV_Height * TileArray.Y) / GetLoadImgHeight();
            //}
            //else
            {
                mm_per_pixel_width = CamFOV_Width / GetLoadImgWidth();
                mm_per_pixel_height = CamFOV_Height / GetLoadImgHeight();
            }

            CvPoint2D32f centerpt = new CvPoint2D32f((CamFOV_Width / 2) * -1, (CamFOV_Height / 2) * -1);

            mmPosition.X = (float)(pixelPosition.X * mm_per_pixel_width) + centerpt.X;
            mmPosition.Y = (float)-((pixelPosition.Y * mm_per_pixel_height) + centerpt.Y);

            return mmPosition;
        }

        public Point ConvertPixelPosition(CvPoint2D32f mmPosition)
        {
            Point pixelPosition = new Point(-99999, -99999);

            double mm_per_pixel_width = 0;
            double mm_per_pixel_height = 0;

            //if (VisionMode == modeType.AreaTile)
            //{
            //    mm_per_pixel_width = GetLoadImgWidth() / CamFOV_Width * TileArray.X);
            //    mm_per_pixel_height = GetLoadImgHeight() / (CamFOV_Height * TileArray.Y);
            //}
            //else
            {
                mm_per_pixel_width = GetLoadImgWidth() / CamFOV_Width;
                mm_per_pixel_height = GetLoadImgHeight() / CamFOV_Height;
            }

            CvPoint2D32f centerpt = new CvPoint2D32f((CamFOV_Width / 2) * -1, (CamFOV_Height / 2) * -1);

            pixelPosition.X = (int)((mmPosition.X - centerpt.X) * mm_per_pixel_width);
            pixelPosition.Y = (int)(-((mmPosition.Y + centerpt.Y) * mm_per_pixel_height));

            return pixelPosition;
        }

        public void SaveRecipeINI(string path)
        {
            try
            {
                IniFile ini = new IniFile();

                StringBuilder strListProc = new StringBuilder();
                for (int i = 0; i < m_listProcess.Count; i++)
                {
                    processType procType = m_listProcess[i];
                    if (i == m_listProcess.Count - 1)
                    {
                        strListProc.Append(Convert.ToInt32(m_listProcess[i]));
                    }
                    else
                    {
                        strListProc.Append(Convert.ToInt32(m_listProcess[i]).ToString() + ",");
                    }
                }

                if (strListProc.Length > 0)
                {
                    ini.IniWriteValue(VISIONRCP_SECTION, VISIONRCP_KEY_LISTPROCESS, strListProc.ToString(), path);
                }

                ini.IniWriteValue(VISIONRCP_SECTION, VISIONRCP_KEY_TARGETMODE, Convert.ToInt32(m_targetMode).ToString(), path);
                ini.IniWriteValue(VISIONRCP_SECTION, VISIONRCP_KEY_TARGETOBJINDEX, m_nTargetObjIndex.ToString(), path);
                ini.IniWriteValue(VISIONRCP_SECTION, VISIONRCP_KEY_TARGETGROUPINDEX, m_nTargetGroupIndex.ToString(), path);
                ini.IniWriteValue(VISIONRCP_SECTION, VISIONRCP_KEY_USETARGETOBJINDEX, m_bUseTargetObject.ToString(), path);

                m_processTempMatch.SaveRecipeINI(path, ini);
                m_processFitCircle.SaveRecipeINI(path, ini);
                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        public bool LoadRecipeINI(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    String iniValue = "";
                    IniFile ini = new IniFile();
                    iniValue = ini.IniReadValue(VISIONRCP_SECTION, VISIONRCP_KEY_LISTPROCESS, path);
                    if (iniValue != "") 
                    {
                        string[] arrProcess = iniValue.Split(',');
                        if (arrProcess != null)
                        {
                            m_listProcess.Clear();
                            for (int i = 0; i < arrProcess.Length; i++)
                            {
                                processType procType = (processType)Convert.ToInt32(arrProcess[i].ToString());
                                m_listProcess.Add(procType);
                            }
                        }
                    }

                    iniValue = ini.IniReadValue(VISIONRCP_SECTION, VISIONRCP_KEY_TARGETMODE, path);
                    if (iniValue != "") { m_targetMode = (targetMode)Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_SECTION, VISIONRCP_KEY_TARGETOBJINDEX, path);
                    if (iniValue != "") { m_nTargetObjIndex = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_SECTION, VISIONRCP_KEY_TARGETGROUPINDEX, path);
                    if (iniValue != "") { m_nTargetGroupIndex = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(VISIONRCP_SECTION, VISIONRCP_KEY_USETARGETOBJINDEX, path);
                    if (iniValue != "") { m_bUseTargetObject = Convert.ToBoolean(iniValue); }

                    m_processTempMatch.LoadRecipeINI(path, ini);
                    m_processFitCircle.LoadRecipeINI(path, ini);

                    m_cvData.m_strRecipePath = path;

                    return true;
                }
                else
                    return false;


            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
                return false;
            }
        }
    }
}
