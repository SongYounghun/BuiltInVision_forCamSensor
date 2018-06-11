using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CyUSB;
using System.Threading;

namespace GalvoScanner.Utils
{
    public partial class LightControl : UserControl
    {
        private bool m_bAbleClose = false;
        public bool AbleClose
        {
            get { return m_bAbleClose; }
            set { m_bAbleClose = value; }
        }

        USBDeviceList m_USBDevice;
        CyUSBDevice m_device;

        public LightControl()
        {
            InitializeComponent();
            OpenUSB();

            if (checkBox_totalControl.Checked)
            {
                for (int i = 0; i < 8; i++)
                {
                    TextBox txb = (TextBox)Controls.Find(String.Format("textBox_light_value{0}", i + 1), true)[0];
                    TrackBar trb = (TrackBar)Controls.Find(String.Format("trackBar_light_value{0}", i + 1), true)[0];
                    txb.Enabled = false;
                    trb.Enabled = false;
                }
                textBox_light_value_total.Enabled = true;
                trackBar_light_value_total.Enabled = true;
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    TextBox txb = (TextBox)Controls.Find(String.Format("textBox_light_value{0}", i + 1), true)[0];
                    TrackBar trb = (TrackBar)Controls.Find(String.Format("trackBar_light_value{0}", i + 1), true)[0];
                    txb.Enabled = true;
                    trb.Enabled = true;
                }
                textBox_light_value_total.Enabled = false;
                trackBar_light_value_total.Enabled = false;
            }
        }

        ~LightControl()
        {
            OffLight();
        }

        private void IlluminationForm_Load(object sender, EventArgs e)
        {
            
        }

        void OpenUSB()
        {
            try 
            { 
                m_USBDevice = new USBDeviceList(CyConst.DEVICES_CYUSB); 
            }
            catch (Exception e) 
            {
                MessageBox.Show(e.Message.ToString()); return; 
            }

            if (m_USBDevice == null) 
            { 
                MessageBox.Show("OpenLight - Device Not Found"); 
                return; 
            }
            m_device = m_USBDevice[0x0547, 0x1003] as CyUSBDevice;
            if (m_device == null) 
            {
                MessageBox.Show("OpenLight - Light Board not Found"); 
                return;
            }
        }

        byte ReadPort(ushort wIndex)
        {
            CyControlEndPoint pEP; int nLength = 4;
            byte[] buf = new byte[4] { 0, 0, 0, 0 };
            if (m_device == null) return 0;
            pEP = m_device.ControlEndPt;
            pEP.Target = CyConst.TGT_DEVICE;
            pEP.ReqType = CyConst.REQ_VENDOR;
            pEP.Direction = CyConst.DIR_FROM_DEVICE;
            pEP.ReqCode = 0x81;
            pEP.Value = 0;
            pEP.Index = wIndex;
            Thread.Sleep(1);
            if (pEP.XferData(ref buf, ref nLength)) return buf[0]; else return 0;
        }

        bool WritePort(ushort wIndex, ushort wValue)
        {
            CyControlEndPoint pEP; int nLength = 0;
            byte[] buf = new byte[4] { 0, 0, 0, 0 };
            if (m_device == null) return false;
            pEP = m_device.ControlEndPt;
            pEP.Target = CyConst.TGT_DEVICE;
            pEP.ReqType = CyConst.REQ_VENDOR;
            pEP.Direction = CyConst.DIR_TO_DEVICE;
            pEP.ReqCode = 0x80;
            pEP.Value = wValue;
            pEP.Index = wIndex;
            Thread.Sleep(1);
            try
            {
                return pEP.XferData(ref buf, ref nLength);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void IlluminationForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {                
                TrackBar trackbar = (TrackBar)sender;
                ushort deviceNum = (ushort)Int32.Parse(trackbar.Tag.ToString());
                ushort value = (ushort)trackbar.Value;
                WritePort(deviceNum, (ushort)value);

                TextBox tb = (TextBox)Controls.Find(String.Format("textBox_light_value{0}", deviceNum + 1), true)[0];
                tb.Text = String.Format("{0}", value);

                if (m_USBDevice.Count > 0 && m_device != null)
                {
                    WritePort(deviceNum, (ushort)value);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void textBox_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                ushort deviceNum = (ushort)Int32.Parse(textBox.Tag.ToString());
                ushort value = (ushort)Int32.Parse(textBox.Text);

                TrackBar tb = (TrackBar)Controls.Find(String.Format("trackBar_light_value{0}", deviceNum + 1), true)[0];
                tb.Value = value;

                if (m_USBDevice.Count > 0 && m_device != null)
                {
                    WritePort(deviceNum, (ushort)value);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void IlluminationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_totalControl_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_totalControl.Checked)
            {
                for (int i = 0; i < 8; i++)
                {
                    TextBox txb = (TextBox)Controls.Find(String.Format("textBox_light_value{0}", i + 1), true)[0];
                    TrackBar trb = (TrackBar)Controls.Find(String.Format("trackBar_light_value{0}", i + 1), true)[0];
                    txb.Enabled = false;
                    trb.Enabled = false;
                }
                textBox_light_value_total.Enabled = true;
                trackBar_light_value_total.Enabled = true;
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    TextBox txb = (TextBox)Controls.Find(String.Format("textBox_light_value{0}", i + 1), true)[0];
                    TrackBar trb = (TrackBar)Controls.Find(String.Format("trackBar_light_value{0}", i + 1), true)[0];
                    txb.Enabled = true;
                    trb.Enabled = true;
                }
                textBox_light_value_total.Enabled = false;
                trackBar_light_value_total.Enabled = false;
            }
        }

        private void trackBar_light_value_total_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ushort value = (ushort)trackBar_light_value_total.Value;
                textBox_light_value_total.Text = String.Format("{0}", value);

                for (int i = 0; i < 8; i++)
                {
                    TextBox txb = (TextBox)Controls.Find(String.Format("textBox_light_value{0}", i + 1), true)[0];
                    TrackBar trb = (TrackBar)Controls.Find(String.Format("trackBar_light_value{0}", i + 1), true)[0];
                    txb.Text = String.Format("{0}", value);
                    trb.Value = value;
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void textBox_light_value_total_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ushort value = (ushort)Int32.Parse(textBox_light_value_total.Text);
                trackBar_light_value_total.Value = value;

                for (int i = 0; i < 8; i++)
                {
                    TextBox txb = (TextBox)Controls.Find(String.Format("textBox_light_value{0}", i + 1), true)[0];
                    TrackBar trb = (TrackBar)Controls.Find(String.Format("trackBar_light_value{0}", i + 1), true)[0];
                    txb.Text = String.Format("{0}", value);
                    trb.Value = value;
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void button_light_on_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    TrackBar tb = (TrackBar)Controls.Find(String.Format("trackBar_light_value{0}", i + 1), true)[0];
                    ushort value = (ushort)tb.Value;

                    if (m_USBDevice.Count > 0 && m_device != null)
                    {
                        WritePort((ushort)i, (ushort)value);
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void button_light_off_Click(object sender, EventArgs e)
        {
            OffLight();
        }

        public void OffLight()
        {
            for (int i = 0; i < 8; i++)
            {
                if (m_USBDevice.Count > 0 && m_device != null)
                {
                    WritePort((ushort)i, (ushort)0);
                }
            }
        }
    }
}
