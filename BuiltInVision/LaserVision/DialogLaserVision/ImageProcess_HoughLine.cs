using BuiltInVision.LaserVision.OpenCV;
using GalvoScanner.LaserVision.DialogLaserVision;
using GalvoScanner.LaserVision.OpenCV;
using OpenCvSharp.Extensions;
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
    public partial class ImageProcess_HoughLine : Form
    {
        OpenCVData m_cvData = null;
        LaserVisionControl m_visionControl = null;

        public ImageProcess_HoughLine(LaserVisionControl visionControl)
        {
            m_cvData = OpenCVData.GetInstance();
            m_visionControl = visionControl;

            InitializeComponent();
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            UpdateData(false);
            MessageBox.Show("Completed apply.");
        }

        public void UpdateData(bool isLoad = true)
        {
            try
            {
                if (isLoad)
                {
                    textBox_bin_thresold.Text = m_cvData.GetProcessHoughLine().BinaryThreshold.ToString();
                    textBox_can_thresold1.Text = m_cvData.GetProcessHoughLine().CannyThreshold1.ToString();
                    textBox_can_thresold2.Text = m_cvData.GetProcessHoughLine().CannyThreshold2.ToString();
                    textBox_hough_thresold.Text = m_cvData.GetProcessHoughLine().HoughLineThreshold.ToString();                    
                    textBox_tilt.Text = m_cvData.GetProcessHoughLine().Tilt.ToString();
                    textBox_tilt_range.Text = m_cvData.GetProcessHoughLine().TiltRange.ToString();
                    checkBox_insp_in_result_roi.Checked = m_cvData.GetProcessHoughLine().ResultInROI;
                }
                else
                {
                    m_cvData.GetProcessHoughLine().BinaryThreshold = Convert.ToInt32(textBox_bin_thresold.Text);
                    m_cvData.GetProcessHoughLine().CannyThreshold1 = Convert.ToInt32(textBox_can_thresold1.Text);
                    m_cvData.GetProcessHoughLine().CannyThreshold2 = Convert.ToInt32(textBox_can_thresold2.Text);
                    m_cvData.GetProcessHoughLine().HoughLineThreshold = Convert.ToInt32(textBox_hough_thresold.Text);
                    m_cvData.GetProcessHoughLine().Tilt = Convert.ToDouble(textBox_tilt.Text);
                    m_cvData.GetProcessHoughLine().TiltRange = Convert.ToDouble(textBox_tilt_range.Text);
                    m_cvData.GetProcessHoughLine().ResultInROI = checkBox_insp_in_result_roi.Checked;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void ImageProcess_HoughLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void ImageProcess_HoughLine_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (Visible)
                {
                    UpdateData();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void ImageProcess_HoughLine_Load(object sender, EventArgs e)
        {
            m_visionControl.ChangedVisionIndex += new EventHandler(onChangedVisionIndex);
        }

        private void button_exec_hough_line_Click(object sender, EventArgs e)
        {
            m_cvData.ResetResultImage();
            //m_cvData.ResetProcessImage();

            m_cvData.StringResult.Clear();

            ProcessHoughLine.houghResultType resultType = m_cvData.GetProcessHoughLine().ExecuteHoughLines();
            switch (resultType)
            {
                case ProcessHoughLine.houghResultType.NoProcessImage:
                    {
                        MessageBox.Show("There is no process image!");
                    }
                    break;

                case ProcessHoughLine.houghResultType.Tilt:
                    {
                        pictureBox_hough_result.Image = BitmapConverter.ToBitmap(m_cvData.GetProcessHoughLine().GetIPLImageHoughResult());
                        MessageBox.Show("Over tilt angle!");
                    }
                    break;

                case ProcessHoughLine.houghResultType.OK:
                    {
                        pictureBox_hough_result.Image = BitmapConverter.ToBitmap(m_cvData.GetProcessHoughLine().GetIPLImageHoughResult());
                    }
                    break;
            }

            MessageBox.Show(m_cvData.StringResult.ToString());
        }

        private void button_add_houghline_process_Click(object sender, EventArgs e)
        {
            try
            {
                m_visionControl.AddListProcess(OpenCVData.processType.HoughLine);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void pictureBox_hough_result_DoubleClick(object sender, EventArgs e)
        {
            ImageProcessViewer viewer = new ImageProcessViewer(m_cvData.GetProcessHoughLine().GetIPLImageHoughResult());
            viewer.ShowDialog();
        }

        private void onChangedVisionIndex(object sender, EventArgs e)
        {
            m_cvData = OpenCVData.GetInstance();
        }
    }
}
