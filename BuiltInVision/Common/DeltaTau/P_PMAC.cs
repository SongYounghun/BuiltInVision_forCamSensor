using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.Common.DeltaTau
{
    public class P_PMAC
    {
        static P_PMAC m_powerPmac = null;
        public static P_PMAC GetInstance()
        {
            if (m_powerPmac == null)
            {
                m_powerPmac = new P_PMAC();
            }

            return m_powerPmac;
        }

        string m_strIpAddress = "192.168.0.200";
        public string IPAddressPmac
        {
            get { return m_strIpAddress; }
            set { m_strIpAddress = value; }
        }

        uint m_uDeviceID = 0xFFFFFFFF;
        uint m_uReturnStatus = 0U;
        public DTK_STATUS PmacStatus
        {
            get { return (DTK_STATUS)m_uReturnStatus; }
        }

        bool m_bIsConnected = false;
        public bool IsConnectedC
        {
            get { return m_bIsConnected; }
        }

        public bool Connect()
        {
            try
            {
                UInt32 uIPAddress;
                String[] strIP = new String[4];

                strIP = m_strIpAddress.Split('.');
                uIPAddress = (Convert.ToUInt32(strIP[0]) << 24) | (Convert.ToUInt32(strIP[1]) << 16) | (Convert.ToUInt32(strIP[2]) << 8) | Convert.ToUInt32(strIP[3]);
                m_uDeviceID = PowerPmac.DTKPowerPmacOpen(uIPAddress, (uint)DTK_MODE_TYPE.DM_GPASCII);
                m_uReturnStatus = PowerPmac.DTKConnect(m_uDeviceID);
                if (m_uReturnStatus == (uint)DTK_STATUS.DS_Ok)
                {
                    m_bIsConnected = true;
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

        public bool IsConnected()
        {
            try
            {
                int isConnceted = -1;
                PowerPmac.DTKIsConnected(m_uDeviceID, out isConnceted);
                if (isConnceted <= 0 || !m_bIsConnected)
                    return false;
                else
                    return true;
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }

        public bool Disconnect()
        {
            try
            {
                if (IsConnected())
                {
                    m_uReturnStatus = PowerPmac.DTKDisconnect(m_uDeviceID);
                    if (m_uReturnStatus == (uint)DTK_STATUS.DS_Ok)
                    {
                        PowerPmac.DTKPowerPmacClose(m_uDeviceID);
                        m_bIsConnected = false;
                        return true;
                    }
                    return false;
                }

                return true;
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
            
        }

        public string GetRespons(string cmd)
        {
            try
            {
                if (IsConnected())
                {
                    byte[] respons = new byte[4096];
                    m_uReturnStatus = PowerPmac.DTKGetResponseA(m_uDeviceID, Encoding.ASCII.GetBytes(cmd), respons, 4096);
                    if (m_uReturnStatus == (uint)DTK_STATUS.DS_Ok)
                    {
                        return Encoding.ASCII.GetString(respons);
                    }
                    else if (m_uReturnStatus == (uint)DTK_STATUS.DS_USNotConnected)
                    {
                        m_bIsConnected = false;
                    }
                }
                return "";
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }
    }
}
