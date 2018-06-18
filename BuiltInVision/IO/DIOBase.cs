using GalvoScanner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalvoScanner.IO
{
    public class DIOBase
    {
        public enum KIND_DIO { AJIN = 0, P_PMAC };

        public static IDIO GetInstanceInterface()
        {
            if (DIOBase.GetUseDIO())
            {
                switch (DIOBase.GetKindOfDIO())
                {
                    case DIOBase.KIND_DIO.AJIN:
                        return DIOAjin.GetInstance();
                    case DIOBase.KIND_DIO.P_PMAC:
                        return DIOPmac.GetInstance();
                }
            }
            return null;
        }

        ~DIOBase()
        {
            IDIO dio = GetInstanceInterface();
            if (dio != null)
            {
                for (int i = 0; i < m_nOutputCount; i++)
                {
                    dio.WriteOutBit(i, 0);
                }
            }
        }

        protected static bool m_bIsUseDIO = false;
        public static bool GetUseDIO()
        {
            return m_bIsUseDIO;
        }

        protected static KIND_DIO m_kindOfDIO = KIND_DIO.AJIN;
        public static KIND_DIO GetKindOfDIO()
        {
            return m_kindOfDIO;
        }

        public static string INISECTION_IO_INITIAL = "InitialIO";
        public static string INIKEY_INIT_USE_IO = "USE_IO";
        public static string INIKEY_INIT_KIND_OF_IO = "KIND_OF_IO";

        public static string INISECTION_INPUT_MAP = "INPUT";
        public static string INISECTION_OUTPUT_MAP = "OUTPUT";

        public static void ReadIoINI(string path)
        {
            bool useIO = false;
            int kindOfIO = 0;

            IniFile ini = new IniFile();
            string strIniValue = "";

            strIniValue = ini.IniReadValue(DIOBase.INISECTION_IO_INITIAL, DIOBase.INIKEY_INIT_USE_IO, path);
            if (strIniValue != "") { useIO = Convert.ToBoolean(strIniValue); }
            strIniValue = ini.IniReadValue(DIOBase.INISECTION_IO_INITIAL, DIOBase.INIKEY_INIT_KIND_OF_IO, path);
            if (strIniValue != "") { kindOfIO = Convert.ToInt32(strIniValue); }

            m_bIsUseDIO = useIO;
            m_kindOfDIO = (KIND_DIO)kindOfIO;

            if (useIO)
            {
                try
                {
                    switch (kindOfIO)
                    {
                        case 0: // Ajin
                            {
                                DIOAjin ioAjin = null;
                                ioAjin = DIOAjin.GetInstance();
                                ioAjin.InitDIO();
                                ioAjin.LoadIOMap(DefPath.IOMap);
                            }
                            break;

                        case 1: // Power PMAC
                            {
                                DIOPmac ioPmac = DIOPmac.GetInstance();
                                ioPmac.InitDIO();
                                ioPmac.LoadIOMap(DefPath.IOMap);
                            }
                            break;
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message.ToString());
                }
            }
        }

        protected int m_nModuleCount = 0;
        public int MouduleCount
        {
            get { return m_nModuleCount; }
        }

        protected int m_nInputCount = 0;
        public int InputCount
        {
            get { return m_nInputCount; }
        }

        protected int m_nOutputCount = 0;
        public int OutputCount
        {
            get { return m_nOutputCount; }
        }

        protected List<string> m_listInNames = null;
        public List<string> ListInputNames
        {
            get { return m_listInNames; }
        }

        protected List<string> m_listOutNames = null;
        public List<string> ListOutputNames
        {
            get { return m_listOutNames; }
        }
    }
}
