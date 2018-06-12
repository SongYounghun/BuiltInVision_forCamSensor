using GalvoScanner.LaserVision.OpenCV;
using OpenCvSharp;
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

namespace GalvoScanner.LaserVision.DialogLaserVision
{
    public partial class ImageProcess_TemplateMatch : Form
    {
        OpenCVData m_cvData = null;
        LaserVisionControl m_visionControl = null;

        public ImageProcess_TemplateMatch(LaserVisionControl visionControl)
        {
            m_cvData = OpenCVData.GetInstance();
            m_visionControl = visionControl;

            InitializeComponent();
        }

        private void button_save_template_image_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "BMP file(*.bmp)|*.bmp|AllFile(*.*)|*.*";
                save.Title = "Save BMP file";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    Rectangle roi = m_visionControl.ProcessingImageViewer.ROIRegion;
                    if (roi.Width != 0 && roi.Height != 0)
                    {
                        GetTargetPosFromView();
                        if (m_cvData.GetProcessTeplateMatch().SaveTemplateImage(save.FileName, roi))
                        {
                            pictureBox_template_image.Image = BitmapConverter.ToBitmap(m_cvData.GetProcessTeplateMatch().GetIPLImageTemplate());
                            textBox_template_image_path.Text = m_cvData.GetProcessTeplateMatch().TemplateImagePath;
                        }
                        else
                        {
                            //MessageBox.Show(StringLib.Msg_VisionIsNotExistProcessingImage);
                        }
                    }
                    else
                    {
                        //MessageBox.Show(StringLib.Msg_VisionROInotSetting);
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_load_template_image_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "BMP file(*.bmp)|*.bmp|AllFile(*.*)|*.*";
                open.Title = "Open BMP file";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    if (m_cvData.GetProcessTeplateMatch().LoadTemplateImage(open.FileName))
                    {
                        IplImage templateImg = m_cvData.GetProcessTeplateMatch().GetIPLImageTemplate();
                        if (templateImg != null)
                        {
                            pictureBox_template_image.Image = BitmapConverter.ToBitmap(templateImg);
                            textBox_template_image_path.Text = m_cvData.GetProcessTeplateMatch().TemplateImagePath;
                        }
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_exec_tempate_match_Click(object sender, EventArgs e)
        {
            try
            {
                m_cvData.ResetResultImage();
                //m_cvData.ResetProcessImage();

                m_cvData.StringResult.Clear();
                    
                ProcessTemplateMatch.matchResultType resultType = m_cvData.GetProcessTeplateMatch().ExcuteTemplateMatch();
                if (resultType == ProcessTemplateMatch.matchResultType.OK)
                {
                    pictureBox_match_result.Image = BitmapConverter.ToBitmap(m_cvData.GetProcessTeplateMatch().GetIPLImageTemplateResult());
                    textBox_match_score.Text = String.Format("{0}", m_cvData.GetProcessTeplateMatch().MatchScore);                    
                    
                    m_visionControl.ProcessingImageViewer.RefreshImage();                
                }
                else if (resultType == ProcessTemplateMatch.matchResultType.LowScore)
                {
                    textBox_match_score.Text = String.Format("{0}", m_cvData.GetProcessTeplateMatch().MatchScore);                    
                    MessageBox.Show("Low template match score.");
                }
                else if (resultType == ProcessTemplateMatch.matchResultType.Shift)
                {
                    pictureBox_match_result.Image = BitmapConverter.ToBitmap(m_cvData.GetProcessTeplateMatch().GetIPLImageTemplateResult());
                    textBox_match_score.Text = String.Format("{0}", m_cvData.GetProcessTeplateMatch().MatchScore);                    
                    m_visionControl.ProcessingImageViewer.RefreshImage();

                    MessageBox.Show("Shift Error.");
                }

                MessageBox.Show(m_cvData.StringResult.ToString());
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void ImageProcess_TemplateMatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void button_targetPos_get_fromView_Click(object sender, EventArgs e)
        {
            try
            {
                GetTargetPosFromView();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void GetTargetPosFromView()
        {
            Point targetPosition = m_visionControl.ProcessingImageViewer.TargetPosition;
            Rectangle roi = m_visionControl.ProcessingImageViewer.ROIRegion;

            m_cvData.GetProcessTeplateMatch().TartgetOffsetPosition = new CvPoint(targetPosition.X - roi.X, targetPosition.Y - roi.Y);

            textBox_target_offsetX.Text = String.Format("{0}", m_cvData.GetProcessTeplateMatch().TartgetOffsetPosition.X);
            textBox_target_offsetY.Text = String.Format("{0}", m_cvData.GetProcessTeplateMatch().TartgetOffsetPosition.Y);
        }

        private void GetRoiRegion_forInspRange()
        {
            Rectangle roi = m_visionControl.ProcessingImageViewer.ROIRegion;
            textBox_insp_x.Text = roi.X.ToString();
            textBox_insp_y.Text = roi.Y.ToString();
            textBox_insp_w.Text = roi.Width.ToString();
            textBox_insp_h.Text = roi.Height.ToString();
        }

        private void button_button_targetPos_set_toView_Click(object sender, EventArgs e)
        {
            try
            {
                Rectangle roi = m_visionControl.ProcessingImageViewer.ROIRegion;
                int tartgetOffsetX = Convert.ToInt32(textBox_target_offsetX.Text);
                int tartgetOffsetY = Convert.ToInt32(textBox_target_offsetY.Text);

                m_cvData.GetProcessTeplateMatch().TartgetOffsetPosition = new CvPoint(tartgetOffsetX, tartgetOffsetY);

                m_visionControl.ProcessingImageViewer.SetTargetPosition(tartgetOffsetX + roi.X, tartgetOffsetY + roi.Y);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void button_add_templatematch_process_Click(object sender, EventArgs e)
        {
            try
            {
                m_visionControl.AddListProcess(OpenCVData.processType.TemplateMatch);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void textBox_threshold_socre_TextChanged(object sender, EventArgs e)
        {
            try
            {
               m_cvData.GetProcessTeplateMatch().ScoreThreshold = Convert.ToDouble(textBox_threshold_socre.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void ImageProcess_TemplateMatch_Load(object sender, EventArgs e)
        {
            
        }

        private void ImageProcess_TemplateMatch_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (Visible)
                {
                    UpdateData();

                    textBox_threshold_socre.Text = m_cvData.GetProcessTeplateMatch().ScoreThreshold.ToString();
                }                
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
                if (isLoad)
                {
                    textBox_target_offsetX.Text = m_cvData.GetProcessTeplateMatch().TartgetOffsetPosition.X.ToString();
                    textBox_target_offsetY.Text = m_cvData.GetProcessTeplateMatch().TartgetOffsetPosition.Y.ToString();
                    textBox_threshold_socre.Text = m_cvData.GetProcessTeplateMatch().ScoreThreshold.ToString();
                    textBox_insp_x.Text = m_cvData.GetProcessTeplateMatch().InspectionRect.X.ToString();
                    textBox_insp_y.Text = m_cvData.GetProcessTeplateMatch().InspectionRect.Y.ToString();
                    textBox_insp_w.Text = m_cvData.GetProcessTeplateMatch().InspectionRect.Width.ToString();
                    textBox_insp_h.Text = m_cvData.GetProcessTeplateMatch().InspectionRect.Height.ToString();                    
                    if (m_cvData.GetProcessTeplateMatch().LoadTemplateImage(m_cvData.GetProcessTeplateMatch().TemplateImagePath))
                    {
                        IplImage templateImg = m_cvData.GetProcessTeplateMatch().GetIPLImageTemplate();
                        if (templateImg != null)
                        {
                            pictureBox_template_image.Image = BitmapConverter.ToBitmap(templateImg);
                            textBox_template_image_path.Text = m_cvData.GetProcessTeplateMatch().TemplateImagePath;
                        }
                    }
                }
                else
                {
                    m_cvData.GetProcessTeplateMatch().TartgetOffsetPosition = new CvPoint(Convert.ToInt32(textBox_target_offsetX.Text),
                                                                                            Convert.ToInt32(textBox_target_offsetY.Text));
                    m_cvData.GetProcessTeplateMatch().ScoreThreshold = Convert.ToDouble(textBox_threshold_socre.Text);
                    m_cvData.GetProcessTeplateMatch().InspectionRect = new CvRect(Convert.ToInt32(textBox_insp_x.Text),
                                                                                Convert.ToInt32(textBox_insp_y.Text),
                                                                                Convert.ToInt32(textBox_insp_w.Text),
                                                                                Convert.ToInt32(textBox_insp_h.Text));                    
                    m_cvData.GetProcessTeplateMatch().TemplateImagePath = textBox_template_image_path.Text;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());                
            }
        }        

        private void button_apply_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateData(false);                

                MessageBox.Show("Completed apply.");

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void pictureBox_match_result_Click(object sender, EventArgs e)
        {
            ImageProcessViewer viewer = new ImageProcessViewer(m_cvData.GetProcessTeplateMatch().GetIPLImageTemplateResult());
            viewer.ShowDialog();
        }

        private void button_getfrom_insp_roi_Click(object sender, EventArgs e)
        {
            try
            {
                GetRoiRegion_forInspRange();
                button_apply_Click(this, EventArgs.Empty);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
    }
}
