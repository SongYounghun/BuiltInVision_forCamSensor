using BuiltInVision.LaserVision.OpenCV;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using GalvoScanner.Common;
using GalvoScanner.IO;
using GalvoScanner.IO.DialogIO;
using GalvoScanner.LaserCanvas.DialogLaserGalvo;
using GalvoScanner.LaserVision.DialogLaserVision;
using GalvoScanner.LaserVision.OpenCV;
using GalvoScanner.Utils;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuiltInVision
{
    public partial class MainForm : Form
    {
        OpenCVData m_cvData = null;
        LaserVisionControl m_visionControl = null;
        //LightControl m_LightControl = null;
        VisionSetting m_VisionSetting = null;
        IOControl m_ioControl = null;
        SensingInterface m_sensingInterface = null;

        public MainForm()
        {
            InitializeComponent();
            viewportLayout1.Unlock("EYEULT-9VQS-QUNSX-RM12U-SH0LL");

            OpenCVData.CreateOpenCvDataList();            
            m_cvData = OpenCVData.GetInstance();

            DIOBase.ReadIoINI(DefPath.MotionSetting);

            InitialVisionCtrl();
        }

        public EntityList LayoutEntityList
        {
            get { return viewportLayout1.Entities; }
            set { viewportLayout1.Entities = value; }
        }

        private void InitialVisionCtrl()
        {   
            if (m_visionControl == null)
            {
                m_visionControl = new LaserVisionControl();
                m_visionControl.SetLayout(viewportLayout1);
                m_visionControl.Dock = DockStyle.Fill;
                tabPage_vision.Controls.Add(m_visionControl);
                m_visionControl.Enabled = m_cvData.GetUseVision();
                m_visionControl.GrabFromCamera += new EventHandler(GrabFromVisionControl);
                m_visionControl.VisionVisibleCheckBox.CheckedChanged += new EventHandler(checkBox_vision_visible_CheckedChanged);
            }

            if (m_cvData.GetUseVision())
            {
                //if (m_LightControl == null)
                //{
                //    m_LightControl = new LightControl();
                //    tabPage_Light.Controls.Add(m_LightControl);
                //    m_LightControl.Enabled = m_cvData.GetUseVision();

                //    ResetGrid();
                //}
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            viewportLayout1.Entities.Parallel = true;

            NewViewportLayout();

            if (m_cvData != null)
            {
                if (m_VisionSetting == null)
                {
                    m_VisionSetting = new VisionSetting();

                    if (m_visionControl != null)
                    {
                        m_visionControl.StopContinuousGrab();
                    }

                    Picture picEnt = (Picture)LayoutEntityList[0];
                    if (m_cvData.VisionMode == OpenCVData.modeType.AreaTile)
                    {
                        picEnt.Width = m_cvData.CamFOV_Width * m_cvData.TileArray.X;
                        picEnt.Height = m_cvData.CamFOV_Height * m_cvData.TileArray.Y;
                    }
                    else
                    {
                        picEnt.Width = m_cvData.CamFOV_Width;
                        picEnt.Height = m_cvData.CamFOV_Height;
                    }

                    viewportLayout1.Entities.Regen();

                    Point2D currPt = new Point2D(picEnt.BoxMin.X, picEnt.BoxMin.Y);
                    Point2D pt = new Point2D(-(picEnt.Width / 2), -(picEnt.Height / 2));
                    picEnt.Translate(pt.X - currPt.X, pt.Y - currPt.Y, 0);

                    picEnt.Visible = m_cvData.GetUseVision();

                    viewportLayout1.Entities.Regen();
                    viewportLayout1.Invalidate();


                    if (!m_cvData.GetUseVision())
                    {
                        tabControl_properties.TabPages.RemoveAt(tabControl_properties.TabPages.IndexOfKey("tabPage_vision"));
                        //tabControl_properties.TabPages.RemoveAt(tabControl_properties.TabPages.IndexOfKey("tabPage_Light"));
                    }

                    m_VisionSetting.ChangedVisionSetting += new EventHandler(ChangedVisionSetting);
                    m_visionControl.ChangedVisionIndex += new EventHandler(onChangedVisionIndex);
                }
            }

            IDIO io = DIOBase.GetInstanceInterface();
            if (io == null)
            {
                iOControlToolStripMenuItem.Visible = false;
            }
            else
            {
                if (m_ioControl == null)
                {
                    m_ioControl = new IOControl();
                    m_ioControl.Show();
                    m_ioControl.Hide();
                }
            }

            if (m_sensingInterface == null)
            {
                m_sensingInterface = new SensingInterface();
            }
        }

        private void GrabFromVisionControl(object sender, EventArgs e)
        {
            try
            {                
                if (m_cvData != null && m_cvData.GetUseVision())
                {
                    //if (m_LightControl == null)
                    //{
                    //    m_LightControl = new LightControl();
                    //    tabPage_Light.Controls.Add(m_LightControl);
                    //    m_LightControl.Enabled = m_cvData.GetUseVision();
                    //}
                }

                Picture picEnt = (Picture)LayoutEntityList[0];
                if (m_cvData != null && m_cvData.GetIPLImage() != null)
                {
                    picEnt.Image = BitmapConverter.ToBitmap(m_cvData.GetIPLImage());
                    if (m_cvData.IsAutoSave && m_visionControl.IsLiveMode)
                    {
                        if (m_cvData.AutoSavePath == null || m_cvData.AutoSavePath == "")
                        {                            
                            m_cvData.GetIPLImage().SaveImage(Application.StartupPath.ToString() + "\\images" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".bmp");
                        }
                        else
                        {
                            m_cvData.GetIPLImage().SaveImage(m_cvData.AutoSavePath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".bmp");
                        }
                        
                    }
                }

                if (viewportLayout1.InvokeRequired)
                {
                    viewportLayout1.BeginInvoke((MethodInvoker)delegate()
                    {
                        viewportLayout1.Entities.Regen();
                        viewportLayout1.Invalidate();
                        //m_visionControl.IsReadyForGrab = true;
                    });
                }
                else
                {
                    viewportLayout1.Entities.Regen();
                    viewportLayout1.Invalidate();
                    //m_visionControl.IsReadyForGrab = true;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void ChangedVisionSetting(object sender, EventArgs e)
        {
            if (m_cvData != null)
            {
                m_visionControl.StopContinuousGrab();

                Picture picEnt = (Picture)LayoutEntityList[0];
                if (m_cvData.VisionMode == OpenCVData.modeType.AreaTile)
                {
                    picEnt.Width = m_cvData.CamFOV_Width * m_cvData.TileArray.X;
                    picEnt.Height = m_cvData.CamFOV_Height * m_cvData.TileArray.Y;
                }
                else
                {
                    picEnt.Width = m_cvData.CamFOV_Width;
                    picEnt.Height = m_cvData.CamFOV_Height;
                }

                viewportLayout1.Entities.Regen();

                Point2D currPt = new Point2D(picEnt.BoxMin.X, picEnt.BoxMin.Y);
                Point2D pt = new Point2D(-(picEnt.Width / 2), -(picEnt.Height / 2));
                picEnt.Translate(pt.X - currPt.X, pt.Y - currPt.Y, 0);

                picEnt.Visible = m_cvData.GetUseVision();

                viewportLayout1.Entities.Regen();
                viewportLayout1.Invalidate();

                if (m_cvData.GetUseVision())
                {
                    //if (m_LightControl == null)
                    //{
                    //    m_LightControl = new LightControl();
                    //    tabPage_Light.Controls.Add(m_LightControl);
                    //    m_LightControl.Enabled = m_cvData.GetUseVision();
                    //}
                }

                if (m_visionControl != null)
                {
                    if (m_cvData.GetUseVision())
                    {
                        if (tabControl_properties.TabPages.IndexOfKey("tabPage_vision") == -1)
                        {
                            tabControl_properties.TabPages.Insert(0, "tabPage_vision", "Vision");
                            tabControl_properties.TabPages[tabControl_properties.TabPages.IndexOfKey("tabPage_vision")].Controls.Add(m_visionControl);
                        }

                        //if (tabControl_properties.TabPages.IndexOfKey("tabPage_Light") == -1)
                        //{
                        //    tabControl_properties.TabPages.Insert(1, "tabPage_Light", "Light");
                        //    tabControl_properties.TabPages[tabControl_properties.TabPages.IndexOfKey("tabPage_Light")].Controls.Add(m_LightControl);
                        //}
                    }
                    else
                    {
                        if (tabControl_properties.TabPages.IndexOfKey("tabPage_vision") != -1)
                        {
                            tabControl_properties.TabPages.RemoveAt(tabControl_properties.TabPages.IndexOfKey("tabPage_vision"));
                        }
                    }
                }

                m_visionControl.Enabled = m_cvData.GetUseVision();

                ResetGrid();
            }
        }

        private void checkBox_vision_visible_CheckedChanged(object sender, EventArgs e)
        {
            if (m_cvData.GetUseVision())
            {
                Picture picEnt = (Picture)LayoutEntityList[0];
                picEnt.Visible = m_visionControl.VisionVisibleCheckBox.Checked;

                viewportLayout1.Entities.Regen();
                viewportLayout1.Invalidate();
            }
        }

        private void SetImageToEntity()
        {
            try
            {
                IplImage pImage = Cv.CreateImage(new CvSize(m_cvData.CamPixel_Width, m_cvData.CamPixel_Height), BitDepth.U8, 1);
                Point3D pt = new Point3D((m_cvData.CamFOV_Width * m_cvData.TileArray.X / 2) * -1, (m_cvData.CamFOV_Height * m_cvData.TileArray.X / 2) * -1, -0.001);
                Picture pict = new Picture(viewportLayout1.GetPlane(), pt, m_cvData.CamFOV_Width * m_cvData.TileArray.X, m_cvData.CamFOV_Height * m_cvData.TileArray.X, BitmapConverter.ToBitmap(pImage));
                pict.Visible = m_cvData.GetUseVision();
                pict.Selectable = false;
                pict.Lighted = false;
                pict.DrawEdge = false;
                pict.Regen(0.001);
                viewportLayout1.Entities.Add(pict);

                viewportLayout1.Entities.Regen();
                viewportLayout1.Invalidate();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void NewViewportLayout()
        {
            if (m_visionControl != null)
            {
                m_visionControl.StopContinuousGrab();
            }
            
            viewportLayout1.Clear();

            SetImageToEntity();

            if (m_visionControl != null)
            {
                Picture picEnt = (Picture)LayoutEntityList[0];
                if (m_cvData.VisionMode == OpenCVData.modeType.AreaTile)
                {
                    picEnt.Width = m_cvData.CamFOV_Width * m_cvData.TileArray.X;
                    picEnt.Height = m_cvData.CamFOV_Height * m_cvData.TileArray.Y;
                }
                else
                {
                    picEnt.Width = m_cvData.CamFOV_Width;
                    picEnt.Height = m_cvData.CamFOV_Height;
                }
                viewportLayout1.Entities.Regen();

                Point2D currPt = new Point2D(picEnt.BoxMin.X, picEnt.BoxMin.Y);
                Point2D pt = new Point2D(-(picEnt.Width / 2), -(picEnt.Height / 2));
                picEnt.Translate(pt.X - currPt.X, pt.Y - currPt.Y, 0);

                picEnt.Visible = m_cvData.GetUseVision();

                viewportLayout1.Entities.Regen();
                viewportLayout1.Invalidate();

                m_visionControl.Enabled = m_cvData.GetUseVision();
            }

            viewportLayout1.Layers[0].LineWeight = 1;

            viewportLayout1.Invalidate();

        }

        private void visionSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_VisionSetting == null)
            {
                m_VisionSetting = new VisionSetting();
                m_VisionSetting.ChangedVisionSetting += new EventHandler(ChangedVisionSetting);
            }

            m_VisionSetting.Show();
            m_VisionSetting.Focus();

            m_visionControl.StopContinuousGrab();
        }

        private void gridSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridSettingForm gridSetting = new GridSettingForm(viewportLayout1.Grid.Step);
            if (gridSetting.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                viewportLayout1.Grid.Step = gridSetting.GridStep;
                ResetGrid();
                viewportLayout1.Invalidate();
            }
        }

        private void ResetGrid()
        {
            if (m_cvData == null)
                return;

            viewportLayout1.Grid.Max = new Point2D(m_cvData.CamFOV_Width / 2, m_cvData.CamFOV_Height / 2);
            viewportLayout1.Grid.Min = new Point2D(-(m_cvData.CamFOV_Width / 2), -(m_cvData.CamFOV_Height / 2));
            viewportLayout1.Grid.MaxNumberOfLines = (int)((m_cvData.CamFOV_Width / 2 / viewportLayout1.Grid.Step));
            viewportLayout1.Grid.MinNumberOfLines = (int)(-(m_cvData.CamFOV_Height / 2 / viewportLayout1.Grid.Step));
        }

        private void gridSnapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.Checked = !item.Checked;
            viewportLayout1.gridSnapEnabled = item.Checked;
            viewportLayout1.Invalidate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to quit program?", "", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (m_visionControl != null)
            {
                m_visionControl.StopContinuousGrab();
            }

            if (m_sensingInterface != null)
            {
                m_sensingInterface.AllStopProc();
            }

            USBCamControl usbCam = USBCamControl.GetInstance();
            usbCam.CloseCamera();
        }

        private void onChangedVisionIndex(object sender, EventArgs e)
        {
            m_cvData = OpenCVData.GetInstance();
        }

        private void iOControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ioControl == null)
            {
                m_ioControl = new IOControl();
            }

            m_ioControl.Show();
            m_ioControl.Focus();
        }

        private void iOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_sensingInterface == null)
            {
                m_sensingInterface = new SensingInterface();
            }

            m_sensingInterface.Show();
            m_sensingInterface.Focus();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon_camsensor.Visible = true;
                Hide();
            }
            else
            {
                notifyIcon_camsensor.Visible = false;
                ShowInTaskbar = true;
            }
            
        }

        private void notifyIcon_camsensor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}
