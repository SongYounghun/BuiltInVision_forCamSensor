using GalvoScanner.Common;
using GalvoScanner.Common.DeltaTau;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalvoScanner.IO
{
    public class DIOPmac : DIOBase, IDIO
    {
        public static string INISECTION_IO_SETUP = "PMAC_SETUP";
        public static string INIKEY_IPADDRESS = "IPADDRESS";
        public static string INIKEY_IN_CNT = "IN_CNT";
        public static string INIKEY_OUT_CNT = "OUT_CNT";
        public static string INIKEY_IN_MVAL = "IN_MVALUE";
        public static string INIKEY_OUT_MVAL = "OUT_MVALUE";


        private static DIOPmac m_dioPmac = null;
        public static DIOPmac GetInstance(bool isUseIO, KIND_DIO kindOfDIO)
        {
            m_bIsUseDIO = isUseIO;
            m_kindOfDIO = kindOfDIO;
            if (m_bIsUseDIO)
            {
                if (m_dioPmac == null)
                {
                    m_dioPmac = new DIOPmac();
                }

                return m_dioPmac;
            }

            return null;
        }

        public static DIOPmac GetInstance()
        {
            if (m_bIsUseDIO)
            {
                if (m_dioPmac == null)
                {
                    m_dioPmac = new DIOPmac();
                }

                return m_dioPmac;
            }

            return null;
        }

        P_PMAC m_pPmac = P_PMAC.GetInstance();

        public string IPAddressPmac
        {
            get { return m_pPmac.IPAddressPmac; }
            set { m_pPmac.IPAddressPmac = value; }
        }

        List<string> m_listInMVal = null;
        List<string> m_listOutMVal = null;

        System.Windows.Forms.Timer m_tmPMAC_ConnectionCheck = new System.Windows.Forms.Timer();

        public void InitDIO()
        {
            try
            {
                IniFile ini = new IniFile();
                string tempVal = "";
                tempVal = ini.IniReadValue(INISECTION_IO_SETUP, INIKEY_IPADDRESS, DefPath.IOMap);
                if (!String.IsNullOrEmpty(tempVal)) { IPAddressPmac = tempVal; }
                tempVal = ini.IniReadValue(INISECTION_IO_SETUP, INIKEY_IN_CNT, DefPath.IOMap);
                if (!String.IsNullOrEmpty(tempVal)) { m_nInputCount = Convert.ToInt32(tempVal); }
                tempVal = ini.IniReadValue(INISECTION_IO_SETUP, INIKEY_OUT_CNT, DefPath.IOMap);
                if (!String.IsNullOrEmpty(tempVal)) { m_nOutputCount = Convert.ToInt32(tempVal); }
                tempVal = ini.IniReadValue(INISECTION_IO_SETUP, INIKEY_IN_MVAL, DefPath.IOMap);
                if (!String.IsNullOrEmpty(tempVal))
                {
                    char firChar = tempVal[0];
                    int firVal = Convert.ToInt32(tempVal.Substring(1));                    
                    
                    if (m_nInputCount > 0)
                    {
                        m_listInMVal = new List<string>();
                        StringBuilder sb = new StringBuilder(10);
                        for (int i = 0; i < m_nInputCount; i++)
                        {
                            sb.Clear();
                            sb.Append(firChar); sb.Append(firVal + i);
                            m_listInMVal.Add(sb.ToString());
                        }
                    }
                }

                tempVal = ini.IniReadValue(INISECTION_IO_SETUP, INIKEY_OUT_MVAL, DefPath.IOMap);
                if (!String.IsNullOrEmpty(tempVal))
                {
                    char firChar = tempVal[0];
                    int firVal = Convert.ToInt32(tempVal.Substring(1));

                    if (m_nOutputCount > 0)
                    {
                        m_listOutMVal = new List<string>();
                        StringBuilder sb = new StringBuilder(10);
                        for (int i = 0; i < m_nOutputCount; i++)
                        {
                            sb.Clear();
                            sb.Append(firChar); sb.Append(firVal + i);
                            m_listOutMVal.Add(sb.ToString());
                        }
                    }
                }

                m_listInNames = new List<string>(m_nInputCount);
                for (int i = 0; i < m_nInputCount; i++)
                    m_listInNames.Add("");

                m_listOutNames = new List<string>(m_nOutputCount);
                for (int i = 0; i < m_nOutputCount; i++)
                    m_listOutNames.Add("");

                if (!m_pPmac.IsConnected())
                {
                    m_pPmac.Connect();
                }

                m_tmPMAC_ConnectionCheck.Interval = 1500;
                m_tmPMAC_ConnectionCheck.Tick += new EventHandler(CheckConnectionTimer);
                m_tmPMAC_ConnectionCheck.Start();
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }

        private void CheckConnectionTimer(object sender, EventArgs e)
        {
            if (!m_bIsUseDIO)
                return;

            if (m_pPmac != null && !m_pPmac.IsConnected())
            {
                m_tmPMAC_ConnectionCheck.Stop();
                if (MessageBox.Show("Disconnected PMAC. Do you try reconnect continue? If you select 'No', You have to re-excute program, if you want to connect PMAC.", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    m_pPmac.Connect();
                    m_tmPMAC_ConnectionCheck.Start();
                }
                else
                {
                    m_tmPMAC_ConnectionCheck.Stop();
                }
                
            }
        }

        public int GetInputCount()
        {
            return m_nInputCount;
        }

        public int GetOutputCount()
        {
            return m_nOutputCount;
        }

        public bool WriteOutBit(int index, uint value)
        {
            try
            {
                if (!m_bIsUseDIO)
                    return false;

                if (m_nOutputCount <= 0 || !m_pPmac.IsConnected() || index >= m_nOutputCount || index < 0)
                {
                    return false;
                }

                StringBuilder sb = new StringBuilder(30);
                sb.Append(m_listOutMVal[index]); sb.Append('='); sb.Append(value);                
                m_pPmac.GetRespons(sb.ToString());
                return true;
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }

        public bool ReadOutBit(int index, ref uint value)
        {
            try
            {
                if (!m_bIsUseDIO)
                    return false;

                if (m_nOutputCount <= 0 || !m_pPmac.IsConnected() || index >= m_nOutputCount || index < 0)
                {
                    return false;
                }

                StringBuilder sb = new StringBuilder(30);
                sb.Append(m_listOutMVal[index]);                
                string resp = m_pPmac.GetRespons(sb.ToString());
                sb.Append('=');
                if (resp.Contains(sb.ToString()))
                {
                    int outBit = Convert.ToInt16(resp.Replace(sb.ToString(), ""));
                    value = (uint)outBit;
                    return true;
                }

                return false;
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }

        public bool ReadInBit(int index, ref uint value)
        {
            try
            {
                if (!m_bIsUseDIO)
                    return false;

                if (m_nInputCount <= 0 || !m_pPmac.IsConnected() || index >= m_nInputCount || index < 0)
                {
                    return false;
                }

                StringBuilder sb = new StringBuilder(30);
                sb.Append(m_listInMVal[index]);                
                string resp = m_pPmac.GetRespons(sb.ToString());
                sb.Append('=');
                if (resp.Contains(sb.ToString()))
                {
                    int outBit = Convert.ToInt16(resp.Replace(sb.ToString(), ""));
                    value = (uint)outBit;
                    return true;
                }

                return false;
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }

        public void SaveIOMap(string path)
        {
            try
            {
                if (!m_bIsUseDIO)
                    return;

                if (m_nInputCount > 0 || m_nOutputCount > 0)
                {
                    IniFile ini = new IniFile();
                    for (int i = 0; i < m_nInputCount; i++)
                    {
                        ini.IniWriteValue(INISECTION_INPUT_MAP, String.Format("{0}", i), m_listInNames[i], path);
                    }

                    for (int i = 0; i < m_nOutputCount; i++)
                    {
                        ini.IniWriteValue(INISECTION_OUTPUT_MAP, String.Format("{0}", i), m_listOutNames[i], path);
                    }
                }
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }

        public void LoadIOMap(string path)
        {
            try
            {
                if (!m_bIsUseDIO)
                    return;

                if (m_nInputCount > 0 || m_nOutputCount > 0)
                {
                    IniFile ini = new IniFile();
                    string tempVal = "";
                    for (int i = 0; i < m_nInputCount; i++)
                    {
                        tempVal = ini.IniReadValue(INISECTION_INPUT_MAP, String.Format("{0}", i), path);
                        if (tempVal == "")
                        {
                            m_listInNames[i] = "";
                        }
                        else
                        {
                            m_listInNames[i] = tempVal;
                        }
                    }

                    for (int i = 0; i < m_nOutputCount; i++)
                    {
                        tempVal = ini.IniReadValue(INISECTION_OUTPUT_MAP, String.Format("{0}", i), path);
                        if (tempVal == "")
                        {
                            m_listOutNames[i] = "";
                        }
                        else
                        {
                            m_listOutNames[i] = tempVal;
                        }
                    }
                }
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }
    }
}
