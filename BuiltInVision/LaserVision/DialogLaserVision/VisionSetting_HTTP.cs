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

namespace BuiltInVision.LaserVision.DialogLaserVision
{
    public partial class VisionSetting_HTTP : Form
    {
        OpenCVData m_cvData = OpenCVData.GetInstance(0, true);

        public VisionSetting_HTTP()
        {
            InitializeComponent();
        }

        private void VisionSetting_HTTP_Load(object sender, EventArgs e)
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

            Updatedata();
        }

        private void Updatedata(bool isLoad = true) 
        {
            int selIndex = comboBox_vis_index.SelectedIndex;
            if (selIndex != -1)
            {
                m_cvData = OpenCVData.GetInstance(selIndex, true);

                if (isLoad)
                {
                    checkBox_use_vision.Checked = m_cvData.GetUseVision();
                    textBox_address.Text = m_cvData.HTTP_Camera.Address;
                    textBox_stream_address.Text = m_cvData.HTTP_Camera.StreamAddress;
                    textBox_id.Text = m_cvData.HTTP_Camera.ID;
                    textBox_pw.Text = m_cvData.HTTP_Camera.PW;
                    numericUpDown_timeout.Value = Convert.ToDecimal(m_cvData.HTTP_Camera.TimeOut);
                }
                else
                {
                    m_cvData.SetUseVision(checkBox_use_vision.Checked);
                    m_cvData.HTTP_Camera.SetInit(textBox_address.Text,
                                                    textBox_stream_address.Text,
                                                    Convert.ToInt32(numericUpDown_timeout.Value),
                                                    textBox_id.Text,
                                                    textBox_pw.Text);
                }
            }            
        }

        private void timer_status_Tick(object sender, EventArgs e)
        {
            int selIndex = comboBox_vis_index.SelectedIndex;
            if (selIndex != -1)
            {
                m_cvData = OpenCVData.GetInstance(selIndex, true);
                if (m_cvData.HTTP_Camera.IsInitialize)
                {
                    textBox_status.Text = "Initialized.";
                }
                else
                {
                    textBox_status.Text = "Not Initialized.";
                }
            }
            
        }

        private void comboBox_vis_index_SelectedIndexChanged(object sender, EventArgs e)
        {
            Updatedata();
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            Updatedata(false);
        }
    }
}
