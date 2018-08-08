using GalvoScanner;
using GalvoScanner.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.LaserVision.OpenCV
{
    public class HTTP_Cam
    {
        static private HTTP_Cam m_http_cam = null;
        static public HTTP_Cam GetInstance()
        {
            if (m_http_cam == null)
            {
                m_http_cam = new HTTP_Cam();
            }

            return m_http_cam;
        }

        static public string INISECT_HTTP = "HTTP_SETTING";
        static public string INIKEY_ADDRESS = "ADDRESS";
        static public string INIKEY_STREAM_ADDR = "STREAM_ADDRESS";
        static public string INIKEY_TIMEOUT = "TIMEOUT";
        static public string INIKEY_ID = "ID";
        static public string INIKEY_PW = "PW";

        private string m_strAddress = "";
        public string Address { get { return m_strAddress; } }

        private string m_strStreamAddress = "";
        public string StreamAddress { get { return m_strStreamAddress; } }

        private string m_strID = "";
        public string ID { get { return m_strID; } }

        private string m_strPW = "";
        public string PW { get { return m_strPW; } }

        private int m_nTimeOut = 1500;
        public int TimeOut { get { return m_nTimeOut; } }

        private string m_strSavePath = "C:";
        public string SavePath
        {
            get { return m_strSavePath; }
            set { m_strSavePath = value; }
        }


        int m_nIndex = -1;

        HttpClient m_httpClient;
        WebRequestHandler m_webHandler;

        bool m_bIsInit = false;
        public bool IsInitialize { get { return m_bIsInit; } }

        public HTTP_Cam(int index = -1)
        {
            m_nIndex = index;
        }

        public bool SetInit(string address, string stream_address, int timeout, string id, string pw)
        {
            try
            {
                m_webHandler = new WebRequestHandler();
                m_webHandler.Credentials = new NetworkCredential(id, pw);
                m_httpClient = new HttpClient(m_webHandler);
                m_httpClient.Timeout = new TimeSpan(timeout * TimeSpan.TicksPerMillisecond);
                m_httpClient.BaseAddress = new Uri(address);

                Stream stream = m_httpClient.GetStreamAsync(m_strStreamAddress).Result;

                m_strAddress = address;
                m_strStreamAddress = stream_address;
                m_nTimeOut = timeout;
                m_strID = id;
                m_strPW = pw;

                IniFile ini = new IniFile();
                string iniSection;
                if (m_nIndex == -1) iniSection = INISECT_HTTP;
                else iniSection = INISECT_HTTP + "_" + m_nIndex;
                ini.IniWriteValue(iniSection, INIKEY_ADDRESS, m_strAddress, DefPath.VisionSetting);
                ini.IniWriteValue(iniSection, INIKEY_STREAM_ADDR, m_strStreamAddress, DefPath.VisionSetting);
                ini.IniWriteValue(iniSection, INIKEY_TIMEOUT, m_nTimeOut.ToString(), DefPath.VisionSetting);
                ini.IniWriteValue(iniSection, INIKEY_ID, m_strID, DefPath.VisionSetting);
                ini.IniWriteValue(iniSection, INIKEY_PW, m_strPW, DefPath.VisionSetting);

                m_bIsInit = true;

                return true;
            }
            catch (Exception)
            {
                m_bIsInit = false;
                return false;
            }
        }

        public bool SetInit()
        {
            try
            {
                LoadIni();

                m_webHandler = new WebRequestHandler();
                m_webHandler.Credentials = new NetworkCredential(m_strID, m_strPW);
                m_httpClient = new HttpClient(m_webHandler);
                m_httpClient.Timeout = new TimeSpan(m_nTimeOut * TimeSpan.TicksPerMillisecond);
                m_httpClient.BaseAddress = new Uri(m_strAddress);

                Stream stream = m_httpClient.GetStreamAsync(m_strStreamAddress).Result;

                m_bIsInit = true;

                return true;
            }
            catch (Exception)
            {
                m_bIsInit = false;
                return false;
            }


        }

        public Bitmap SnapShot()
        {
            try
            {
                Stream stream = m_httpClient.GetStreamAsync(m_strStreamAddress).Result;
                Bitmap snap = new Bitmap(stream);

                return snap;
            }
            catch (Exception)
            {
                m_bIsInit = false;
                return null;
            }
        }

        public void LoadIni()
        {
            IniFile ini = new IniFile();
            string iniSection;
            if (m_nIndex == -1) iniSection = INISECT_HTTP;
            else iniSection = INISECT_HTTP + "_" + m_nIndex;

            string iniVal = "";
            iniVal = ini.IniReadValue(iniSection, INIKEY_ADDRESS, DefPath.VisionSetting);
            if (!String.IsNullOrEmpty(iniVal)) { m_strAddress = iniVal; }            
            iniVal = ini.IniReadValue(iniSection, INIKEY_STREAM_ADDR, DefPath.VisionSetting);
            if (!String.IsNullOrEmpty(iniVal)) { m_strStreamAddress = iniVal; }            
            iniVal = ini.IniReadValue(iniSection, INIKEY_TIMEOUT, DefPath.VisionSetting);
            if (!String.IsNullOrEmpty(iniVal)) { m_nTimeOut = Convert.ToInt32(iniVal); }            
            iniVal = ini.IniReadValue(iniSection, INIKEY_ID, DefPath.VisionSetting);
            if (!String.IsNullOrEmpty(iniVal)) { m_strID = iniVal; }            
            iniVal = ini.IniReadValue(iniSection, INIKEY_PW, DefPath.VisionSetting);
            if (!String.IsNullOrEmpty(iniVal)) { m_strPW = iniVal; }            
        }
    }
}
