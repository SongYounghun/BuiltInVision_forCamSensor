using GalvoScanner.LaserVision.OpenCV;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
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
    public partial class ImageProcess_FitCircle : Form
    {
        OpenCVData m_cvData = null;
        LaserVisionControl m_visionControl = null;
        ProcessFitCircle m_procFitCircle = null;

        public ImageProcess_FitCircle(LaserVisionControl visionControl)
        {
            m_cvData = OpenCVData.GetInstance();
            m_visionControl = visionControl;
            if (m_cvData != null)
            {
                m_procFitCircle = m_cvData.GetProcessFitCircle();
            }            

            InitializeComponent();
        }

        private void ImageProcess_FitCircle_Load(object sender, EventArgs e)
        {
            foreach (ColorType type in Enum.GetValues(typeof(ColorType)))
            {
                cbColorType.Items.Add(type);
            }

            m_visionControl.ChangedVisionIndex += new EventHandler(onChangedVisionIndex);
        }

        private void ImageProcess_FitCircle_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (Visible)
                {
                    OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();

                    cbEnabled.SelectedIndex = Convert.ToInt32(para.OperationEnabled);
                    cbAbsoluteGV.SelectedIndex = Convert.ToInt32(para.AbsoluteGVEnabled);
                    cbSearchDirection.SelectedIndex = Convert.ToInt32(para.SearchDirection);
                    cbCircleFitting.SelectedIndex = Convert.ToInt32(para.CircleFittingEnabled);
                    cbColorType.SelectedItem = para.TargetColor;

                    tbSearchStart.Text = para.SearchStart.ToString();
                    tbSearchLength.Text = para.SearchLength.ToString();
                    tbSearchGV.Text = para.SearchGV.ToString();
                    tbThreshold.Text = para.Threshold.ToString();
                    tbMedianFilterAperture.Text = para.MedianFilterAperture.ToString();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void ImageProcess_FitCircle_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void button_inspect_fit_circle_Click(object sender, EventArgs e)
        {
            try
            {
                m_visionControl.ProcessingImageViewer.Hide();
                
                m_procFitCircle.Inspect();
                
                m_visionControl.ProcessingImageViewer.Focus();
                m_visionControl.ProcessingImageViewer.Show();
                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void cbEnabled_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.OperationEnabled = Convert.ToBoolean(cbEnabled.SelectedIndex);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void cbAbsoluteGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.AbsoluteGVEnabled = Convert.ToBoolean(cbAbsoluteGV.SelectedIndex);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void cbSearchDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.SearchDirection = (OverlaySearchDirection)cbSearchDirection.SelectedIndex;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void cbCircleFitting_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.CircleFittingEnabled = Convert.ToBoolean(cbCircleFitting.SelectedIndex);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void cbColorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.TargetColor = (ColorType)cbColorType.SelectedIndex;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void tbSearchStart_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.SearchStart = Convert.ToInt32(tbSearchStart.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void tbSearchLength_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.SearchLength = Convert.ToInt32(tbSearchLength.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void tbSearchGV_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.SearchGV = Convert.ToByte(tbSearchGV.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void tbThreshold_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.Threshold = Convert.ToDouble(tbThreshold.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void tbMedianFilterAperture_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();
                para.MedianFilterAperture = Convert.ToInt32(tbMedianFilterAperture.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_add_fitcircle_process_Click(object sender, EventArgs e)
        {
            try
            {
                m_visionControl.AddListProcess(OpenCVData.processType.FitCircle);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        public void UpdateData(bool isLoad = true)
        {
            try
            {
                OverlayUnitParameter para = m_procFitCircle.GetOverayUnitParameter();

                if (isLoad)
                {
                    cbEnabled.SelectedIndex = Convert.ToInt32(para.OperationEnabled);
                    cbAbsoluteGV.SelectedIndex = Convert.ToInt32(para.AbsoluteGVEnabled);
                    cbSearchDirection.SelectedIndex = Convert.ToInt32(para.SearchDirection);
                    cbCircleFitting.SelectedIndex = Convert.ToInt32(para.CircleFittingEnabled);
                    cbColorType.SelectedIndex = Convert.ToInt32(para.TargetColor);

                    tbSearchStart.Text = para.SearchStart.ToString();
                    tbSearchLength.Text = para.SearchLength.ToString();
                    tbSearchGV.Text = para.SearchGV.ToString();
                    tbThreshold.Text = para.Threshold.ToString();
                    tbMedianFilterAperture.Text = para.MedianFilterAperture.ToString();
                }
                else
                {
                    para.OperationEnabled = Convert.ToBoolean(cbEnabled.SelectedIndex);
                    para.AbsoluteGVEnabled = Convert.ToBoolean(cbAbsoluteGV.SelectedIndex);
                    para.SearchDirection = (OverlaySearchDirection)cbSearchDirection.SelectedIndex;
                    para.CircleFittingEnabled = Convert.ToBoolean(cbCircleFitting.SelectedIndex);
                    para.TargetColor = (ColorType)cbColorType.SelectedIndex;

                    para.SearchStart = Convert.ToInt32(tbSearchStart.Text);
                    para.SearchLength = Convert.ToInt32(tbSearchLength.Text);
                    para.SearchGV = Convert.ToByte(tbSearchGV.Text);
                    para.Threshold = Convert.ToByte(tbSearchGV.Text);
                    para.MedianFilterAperture = Convert.ToInt32(tbMedianFilterAperture.Text);
                }
                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
                throw;
            }
        }

        private void onChangedVisionIndex(object sender, EventArgs e)
        {
            m_cvData = OpenCVData.GetInstance();
        }
    }
}
