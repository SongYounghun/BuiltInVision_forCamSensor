using GalvoScanner.LaserVision.OpenCV;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalvoScanner.LaserVision.DialogLaserVision
{
    public partial class ImageProcessViewer : Form
    {
        OpenCVData m_cvData = null;
        LaserVisionControl m_laserVisionControl = null;
        bool m_bOriginalImage = false;
        double m_zoomCurr = 1;
        //bool m_bIsMouseDownLeft = false;
        bool m_bIsMouseDownRight = false;
        //Point m_ptMouseDownLeft = new Point();
        //Point m_ptMouseUpLeft = new Point();
        Point m_ptMouseDownRight = new Point();
        Point m_ptMouseUpRight = new Point();

        private double ZOOMFACTOR = 1.25;	// = 25% smaller or larger
        private int MINMAX = 5;				// 5 times bigger or smaller than the ctrl

        IplImage m_img = null;

        public ImageProcessViewer(OpenCVData cvData, LaserVisionControl laserVisionControl, bool originaImage)
        {
            m_cvData = cvData;
            m_laserVisionControl = laserVisionControl;
            m_bOriginalImage = originaImage;

            InitializeComponent();

            InitControl();
        }

        public ImageProcessViewer(IplImage img)
        {
            m_img = img;

            InitializeComponent();

            InitControl();
        }

        private void ImageProcessViewer_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void InitControl()
        {
            panel_outer.MouseEnter += new EventHandler(PicBox_MouseEnter);
            pictureBox_image.MouseEnter += new EventHandler(PicBox_MouseEnter);
            panel_outer.MouseWheel += new MouseEventHandler(PicBox_MouseWheel);
        }

        private void PicBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if ( e.Delta > 0 )
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }

        private void PicBox_MouseEnter(object sender, EventArgs e)
        {
            if (pictureBox_image.Focused == false)
            {
                pictureBox_image.Focus();
            }
        }

        private void ZoomIn()
        {
            if ((pictureBox_image.Width < (MINMAX * panel_outer.Width)) &&
                (pictureBox_image.Height < (MINMAX * panel_outer.Height)))
            {
                m_zoomCurr *= ZOOMFACTOR;
                pictureBox_image.Width = Convert.ToInt32(pictureBox_image.Image.Width * m_zoomCurr);
                pictureBox_image.Height = Convert.ToInt32(pictureBox_image.Image.Height * m_zoomCurr);
            }

            pictureBox_image.Invalidate();
        }

        private void ZoomOut()
        {
            if ((pictureBox_image.Width > (panel_outer.Width / MINMAX)) &&
                (pictureBox_image.Height > (panel_outer.Height / MINMAX)))
            {
                m_zoomCurr /= ZOOMFACTOR;
                pictureBox_image.Width = Convert.ToInt32(pictureBox_image.Image.Width * m_zoomCurr);
                pictureBox_image.Height = Convert.ToInt32(pictureBox_image.Image.Height * m_zoomCurr);
            }

            pictureBox_image.Invalidate();
        }

        private void ImageProcessViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void ImageProcessViewer_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_img == null)
                {
                    IplImage img = null;
                    if (m_bOriginalImage)
                    {
                        img = m_cvData.GetIPLImage();
                        pictureBox_image.Image = BitmapConverter.ToBitmap(img);
                    }
                    else
                    {
                        img = m_cvData.GetIPLImageProcessing();
                        pictureBox_image.Image = BitmapConverter.ToBitmap(img);
                    }
                }
                else
                {
                    pictureBox_image.Image = BitmapConverter.ToBitmap(m_img);
                }
                
                

                pictureBox_image.Width = pictureBox_image.Image.Width;
                pictureBox_image.Height = pictureBox_image.Image.Height;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void pictureBox_image_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("X : {0}, Y : {1}", Convert.ToInt32(e.X/m_zoomCurr), Convert.ToInt32(e.Y/m_zoomCurr));
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                m_bIsMouseDownRight = true;
                m_ptMouseDownRight.X = Convert.ToInt32(e.Location.X / m_zoomCurr);
                m_ptMouseDownRight.Y = Convert.ToInt32(e.Location.Y / m_zoomCurr);
                m_ptMouseUpRight.X = Convert.ToInt32(e.Location.X / m_zoomCurr);
                m_ptMouseUpRight.Y = Convert.ToInt32(e.Location.Y / m_zoomCurr);
            }
        }

        private void pictureBox_image_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                m_bIsMouseDownRight = false;
                m_ptMouseUpRight.X = Convert.ToInt32(e.Location.X / m_zoomCurr);
                m_ptMouseUpRight.Y = Convert.ToInt32(e.Location.Y / m_zoomCurr);
            }
        }

        private void pictureBox_image_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_bIsMouseDownRight)
            {
                m_ptMouseUpRight.X = Convert.ToInt32(e.Location.X / m_zoomCurr);
                m_ptMouseUpRight.Y = Convert.ToInt32(e.Location.Y / m_zoomCurr);
                pictureBox_image.Invalidate();
            }
        }

        private void pictureBox_image_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.FromArgb(0, 255, 0), 1);

            g.DrawRectangle(pen, Convert.ToInt32(m_ptMouseDownRight.X * m_zoomCurr),
                                    Convert.ToInt32(m_ptMouseDownRight.Y * m_zoomCurr),
                                    Convert.ToInt32((m_ptMouseUpRight.X - m_ptMouseDownRight.X) * m_zoomCurr),
                                    Convert.ToInt32((m_ptMouseUpRight.Y - m_ptMouseDownRight.Y) * m_zoomCurr));
        }
    }
}
