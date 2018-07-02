using BuiltInVision;
using Camera_NET;
using GalvoScanner.LaserVision.OpenCV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalvoScanner.LaserVision.DialogLaserVision
{
    public partial class VisionSetting : Form
    {
        bool m_bIsReady = false;
        public bool IsReadyVision
        {
            get { return m_bIsReady; }
        }

        bool m_bIsLoading = true;
        bool m_bIsCamNotChange = false;
        private OpenCVData m_cvData = null;

        public event EventHandler ChangedVisionSetting;
        public void ChangeVisionSettingEvent()
        {
            if (this.ChangedVisionSetting != null)
            {
                ChangedVisionSetting(this, EventArgs.Empty);
            }
        }

        public VisionSetting()
        {
            InitializeComponent();

            m_cvData = OpenCVData.GetInstance();

            //int camCnt = m_cvData.GetCameraCount();
            int camCnt = OpenCVData.GetDeviceCount();
            for (int i = 0; i < camCnt; i++)
            {
                comboBox_cam_num.Items.Add(i + ":" + OpenCVData.GetDevList()[i].Name);
            }

            int visionCnt = OpenCVData.GetOpencvDataCount();
            for (int i = 0; i < visionCnt; i++)
            {
                m_cvData = OpenCVData.GetInstance(i, true);
                if (m_cvData.LoadSettingINI(Application.StartupPath + "\\VisionSetting_" + i + ".ini"))
                {
                    if (comboBox_cam_num.Items.Count > m_cvData.GetCameraNum())
                    {
                        m_cvData.SetCameraNum(m_cvData.GetCameraNum());

                        m_bIsCamNotChange = true;
                        comboBox_cam_num.SelectedIndex = m_cvData.GetCameraNum();
                        checkBox_use_vision.Checked = m_cvData.GetUseVision();
                        propertyGrid_vision_setting.SelectedObject = m_cvData;

                        if (m_cvData.GetUseVision())
                        {
                            StartiVisionInit();
                        }

                    }
                    else
                    {
                        comboBox_cam_num.SelectedIndex = 0;
                        m_cvData.SetCameraNum(comboBox_cam_num.SelectedIndex);
                    }
                }
                else
                {
                    comboBox_cam_num.SelectedIndex = 0;
                    m_cvData.SetCameraNum(comboBox_cam_num.SelectedIndex);
                }
            }

                //if (comboBox_cam_num.Items.Count > 0)
                //{
                //    if (m_cvData.LoadSettingINI(Application.StartupPath + "\\VisionSetting.ini"))
                //    {
                //        if (comboBox_cam_num.Items.Count > m_cvData.GetCameraNum())
                //        {
                //            m_cvData.SetCameraNum(m_cvData.GetCameraNum());

                //            m_bIsCamNotChange = true;
                //            comboBox_cam_num.SelectedIndex = m_cvData.GetCameraNum();
                //            checkBox_use_vision.Checked = m_cvData.GetUseVision();
                //            propertyGrid_vision_setting.SelectedObject = m_cvData;

                //            if (m_cvData.GetUseVision())
                //            {
                //                StartiVisionInit();
                //            }

                //        }
                //        else
                //        {
                //            comboBox_cam_num.SelectedIndex = 0;
                //            m_cvData.SetCameraNum(comboBox_cam_num.SelectedIndex);
                //        }
                //    }
                //    else
                //    {
                //        comboBox_cam_num.SelectedIndex = 0;
                //        m_cvData.SetCameraNum(comboBox_cam_num.SelectedIndex);
                //    }
                //}

            try
            {
                SetControlValue();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        public void StartiVisionInit()
        {
            timer_initial_setting.Start();
        }

        private void SetControlValue()
        {
            try
            {
                //m_cvData.GetCaptureProperty();
                propertyGrid_vision_setting.SelectedObject = m_cvData;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                m_cvData.SetUseVision(checkBox_use_vision.Checked);
                if (m_cvData.GetUseVision())
                {
                    m_cvData.SetCaptureProperty();
                }
                
                if (m_cvData.TileArray.X % 2 == 0 || m_cvData.TileArray.Y % 2 == 0)
                {
                    MessageBox.Show(StringLib.Msg_NotBeSetEvenNumber);
                    return;
                }

                Hide();

                ChangeVisionSettingEvent();

                if (comboBox_vis_index.SelectedIndex != -1)
                {
                    m_cvData.SaveSettingINI(Application.StartupPath + "\\VisionSetting_" + comboBox_vis_index.SelectedIndex + ".ini");
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }            
        }

        private void VisionSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void VisionSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                comboBox_cam_num.SelectedIndex = m_cvData.GetCameraNum();
                SetControlValue();
            }
        }

        private void VisionSetting_Load(object sender, EventArgs e)
        {
            int visCnt = OpenCVData.GetOpencvDataCount();
            for (int i = 0; i < visCnt; i++)
            {
                comboBox_vis_index.Items.Add(i);
            }
            if (visCnt > 0)
            {
                comboBox_vis_index.SelectedIndex = 0;
            }

            //propertyGrid_vision_setting.SelectedObject = m_cvData;
            m_bIsLoading = false;
        }

        private void comboBox_cam_num_SelectedIndexChanged(object sender, EventArgs e)
        {
            int camIndex = comboBox_cam_num.SelectedIndex;
            if (camIndex != -1)
            {
                ResolutionList rl = OpenCVData.GetResolutionList(camIndex);
                if (rl == null || rl.Count <= 0)
                    return;

                comboBox_pixel_list.Items.Clear();
                for(int i = 0; i < rl.Count; i++)
                {
                    comboBox_pixel_list.Items.Add(rl[i].ToString());
                }

                comboBox_pixel_list.SelectedIndex = 0;
            }
            

            //if (m_bIsCamNotChange)
            //{
            //    m_bIsCamNotChange = false;
            //    return;
            //}

            //if (!m_bIsLoading)
            //{
            //    if (comboBox_cam_num.SelectedIndex != m_cvData.GetCameraNum())
            //    {
            //        if (MessageBox.Show(StringLib.Msg_VisionSettingChange, Text, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            m_cvData.SetCameraNum(comboBox_cam_num.SelectedIndex);
            //            m_cvData.GetCaptureProperty();
            //            SetControlValue();
            //        }
            //        else
            //        {
            //            m_bIsCamNotChange = true;
            //            comboBox_cam_num.SelectedIndex = m_cvData.GetCameraNum();
            //        }
            //    }                
            //}
            //else
            //{
            //    m_cvData.SetCameraNum(comboBox_cam_num.SelectedIndex);
            //    SetControlValue();
            //}
            
        }

        private void checkBox_use_vision_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_use_vision.Checked)
            {
                propertyGrid_vision_setting.Enabled = true;
            }
            else
            {
                propertyGrid_vision_setting.Enabled = false;
            }
        }

        private void timer_initial_setting_Tick(object sender, EventArgs e)
        {
            m_bIsReady = false;
            timer_initial_setting.Stop();

            if (comboBox_cam_num.Items.Count > m_cvData.GetCameraNum())
            {
                m_cvData.SetCaptureProperty();

                ChangeVisionSettingEvent();

                try
                {
                    if (m_cvData.GrabFromCamera() != null)
                    {
                        m_bIsReady = true;
                        MessageBox.Show(StringLib.Msg_VisionInitialComplete);
                        return;
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                }
            }

            timer_initial_setting.Start();
        }

        private void button_get_captureproperty_Click(object sender, EventArgs e)
        {
            m_cvData.GetCaptureProperty();
            propertyGrid_vision_setting.SelectedObject = m_cvData;
        }

        private void comboBox_vis_index_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                m_cvData = OpenCVData.GetInstance(comboBox_vis_index.SelectedIndex);
                comboBox_cam_num.SelectedIndex = m_cvData.GetCameraNum();
                propertyGrid_vision_setting.SelectedObject = m_cvData;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void comboBox_pixel_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int visionIndex = comboBox_vis_index.SelectedIndex;
                int camIndex = comboBox_cam_num.SelectedIndex;
                if (camIndex != -1 && visionIndex != -1)
                {
                    ResolutionList rl = OpenCVData.GetResolutionList(camIndex);
                    m_cvData = OpenCVData.GetInstance(visionIndex);
                    int resolutionIndex = comboBox_pixel_list.SelectedIndex;
                    if (resolutionIndex != -1)
                    {
                        m_cvData.CamPixel_Width = rl[resolutionIndex].Width;
                        m_cvData.CamPixel_Height = rl[resolutionIndex].Height;
                    }
                    else
                    {
                        m_cvData.CamPixel_Width = rl[0].Width;
                        m_cvData.CamPixel_Height = rl[0].Height;
                    }
                    
                }                
            }
            catch (Exception E)
            {                
                MessageBox.Show(E.Message);
            }
        }
    }
}
