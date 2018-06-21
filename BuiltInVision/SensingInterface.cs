using GalvoScanner.Common;
using GalvoScanner.LaserVision.DialogLaserVision;
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

namespace BuiltInVision
{
    public partial class SensingInterface : Form
    {
        private string m_strINIPath = DefPath.ConfigPath + "\\SensingInterface.ini";
        private Proc_SensorInterface m_procSI = null;

        public SensingInterface()
        {
            m_procSI = Proc_SensorInterface.GetInstance();
            int interfaceCnt = Proc_SensorInterface.GetInterfaceCount();
            for (int i = 0; i < interfaceCnt; i++)
            {
                m_procSI = Proc_SensorInterface.GetInstance(i);
                m_procSI.LoadSettingINI(m_strINIPath);
            }

            InitializeComponent();
        }

        private void SensingInterface_Load(object sender, EventArgs e)
        {
            m_procSI = Proc_SensorInterface.GetInstance(0);
            int interfaceCnt = Proc_SensorInterface.GetInterfaceCount();
            for (int i = 0; i < interfaceCnt; i++)
            {   
                comboBox_sensor_index.Items.Add(i);
            }

            if (interfaceCnt > 0)
            {
                comboBox_sensor_index.SelectedIndex = 0;
            }
        }

        private void SensingInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void comboBox_sensor_index_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox_sensor_index.SelectedIndex;
            if (index != -1)
            {
                m_procSI = Proc_SensorInterface.GetInstance(index);
                propertyGrid_setting_sens_interface.SelectedObject = m_procSI;
                textBox_error_img_path.Text = m_procSI.GetErrorImagePath();
            }
        }

        private void propertyGrid_setting_sens_interface_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            m_procSI.SaveSettingINI(m_strINIPath);
        }

        private void button_start_proc_Click(object sender, EventArgs e)
        {
            int index = comboBox_sensor_index.SelectedIndex;
            if (index != -1)
            {
                m_procSI = Proc_SensorInterface.GetInstance(index);
                m_procSI.StartSensingInterface();
            }
        }

        private void button_stop_process_Click(object sender, EventArgs e)
        {
            int index = comboBox_sensor_index.SelectedIndex;
            if (index != -1)
            {
                m_procSI = Proc_SensorInterface.GetInstance(index);
                m_procSI.StopSensignInterface();
            }
        }

        private void button_start_all_proc_Click(object sender, EventArgs e)
        {
            AllStartProc();
        }

        private void button_stop_all_proc_Click(object sender, EventArgs e)
        {
            AllStopProc();
        }

        public void AllStartProc()
        {
            int interfaceCnt = Proc_SensorInterface.GetInterfaceCount();
            for (int i = 0; i < interfaceCnt; i++)
            {
                m_procSI = Proc_SensorInterface.GetInstance(i);
                m_procSI.StartSensingInterface();
            }

            int index = comboBox_sensor_index.SelectedIndex;
            if (index != -1)
            {
                m_procSI = Proc_SensorInterface.GetInstance(index);
            }
        }

        public void AllStopProc()
        {
            int interfaceCnt = Proc_SensorInterface.GetInterfaceCount();
            for (int i = 0; i < interfaceCnt; i++)
            {
                m_procSI = Proc_SensorInterface.GetInstance(i);
                m_procSI.StopSensignInterface();
            }

            int index = comboBox_sensor_index.SelectedIndex;
            if (index != -1)
            {
                m_procSI = Proc_SensorInterface.GetInstance(index);
            }
        }

        int m_nPreErrorCnt = 0;
        private void timer_monitor_Tick(object sender, EventArgs e)
        {
            int index = comboBox_sensor_index.SelectedIndex;
            if (index != -1)
            {
                m_procSI = Proc_SensorInterface.GetInstance(index);
                if (m_procSI.IsRunningProcess())
                {
                    label_status_proc.Text = "Process Running...";
                }
                else
                {
                    label_status_proc.Text = "Process Stopped.";
                }

                OpenCVData cvData = OpenCVData.GetInstance(index, true);                
                int nErrorCnt = cvData.GetListError().Count;
                if (nErrorCnt != m_nPreErrorCnt)
                {
                    listView_error.BeginUpdate();
                    listView_error.Items.Clear();
                    for (int i = 0; i < nErrorCnt; i++)
                    {
                        listView_error.Items.Add(cvData.GetListError()[i]);
                    }

                    listView_error.EndUpdate();

                    if (listView_error.Items.Count > 0)
                        listView_error.EnsureVisible(listView_error.Items.Count - 1);

                }
                m_nPreErrorCnt = nErrorCnt;
            }
        }

        private void button_show_vision_ui_Click(object sender, EventArgs e)
        {
            LaserVisionControl visionControl = LaserVisionControl.GetInstance();
            if (visionControl != null)
            {
                visionControl.GrabCamera();
                visionControl.OriginImageViewer.RefreshImage();
                visionControl.ProcessingImageViewer.RefreshImage();
                visionControl.ResultImageViewer.RefreshImage();
                visionControl.ShowResultViewer();
            }
        }

        private void button_clear_error_list_Click(object sender, EventArgs e)
        {
            int index = comboBox_sensor_index.SelectedIndex;
            if (index != -1)
            {
                OpenCVData cvData = OpenCVData.GetInstance(index, true);
                cvData.ClearListError();
            }
        }

        private void button_test_inputsig_Click(object sender, EventArgs e)
        {
            m_procSI.TestInputSignal();
        }

        private void button_err_img_path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_procSI.SetErrorImagePath(fbd.SelectedPath);
                textBox_error_img_path.Text = m_procSI.GetErrorImagePath();
                m_procSI.SaveSettingINI(m_strINIPath);
            }
        }
    }
}
