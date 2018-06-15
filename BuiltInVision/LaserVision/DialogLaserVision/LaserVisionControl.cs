using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalvoScanner.LaserVision.OpenCV;
using OpenCvSharp;
using System.Threading;
using BuiltInVision;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using System.IO;
using BuiltInVision.LaserVision.DialogLaserVision;
using BuiltInVision.LaserVision.OpenCV;

namespace GalvoScanner.LaserVision.DialogLaserVision
{
    public partial class LaserVisionControl : UserControl
    {
        static LaserVisionControl m_laserVisionControl = null;
        private DraftingViewportLayout viewportLayout1 = null;

        static public LaserVisionControl GetInstance()
        {
            return m_laserVisionControl;
        }

        OpenCVData m_cvData = null;

        Thread m_continuousGrabThread = null;
        bool m_bIsContinuoisGrabLive = false;
        public bool IsLiveMode
        {
            get { return m_bIsContinuoisGrabLive; }
        }

        Thread m_snapForTileThread = null;
        bool m_bIsSnapForTileLive = false;

        //ImageProcessViewer m_originImageViewer = null;
        ImageProcessingViewer m_originImageViewer = null;
        public ImageProcessingViewer OriginImageViewer
        {
            get { return m_originImageViewer; }
        }
        ImageProcessingViewer m_processingImageViewer = null;
        public ImageProcessingViewer ProcessingImageViewer
        {
            get { return m_processingImageViewer; }
        }
        ImageProcessingViewer m_resultImageViewer = null;
        public ImageProcessingViewer ResultImageViewer
        {
            get { return m_resultImageViewer; }
        }
        // AreaTile 모드 일때만!!
        ImageProcessingViewer m_liveImageViewer = null;
        public ImageProcessingViewer LiveImageViewer
        {
            get { return m_liveImageViewer; }
        }

        ImageProcess_TemplateMatch m_templateMatch = null;
        ImageProcess_FitCircle m_fitCircle = null;
        ImageProcess_HoughLine m_houghLine = null;

        public event EventHandler GrabFromCamera;
        public void GrabCamera()
        {
            if (this.GrabFromCamera != null)
            {
                if (m_cvData.VisionMode == OpenCVData.modeType.AreaTile)
                {
                    LiveImageViewer.RefreshImage();
                }
                else if (m_cvData.VisionMode == OpenCVData.modeType.LineScan)
                {

                }
                else
                {
                    GrabFromCamera(this, EventArgs.Empty);
                    OriginImageViewer.RefreshImage();    
                }                
            }
        }

        public void GrabCameraForTile()
        {
            if (this.GrabFromCamera != null)
            {
                GrabFromCamera(this, EventArgs.Empty);
            }
        }

        public event EventHandler ChangedVisionIndex;
        public void ChangeVisIndex()
        {
            if (this.ChangedVisionIndex != null)
            {
                ChangedVisionIndex(this, EventArgs.Empty);
            }
        }

        public CheckBox VisionVisibleCheckBox
        {
            get { return checkBox_vision_visible; }
        }

        public LaserVisionControl()
        {
            m_cvData = OpenCVData.GetInstance(0);

            InitializeComponent();

            m_laserVisionControl = this;
        }

        void ContinuousGrabThread()
        {
            try
            {
                while (m_bIsContinuoisGrabLive)
                {
                    Thread.Sleep(m_cvData.ContinuousInterval);
                    if (m_cvData.GrabFromCamera() != null)
                    {
                        GrabCamera();
                    }
                }

                m_continuousGrabThread = null;
            }
            catch (Exception)
            {

            }
        }

        public void StopContinuousGrab()
        {
            Thread.Sleep(10);
            m_bIsContinuoisGrabLive = false;
            Thread.Sleep(10);

            while (m_continuousGrabThread != null) { Thread.Sleep(10); }

            m_cvData.GrabFromCamera();
            Thread.Sleep(30);
            m_cvData.GrabFromCamera();
        }

        void SnapForTileThread()
        {
            try
            {
                //IMotion motion = MotionBase.GetInstanceInterface();
                //if (motion != null && motion.IsInitMotion())
                //{
                //    if (motion.AxisGetServoOnState(m_cvData.AxisNumberX) || motion.AxisGetServoOnState(m_cvData.AxisNumberY))
                //    {
                //        if (!motion.AxisIsBusy(m_cvData.AxisNumberX) && !motion.AxisIsBusy(m_cvData.AxisNumberY))
                //        {
                //            m_cvData.GrabFromCamera();

                //            IplImage tiledImage = Cv.CreateImage(new CvSize(m_cvData.CamPixel_Width * m_cvData.TileArray.X, m_cvData.CamPixel_Height * m_cvData.TileArray.Y),
                //                                m_cvData.GetIPLImage().Depth, m_cvData.GetIPLImage().NChannels);

                //            int indexOffsetX = m_cvData.TileArray.X / 2;
                //            int indexOffsetY = m_cvData.TileArray.Y / 2;

                //            double stepX, stepY;
                //            stepX = m_cvData.FOVforTile_W;
                //            stepY = m_cvData.FOVforTile_H;

                //            if (m_cvData.AxisNagativeX)
                //            {
                //                stepX = -stepX;
                //            }

                //            if (m_cvData.AxisNagativeY)
                //            {
                //                stepY = -stepY;
                //            }

                //            double curruntX = motion.AxisGetActualPosition(m_cvData.AxisNumberX);
                //            double curruntY = motion.AxisGetActualPosition(m_cvData.AxisNumberY);

                //            for (int y = 0; y < m_cvData.TileArray.Y; y++)
                //            {
                //                for (int x = 0; x < m_cvData.TileArray.X; x++)
                //                {
                //                    if (!m_bIsSnapForTileLive)
                //                        break;

                //                    double motPosX = curruntX + ((x - indexOffsetX) * stepX);
                //                    double motPosY = curruntY + ((y - indexOffsetY) * stepY);

                //                    double rad = (m_cvData.CameraTilt) * Math.PI / 180.0;
                //                    double tiltX_mot = ((motPosX - curruntX) * Math.Cos(rad) + (motPosY - curruntY) * Math.Sin(rad)) + curruntX;
                //                    double tiltY_mot = (-(motPosX - curruntX) * Math.Sin(rad) + (motPosY - curruntY) * Math.Cos(rad)) + curruntY;

                //                    motion.AxisMove(m_cvData.AxisNumberX, tiltX_mot, false);
                //                    motion.AxisMove(m_cvData.AxisNumberY, tiltY_mot, false);

                //                    while (motion.AxisIsBusy(m_cvData.AxisNumberX) || motion.AxisIsBusy(m_cvData.AxisNumberY)) { Thread.Sleep(10); }

                //                    Thread.Sleep(100);

                //                    m_cvData.GrabFromCamera();
                //                    m_cvData.GrabFromCamera();
                //                    Thread.Sleep(50);
                //                    IplImage img = m_cvData.GetIPLImage();

                //                    tiledImage.SetROI(new CvRect(x * m_cvData.CamPixel_Width, y * m_cvData.CamPixel_Height, img.Width, img.Height));
                //                    img.Copy(tiledImage);
                //                }
                //            }

                //            tiledImage.ResetROI();
                //            m_cvData.SetImageFromCam(tiledImage.Clone());
                //            GrabCamera();

                //            motion.AxisMove(m_cvData.AxisNumberX, curruntX, false);
                //            motion.AxisMove(m_cvData.AxisNumberY, curruntY, false);
                //            while (motion.AxisIsBusy(m_cvData.AxisNumberX) || motion.AxisIsBusy(m_cvData.AxisNumberY)) { Thread.Sleep(10); }

                //            GrabCameraForTile();
                //        }
                //        else
                //        {
                //            MessageBox.Show("Motion is busy!");
                //        }                        
                //    }
                //    else
                //    {
                //        MessageBox.Show("No motion servo on!");
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("No motion!");
                //}

                //m_bIsSnapForTileLive = false;
                m_snapForTileThread = null;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        public void StartContinousGrab()
        {
            try
            {
                if (m_continuousGrabThread == null)
                {
                    m_cvData.ContinuousInterval = Convert.ToInt32(textBox_autosave_interval.Text);
                    if (checkBox_autosave_enable.Checked)
                    {
                        if (textBox_autosave_path.Text == "")
                        {
                            string sDirPath;
                            sDirPath = Application.StartupPath + "\\images";
                            m_cvData.AutoSavePath = textBox_autosave_path.Text = sDirPath;
                            DirectoryInfo di = new DirectoryInfo(sDirPath);
                            if (di.Exists == false)
                            {
                                di.Create();
                            }
                        }
                        else
                        {
                            m_cvData.AutoSavePath = textBox_autosave_path.Text;
                        }
                    }
                    
                    m_bIsContinuoisGrabLive = true;
                    m_continuousGrabThread = new Thread(new ThreadStart(ContinuousGrabThread));

                    m_continuousGrabThread.Start();
                }
            }
            catch (Exception E)
            {
                m_bIsContinuoisGrabLive = false;
                m_continuousGrabThread = null;

                MessageBox.Show(E.ToString());
            }
        }

        public void StartSnapForTile()
        {
            if (m_snapForTileThread == null)
            {
                m_liveImageViewer.Hide();

                m_bIsSnapForTileLive = true;
                m_snapForTileThread = new Thread(new ThreadStart(SnapForTileThread));

                m_snapForTileThread.Start();
            }
        }

        public void StopSnapForTile()
        {
            m_bIsSnapForTileLive = false;
        }

        public void ShowOriginalViewer()
        {
            //StopContinuousGrab();
            m_originImageViewer.Show();
            m_originImageViewer.Focus();
        }

        public void ShowProcessingViewer()
        {
            //StopContinuousGrab();
            m_processingImageViewer.Show();
            m_processingImageViewer.Focus();
        }

        public void ShowResultViewer()
        {
            //StopContinuousGrab();
            m_resultImageViewer.Show();
            m_resultImageViewer.Focus();
        }

        private void comboBox_cam_num_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_continuousGrabThread != null)
            {
                StopContinuousGrab();
            }            
        }

        private void button_one_shot_Click(object sender, EventArgs e)
        {
            if (m_continuousGrabThread != null)
            {
                StopContinuousGrab();
            }

            if (m_cvData.GrabFromCamera() != null)
            {
                switch (m_cvData.VisionMode)
                {
                    case OpenCVData.modeType.AreaTile:
                        {
                            StartSnapForTile();
                        }
                        break;

                    case OpenCVData.modeType.LineScan:
                        {

                        }
                        break;

                    case OpenCVData.modeType.BuiltInVision:
                    case OpenCVData.modeType.GeneralArea:
                        {
                            m_cvData.GrabFromCamera();
                            GrabCamera();
                        }
                        break;
                }
                
            }
        }

        private void button_continuous_Click(object sender, EventArgs e)
        {
            if (m_cvData.VisionMode == OpenCVData.modeType.AreaTile)
            {
                m_liveImageViewer.Show();
                m_liveImageViewer.Focus();
            }
            else if (m_cvData.VisionMode == OpenCVData.modeType.LineScan)
            {

            }
            else
            {
                if (m_originImageViewer.Visible)
                {
                    m_originImageViewer.Hide();
                }
                if (m_processingImageViewer.Visible)
                {
                    m_processingImageViewer.Hide();
                }
            }

            StartContinousGrab();            
            
        }

        private void button_continuous_stop_Click(object sender, EventArgs e)
        {
            if (m_cvData.VisionMode == OpenCVData.modeType.AreaTile)
            {
                StopSnapForTile();
            }

            StopContinuousGrab();
        }        

        private void LaserVisionControl_Load(object sender, EventArgs e)
        {
            if (m_originImageViewer == null)
            {
                m_originImageViewer = new ImageProcessingViewer(this, ImageProcessingViewer.viewMode.Orignal);
            }

            if (m_processingImageViewer == null)
            {
                m_processingImageViewer = new ImageProcessingViewer(this, ImageProcessingViewer.viewMode.Processing);                
            }

            if (m_resultImageViewer == null)
            {
                m_resultImageViewer = new ImageProcessingViewer(this, ImageProcessingViewer.viewMode.Result);                
            }

            if (m_liveImageViewer == null)
            {
                m_liveImageViewer = new ImageProcessingViewer(this, ImageProcessingViewer.viewMode.Live);                
            }

            int visionCnt = OpenCVData.GetOpencvDataCount();
            for (int i = 0; i < visionCnt; i++)
            {
                comboBox_vision_num.Items.Add(i);
            }
            if (visionCnt > 0)
            {
                comboBox_vision_num.SelectedIndex = 0;
            }

            UpdateData();
        }

        public void UpdateData(bool isLoad = true)
        {
            try
            {
                if (isLoad)
                {
                    List<GalvoScanner.LaserVision.OpenCV.OpenCVData.processType> listProcess = m_cvData.GetListProcess();
                    if (listProcess != null)
                    {
                        listView_image_process.Items.Clear();
                        for (int i = 0; i < listProcess.Count; i++)
                        {
                            listView_image_process.Items.Add(listProcess[i].ToString());
                        }
                    }
                    textBox_recipe_path.Text = m_cvData.GetRecipePath();
                    textBox_markgroup_index.Text = m_cvData.GetTargetGroupIndex().ToString();
                    textBox_markobj_index.Text = m_cvData.GetTargetObjIndex().ToString();
                    checkBox_markgroup_index.Checked = !m_cvData.IsUseTargetObject();
                    checkBox_markobj_index.Checked = m_cvData.IsUseTargetObject();
                }
                else
                {
                    m_cvData.SetRecipePath(textBox_recipe_path.Text.ToString());
                    m_cvData.SetTargetGroupIndex(Convert.ToInt32(textBox_markgroup_index.Text));
                    m_cvData.SetTargetObjIndex(Convert.ToInt32(textBox_markobj_index.Text));
                    m_cvData.SetUseTartgetObject(checkBox_markobj_index.Checked);
                }
                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_file_saveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog mySaveFileDialog = new SaveFileDialog();

            //mySaveFileDialog.Filter = "BMP file(*.bmp)|*.bmp|AllFile(*.*)|*.*";
            //mySaveFileDialog.Title = "Load bitmap file";            
            //mySaveFileDialog.FilterIndex = 2;
            //mySaveFileDialog.RestoreDirectory = true;

            //if (mySaveFileDialog.ShowDialog() == DialogResult.OK)
            //{

            //    switch (mySaveFileDialog.FilterIndex)
            //    {

            //        case 1: viewportLayout1.WriteToFileRaster(2, mySaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            //            break;
            //        case 2: viewportLayout1.WriteToFileRaster(2, mySaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            //            break;
            //        case 3: viewportLayout1.WriteToFileRaster(2, mySaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Wmf);
            //            break;
            //        case 4: viewportLayout1.WriteToFileRaster(2, mySaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Emf);
            //            break;

            //    }

            //}
            
            //StopContinuousGrab();

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "BMP file(*.bmp)|*.bmp|AllFile(*.*)|*.*";
            saveFile.Title = "Save bitmap file";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (m_cvData.SaveImage(saveFile.FileName))
                {
                    MessageBox.Show(StringLib.Msg_VisionSaveImageSuccecse);
                }
                else
                {
                    MessageBox.Show(StringLib.Msg_VisionSaveImageFail);
                }
            }
        }

        private void button_file_loadImage_Click(object sender, EventArgs e)
        {
            StopContinuousGrab();

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "BMP file(*.bmp)|*.bmp|AllFile(*.*)|*.*";
            openFile.Title = "Load bitmap file";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (m_cvData.LoadImage(openFile.FileName))
                {
                    GrabCamera();
                }
            }                      
        }

        private void timer_continuous_grab_Tick(object sender, EventArgs e)
        {
            if (m_cvData.GrabFromCamera() != null)
            {
                GrabCamera();
            }
        }

        private void button_origin_viewer_Click(object sender, EventArgs e)
        {
            try
            {
                ShowOriginalViewer();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_result_viewer_Click(object sender, EventArgs e)
        {
            try
            {
                ShowProcessingViewer();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_result_viewer_Click_1(object sender, EventArgs e)
        {
            ShowResultViewer();
        }

        private void button_result_reset_Click(object sender, EventArgs e)
        {
            try
            {
                bool bIsLived = false;
                if (m_bIsContinuoisGrabLive)
                {
                    bIsLived = true;
                    StopContinuousGrab();
                    //Thread.Sleep(30);
                    //while (m_continuousGrabThread != null) { Thread.Sleep(10); }

                    //Thread.Sleep(30);
                    //m_cvData.GrabFromCamera();
                    //Thread.Sleep(30);
                    //m_cvData.GrabFromCamera();

                }

                m_cvData.ResetProcessImage();
                m_processingImageViewer.RefreshImage();
                m_processingImageViewer.ResetROI();
                m_processingImageViewer.ResetTemplateOffsetPosition();

                AddListProcess(OpenCVData.processType.GrabToProcess);

                if (bIsLived)
                {
                    StartContinousGrab();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_template_match_Click(object sender, EventArgs e)
        {
            if (m_templateMatch == null)
            {
                m_templateMatch = new ImageProcess_TemplateMatch(this);
            }

            m_templateMatch.Show();
            m_templateMatch.Focus();
        }

        private void button_processing_result_Click(object sender, EventArgs e)
        {
            try
            {
                m_cvData.ResetResultImage();
                ShowResultViewer();
                AddListProcess(OpenCVData.processType.ProcessToResult);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_move_obj_Click(object sender, EventArgs e)
        {
            try
            {
                //Laser3DForm laserform = Laser3DForm.GetInstance();
                //if (laserform != null)
                //{
                //    if (checkBox_markobj_index.Checked)
                //    {
                //        int objIndex = Convert.ToInt32(textBox_markobj_index.Text.ToString()) + 1;
                //        if (objIndex > 0)
                //        {
                //            laserform.ObjectMovePositionAbs(objIndex, m_cvData.GetResultPosition().X, m_cvData.GetResultPosition().Y);
                //        }
                //    }

                //    if (checkBox_markgroup_index.Checked)
                //    {
                //        int groupIndex = Convert.ToInt32(textBox_markgroup_index.Text.ToString());
                //        if (groupIndex >= 0)
                //        {
                //            //laserform.ObjectMovePositionAbs(groupIndex, m_cvData.GetResultPosition().X, m_cvData.GetResultPosition().Y);
                //            laserform.GroupMovePositionAbs(groupIndex, m_cvData.GetResultPosition().X, m_cvData.GetResultPosition().Y);
                //        }
                //    }
                    
                //}
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_mark_obj_Click(object sender, EventArgs e)
        {
            try
            {
                //Laser3DForm laserform = Laser3DForm.GetInstance();
                //if (laserform != null)
                //{
                //    if (checkBox_markobj_index.Checked)
                //    {
                //        int objIndex = Convert.ToInt32(textBox_markobj_index.Text.ToString()) + 1;
                //        if (objIndex > 0)
                //        {
                //            int[] indexs = new int[1];
                //            indexs[0] = objIndex;
                //            laserform.SelectObject(indexs);
                //            laserform.StartMark(true, false);
                //        }
                //    }
                //    else
                //    {
                //        int groupIndex = Convert.ToInt32(textBox_markgroup_index.Text.ToString());
                //        if (groupIndex >= 0)
                //        {
                //            laserform.SelectGroup(groupIndex);
                //            laserform.StartMark(true, false);
                //        }
                //    }
                    
                //}
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_moveNmark_obj_Click(object sender, EventArgs e)
        {
            try
            {
                //Laser3DForm laserform = Laser3DForm.GetInstance();
                //if (laserform != null)
                //{
                //    if (checkBox_markobj_index.Checked)
                //    {
                //        int objIndex = Convert.ToInt32(textBox_markobj_index.Text.ToString()) + 1;
                //        if (objIndex > 0)
                //        {
                //            int[] indexs = new int[1];
                //            indexs[0] = objIndex;

                //            CvPoint2D32f targpos = m_cvData.ConvertMMPosition(m_cvData.GetProcessTeplateMatch().TargetResultPosition);
                //            laserform.ObjectMovePositionAbs(indexs, m_cvData.GetResultPosition().X, m_cvData.GetResultPosition().Y);

                //            laserform.SelectObject(indexs);
                //            laserform.StartMark(true, true);
                //        }
                //    }
                //    else
                //    {
                //        int groupIndex = Convert.ToInt32(textBox_markgroup_index.Text.ToString());
                //        if (groupIndex >= 0)
                //        {
                //            CvPoint2D32f targpos = m_cvData.ConvertMMPosition(m_cvData.GetProcessTeplateMatch().TargetResultPosition);
                //            laserform.GroupMovePositionAbs(groupIndex, m_cvData.GetResultPosition().X, m_cvData.GetResultPosition().Y);

                //            laserform.SelectGroup(groupIndex);
                //            laserform.StartMark(true, true);
                //        }
                //    }
                    
                //}
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_list_clear_Click(object sender, EventArgs e)
        {
            try
            {
                m_cvData.GetListProcess().Clear();
                listView_image_process.Items.Clear();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }
     
        public void AddListProcess(OpenCVData.processType type)
        {
            try
            {
                int index = m_cvData.GetListProcess().IndexOf(type);
                if (index == -1)
                {
                    m_cvData.GetListProcess().Add(type);
                    listView_image_process.Items.Add(type.ToString());
                }                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_one_cycle_Click(object sender, EventArgs e)
        {
            try
            {
                ExecuteOncyle();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        public void ExecuteOncyle()
        {
            try
            {
                bool bIsLived = false;
                if (m_bIsContinuoisGrabLive)
                {
                    bIsLived = true;
                    StopContinuousGrab();
                    //Thread.Sleep(30);
                    //while (m_continuousGrabThread != null) { Thread.Sleep(10); }

                    //Thread.Sleep(30);
                    //m_cvData.GrabFromCamera();
                    //Thread.Sleep(30);
                    //m_cvData.GrabFromCamera();

                }

                for (int i = 0; i < m_cvData.GetListProcess().Count; i++)
                {
                    switch (m_cvData.GetListProcess()[i])
                    {
                        case OpenCVData.processType.GrabToProcess:
                            {
                                m_cvData.ResetProcessImage();
                                m_processingImageViewer.RefreshImage();
                                m_processingImageViewer.ResetROI();
                                m_processingImageViewer.ResetTemplateOffsetPosition();
                            }
                            break;

                        case OpenCVData.processType.ProcessToResult:
                            {
                                m_cvData.ResetResultImage();
                                m_resultImageViewer.RefreshImage();
                                ShowResultViewer();
                            }
                            break;

                        case OpenCVData.processType.TemplateMatch:
                            {

                                if (i > 0 && m_cvData.GetListProcess().Contains(OpenCVData.processType.GrabToProcess))
                                {
                                    ProcessTemplateMatch.matchResultType result = m_cvData.GetProcessTeplateMatch().ExcuteTemplateMatch();
                                    if (result == ProcessTemplateMatch.matchResultType.LowScore)
                                    {
                                        m_cvData.ResetResultImage();
                                        m_resultImageViewer.RefreshImage();
                                        ShowResultViewer();

                                        MessageBox.Show("Low template match score.");

                                        if (bIsLived)
                                        {
                                            StartContinousGrab();
                                        }
                                        return;
                                    }
                                    else if (result == ProcessTemplateMatch.matchResultType.Shift)
                                    {
                                        m_cvData.ResetResultImage();
                                        m_resultImageViewer.RefreshImage();
                                        ShowResultViewer();

                                        MessageBox.Show("Shift Error. ");

                                        if (bIsLived)
                                        {
                                            StartContinousGrab();
                                        }
                                        return;
                                    }                                    
                                    else if(result == ProcessTemplateMatch.matchResultType.NoTemplateImage)
                                    {
                                        MessageBox.Show("No template image.");
                                        if (bIsLived)
                                        {
                                            StartContinousGrab();
                                        }
                                        return;
                                    }
                                    else if (result == ProcessTemplateMatch.matchResultType.NoProcessingImage)
                                    {
                                        MessageBox.Show("No process image.");
                                        if (bIsLived)
                                        {
                                            StartContinousGrab();
                                        }
                                        return;
                                    }                                 
                                }
                                else
                                {
                                    MessageBox.Show("No process image.");
                                }
                            }
                            break;

                        case OpenCVData.processType.Threshold:
                            {

                            }
                            break;

                        case OpenCVData.processType.FitCircle:
                            {
                                if (i > 0 && m_cvData.GetListProcess().Contains(OpenCVData.processType.GrabToProcess))
                                {
                                    m_cvData.GetProcessFitCircle().Inspect();
                                }
                                else
                                {
                                    MessageBox.Show("No process image.");
                                }
                            }
                            break;
                        case OpenCVData.processType.HoughLine:
                            {
                                if (i > 0 && m_cvData.GetListProcess().Contains(OpenCVData.processType.GrabToProcess))
                                {
                                    ProcessHoughLine.houghResultType result = m_cvData.GetProcessHoughLine().ExecuteHoughLines();
                                    if (result == ProcessHoughLine.houghResultType.Tilt)
                                    {
                                        m_cvData.ResetResultImage();
                                        m_resultImageViewer.RefreshImage();
                                        ShowResultViewer();

                                        MessageBox.Show("Tilt Error. ");

                                        if (bIsLived)
                                        {
                                            StartContinousGrab();
                                        }
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No process image.");
                                }
                            }
                            break;
                    }
                }

                if (bIsLived)
                {
                    StartContinousGrab();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_fit_circle_Click(object sender, EventArgs e)
        {
            if(m_fitCircle == null)
            {
                m_fitCircle = new ImageProcess_FitCircle(this);
            }

            m_fitCircle.Focus();
            m_fitCircle.Show();
        }

        private void button_hough_line_Click(object sender, EventArgs e)
        {
            if (m_houghLine == null)
            {
                m_houghLine = new ImageProcess_HoughLine(this);
            }

            m_houghLine.Focus();
            m_houghLine.Show();
        }

        private void checkBox_markgroup_index_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox_markgroup_index.Checked)
                {
                    checkBox_markobj_index.Checked = false;
                    textBox_markgroup_index.Enabled = true;
                    textBox_markobj_index.Enabled = false;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void checkBox_markobj_index_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox_markobj_index.Checked)
                {
                    checkBox_markgroup_index.Checked = false;
                    textBox_markgroup_index.Enabled = false;
                    textBox_markobj_index.Enabled = true;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_file_saveRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "CAMSENSOR file(*.sensorvis)|*.sensorvis|AllFile(*.*)|*.*";
                saveFile.Title = "Save CAM sensor vision file";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    UpdateData(false);
                    m_cvData.SaveRecipeINI(saveFile.FileName);

                    UpdateData();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());                
            }
        }

        private void button_file_loadRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "CAMSENSOR file(*.sensorvis)|*.sensorvis|AllFile(*.*)|*.*";
                openFile.Title = "Load CAM sensor vision file";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    if (m_cvData.LoadRecipeINI(openFile.FileName))
                    {                        
                        UpdateData();
                        if (m_templateMatch != null)
                        {
                            m_templateMatch.UpdateData();
                        }
                        if (m_fitCircle != null)
                        {
                            m_fitCircle.UpdateData();
                        }
                        if (m_houghLine != null)
                        {
                            m_houghLine.UpdateData();
                        }                        
                    }              
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }

        }

        private void textBox_markgroup_index_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_cvData.SetTargetGroupIndex(Convert.ToInt32(textBox_markgroup_index.Text));
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void textBox_markobj_index_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_cvData.SetTargetObjIndex(Convert.ToInt32(textBox_markobj_index.Text));
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        public void SetLayout(DraftingViewportLayout viewportLayout)
        {
            viewportLayout1 = viewportLayout;
        }
        private void ClearPreviousSelection()
        {
            viewportLayout1.SetView(viewType.Top, false, true);
            viewportLayout1.ClearAllPreviousCommandData();
        }

        private void button_Line_Click(object sender, EventArgs e)
        {
            ClearPreviousSelection();
            viewportLayout1.drawingAlignedDim = true;
        }

        private void button_Straight_Click(object sender, EventArgs e)
        {
            ClearPreviousSelection();
            viewportLayout1.drawingLinearDim = true;
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            int nCount = viewportLayout1.Entities.Count;
            viewportLayout1.Entities.RemoveRange(1, nCount - 1);

            viewportLayout1.Invalidate();
        }

        private void button_set_path_folder_autosave_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selected = dialog.SelectedPath;
                    m_cvData.AutoSavePath = textBox_autosave_path.Text = selected;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void checkBox_autosave_enable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                m_cvData.IsAutoSave = checkBox_autosave_enable.Checked;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_new_vision_recipe_Click(object sender, EventArgs e)
        {

        }

        private void comboBox_vision_num_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selected = comboBox_vision_num.SelectedIndex;
                if (selected != -1)
                {
                    m_cvData = OpenCVData.GetInstance(selected);
                    UpdateData();
                    if (m_templateMatch != null)
                    {
                        m_templateMatch.UpdateData();
                    }
                    if (m_fitCircle != null)
                    {
                        m_fitCircle.UpdateData();
                    }
                    if (m_houghLine != null)
                    {
                        m_houghLine.UpdateData();
                    }

                    ChangeVisIndex();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

    }
}
