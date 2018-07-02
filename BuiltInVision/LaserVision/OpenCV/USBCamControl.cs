using Camera_NET;
using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuiltInVision.LaserVision.OpenCV
{
    public class USBCamControl
    {
        private static USBCamControl m_usbCam = null;
        public static USBCamControl GetInstance()
        {
            if (m_usbCam == null)
            {
                m_usbCam = new USBCamControl();
            }

            return m_usbCam;
        }

        private CameraChoice _CameraChoice = new CameraChoice();
        private List<Camera> m_listCamera = new List<Camera>();
        private List<UserControl> m_listUserControl = new List<UserControl>();
        private List<bool> m_listIsRun = new List<bool>();

        public USBCamControl()
        {
            _CameraChoice.UpdateDeviceList();
            for (int i = 0; i < _CameraChoice.Devices.Count; i++)
            {
                try
                {
                    IMoniker moniker = _CameraChoice.Devices[i].Mon;
                    Camera cam = new Camera();
                    UserControl uc = new UserControl();
                    ResolutionList rl = Camera.GetResolutionList(_CameraChoice.Devices[i].Mon);
                    cam.Resolution = rl[0];
                    cam.Initialize(uc, moniker);

                    Thread.Sleep(200);
                    cam.BuildGraph();
                    try
                    {
                        cam.RunGraph();
                        m_listIsRun.Add(true);
                    }
                    catch (Exception)
                    {
                        cam.StopGraph();
                        m_listIsRun.Add(false);
                    }

                    m_listCamera.Add(cam);
                    m_listUserControl.Add(uc);

                }
                catch (Exception)
                {

                }
            }
        }

        public void InitialAllCam()
        {

        }
                
        public Bitmap SnapShotCam(int camIndex)
        {
            if (!m_listIsRun[camIndex])
            {
                for (int i = 0; i < m_listCamera.Count; i++)
                {
                    if (m_listIsRun[i] && (_CameraChoice.Devices[i].Name == _CameraChoice.Devices[camIndex].Name))
                    {
                        m_listCamera[i].StopGraph();
                        m_listIsRun[i] = false;
                        Thread.Sleep(200);
                    }
                }

                m_listCamera[camIndex].RunGraph();
                m_listIsRun[camIndex] = true;
            }

            Bitmap bmp = m_listCamera[camIndex].SnapshotSourceImage();

            return bmp;
        }

        public void StopGraph(int camIndex)
        {
            m_listCamera[camIndex].StopGraph();
        }

        public List<DsDevice> GetDevList()
        {
            return _CameraChoice.Devices;
        }

        public void CloseCamera()
        {
            for (int i = 0; i < m_listCamera.Count; i++)
            {
                m_listCamera[i].StopGraph();
                Thread.Sleep(50);
            }

            Thread.Sleep(300);
            m_listCamera[0].CloseAll();

            m_listCamera.Clear();
            m_listIsRun.Clear();
            m_listUserControl.Clear();

            _CameraChoice.Dispose();
        }

        public ResolutionList GetResolutionList(int camIndex)
        {
            return Camera.GetResolutionList(_CameraChoice.Devices[camIndex].Mon);
        }
    }
}
