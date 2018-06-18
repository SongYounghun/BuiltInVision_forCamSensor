using GalvoScanner.LaserVision.OpenCV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltInVision
{
    public class Proc_SensorInterface
    {
        private static int m_nSenIntfIndex = -1;
        private static List<Proc_SensorInterface> m_sensorInterface = null;
        public static Proc_SensorInterface GetInstance()
        {
            if (m_sensorInterface == null)
            {
                int cvDataCnt = OpenCVData.GetOpencvDataCount();
                m_sensorInterface = new List<Proc_SensorInterface>();
                for (int i = 0; i < cvDataCnt; i++)
                {
                    Proc_SensorInterface sensorInterface = new Proc_SensorInterface();
                    m_sensorInterface.Add(sensorInterface);
                }
            }
            return m_sensorInterface[m_nSenIntfIndex];
        }

        public const string SENSINGINTERFACE_SECTION = "SENSING_INTERFACE";
        public const string SENSINGINTERFACE_KEY_INPUTTYPE = "IS_INPUTTYPE";
        public const string SENSINGINTERFACE_KEY_INPUTNUMBER = "INPUT_NUMBER";
        public const string SENSINGINTERFACE_KEY_OUTPUTNUMBER = "OUTPUT_NUMBER";

        private bool m_bIsInputType = false;
        public bool IsInputType
        {
            get { return m_bIsInputType; }
            set { m_bIsInputType = value; }
        }

        private int m_nInputNumber = -1;
        public int InputNumber
        {
            get { return m_nInputNumber; }
            set { m_nInputNumber = value; }
        }

        private int m_nOutputNumber = -1;
        public int OutputNumber
        {
            get { return m_nOutputNumber; }
            set { m_nOutputNumber = value; }
        }
    }
}
