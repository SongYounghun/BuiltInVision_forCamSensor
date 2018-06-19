using GalvoScanner;
using GalvoScanner.IO;
using GalvoScanner.LaserVision.OpenCV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BuiltInVision
{
    public class Proc_SensorInterface
    {
        private static int m_nSenIntfIndex = -1;
        private static int m_nCount = 0;
        public static int GetInterfaceCount()
        {
            return m_nCount;
        }

        private static List<Proc_SensorInterface> m_sensorInterface = null;
        public static Proc_SensorInterface GetInstance(int index = -1)
        {
            if (m_sensorInterface == null)
            {
                m_nCount = OpenCVData.GetOpencvDataCount();
                m_sensorInterface = new List<Proc_SensorInterface>();
                for (int i = 0; i < m_nCount; i++)
                {
                    Proc_SensorInterface sensorInterface = new Proc_SensorInterface(i);                    
                    m_sensorInterface.Add(sensorInterface);
                }
            }

            if (index != -1)
            {
                m_nSenIntfIndex = index;
            }
            else
            {
                if (m_sensorInterface.Count > 0) m_nSenIntfIndex = 0;
            }
            return m_sensorInterface[m_nSenIntfIndex];
        }

        public const string SENSINGINTERFACE_SECTION = "SENSING_INTERFACE";
        public const string SENSINGINTERFACE_KEY_ISINPUTTYPE = "IS_INPUT_TYPE";
        public const string SENSINGINTERFACE_KEY_INPUTNUMBER = "INPUT_NUMBER";
        public const string SENSINGINTERFACE_KEY_OUTPUTNUMBER_OK = "OUTPUT_NUMBER_OK";
        public const string SENSINGINTERFACE_KEY_OUTPUTNUMBER_FAIL = "OUTPUT_NUMBER_FAIL";
        public const string SENSINGINTERFACE_KEY_OUTPUTHOLDTIME = "OUTPUT_HOLD_TIME";

        private bool m_bIsInputType = false;
        [CategoryAttribute("Sensing interface(I/O)"), DescriptionAttribute("Is input type (If this is false output type)")]
        public bool IsInputType
        {
            get { return m_bIsInputType; }
            set { m_bIsInputType = value; }
        }

        private int m_nInputNumber = -1;
        [CategoryAttribute("Sensing interface(I/O)"), DescriptionAttribute("Input number")]
        public int InputNumber
        {
            get { return m_nInputNumber; }
            set { m_nInputNumber = value; }
        }

        private int m_nOutputNumberOK = -1;
        [CategoryAttribute("Sensing interface(I/O)"), DescriptionAttribute("Output number(OK)")]
        public int OutputNumberOK
        {
            get { return m_nOutputNumberOK; }
            set { m_nOutputNumberOK = value; }
        }

        private int m_nOutputNumberFAIL = -1;
        [CategoryAttribute("Sensing interface(I/O)"), DescriptionAttribute("Output number(FAIL)")]
        public int OutputNumberFAIL
        {
            get { return m_nOutputNumberFAIL; }
            set { m_nOutputNumberFAIL = value; }
        }

        private int m_nOutputHoldTime = 50;
        [CategoryAttribute("Sensing interface(I/O)"), DescriptionAttribute("Output hold on time")]
        public int OutputHoldTime
        {
            get { return m_nOutputHoldTime; }
            set { m_nOutputHoldTime = value; }
        }

        private OpenCVData m_cvData = null;
        private int m_nID = -1;

        public Proc_SensorInterface(int index)
        {
            m_nID = index;
            m_cvData = OpenCVData.GetInstance(index);
        }

        public void SaveSettingINI(string path)
        {
            try
            {
                IniFile ini = new IniFile();
                StringBuilder section = new StringBuilder();
                section.Append(SENSINGINTERFACE_SECTION); section.Append("_"); section.Append(m_nID);
                ini.IniWriteValue(section.ToString(), SENSINGINTERFACE_KEY_ISINPUTTYPE, m_bIsInputType.ToString(), path);
                ini.IniWriteValue(section.ToString(), SENSINGINTERFACE_KEY_INPUTNUMBER, m_nInputNumber.ToString(), path);
                ini.IniWriteValue(section.ToString(), SENSINGINTERFACE_KEY_OUTPUTNUMBER_OK, m_nOutputNumberOK.ToString(), path);
                ini.IniWriteValue(section.ToString(), SENSINGINTERFACE_KEY_OUTPUTNUMBER_FAIL, m_nOutputNumberFAIL.ToString(), path);
                ini.IniWriteValue(section.ToString(), SENSINGINTERFACE_KEY_OUTPUTHOLDTIME, m_nOutputHoldTime.ToString(), path);
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }

        public void LoadSettingINI(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    String iniValue = "";
                    IniFile ini = new IniFile();

                    StringBuilder section = new StringBuilder();
                    section.Append(SENSINGINTERFACE_SECTION); section.Append("_"); section.Append(m_nID);
                    iniValue = ini.IniReadValue(section.ToString(), SENSINGINTERFACE_KEY_ISINPUTTYPE, path);
                    if (!string.IsNullOrEmpty(iniValue)) { m_bIsInputType = Convert.ToBoolean(iniValue); }
                    iniValue = ini.IniReadValue(section.ToString(), SENSINGINTERFACE_KEY_INPUTNUMBER, path);
                    if (!string.IsNullOrEmpty(iniValue)) { m_nInputNumber = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(section.ToString(), SENSINGINTERFACE_KEY_OUTPUTNUMBER_OK, path);
                    if (!string.IsNullOrEmpty(iniValue)) { m_nOutputNumberOK = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(section.ToString(), SENSINGINTERFACE_KEY_OUTPUTNUMBER_FAIL, path);
                    if (!string.IsNullOrEmpty(iniValue)) { m_nOutputNumberFAIL = Convert.ToInt32(iniValue); }
                    iniValue = ini.IniReadValue(section.ToString(), SENSINGINTERFACE_KEY_OUTPUTHOLDTIME, path);
                    if (!string.IsNullOrEmpty(iniValue)) { m_nOutputHoldTime = Convert.ToInt32(iniValue); }
                }
            }
            catch (Exception E)
            {
                LogFile.LogExceptionErr(E.ToString());
                throw E;
            }
        }

        private Thread m_thrSensingInterface = null;
        private bool m_bIsProcLive = false;

        private void ThreadSensignInterface()
        {            
            IDIO io = DIOBase.GetInstanceInterface();
            if (io != null)
            {
                bool preInput = false, currInput = false;
                while (m_bIsProcLive)
                {
                    Thread.Sleep(3);
                    uint readBit = 0U;
                    if (m_bIsInputType)
                    {
                        io.ReadInBit(m_nInputNumber, ref readBit);
                        if (readBit == 1) currInput = true;
                        else currInput = false;
                    }
                    else
                    {
                        io.ReadOutBit(m_nInputNumber, ref readBit);
                        if (readBit == 1) currInput = true;
                        else currInput = false;
                    }

                    if (currInput && preInput != currInput)
                    {
                        m_cvData.GrabFromCamera();
                        if (m_cvData.ProcessOneCycle())
                        {
                            io.WriteOutBit(m_nOutputNumberOK, 1U);
                        }
                        else
                        {
                            io.WriteOutBit(m_nOutputNumberFAIL, 1U);
                        }

                        Thread.Sleep(m_nOutputHoldTime);
                        io.WriteOutBit(m_nOutputNumberOK, 0U);
                        io.WriteOutBit(m_nOutputNumberFAIL, 0U);
                    }

                    preInput = currInput;
                }
            }

            m_thrSensingInterface = null;
            m_bIsProcLive = false;
        }

        public void StartSensingInterface()
        {
            if (m_thrSensingInterface == null)
            {
                m_bIsProcLive = true;
                m_thrSensingInterface = new Thread(new ThreadStart(ThreadSensignInterface));
                m_thrSensingInterface.Start();
            }
        }

        public void StopSensignInterface()
        {
            m_bIsProcLive = false;
        }

        public bool IsRunningProcess()
        {
            return (m_thrSensingInterface != null);
        }
    }
}
