using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.IO
{
    public class DIOAjin : DIOBase, IDIO
    {
        public struct ModuleInfo
        {
            public int nIndex;
            public string strDevName;
            public int nInputCount;
            public int nOutputCount;
            public uint uID;
            public string strUserName;
            public int nInStartIndex;
            public int nInEndIndex;
            public int nOutStartIndex;
            public int nOutEndIndex;

            public ModuleInfo(int moduleIndex, string devName, int inputCount, int outputCount, uint id, string usrName, int inStartInd, int inEndInd, int outStartInd, int outEndInd)
            {
                nIndex = moduleIndex;
                strDevName = devName;
                nInputCount = inputCount;
                nOutputCount = outputCount;
                uID = id;
                strUserName = usrName;
                nInStartIndex = inStartInd;
                nInEndIndex = inStartInd;
                nOutStartIndex = outStartInd;
                nOutEndIndex = outStartInd;
            }
        }

        private static DIOAjin m_dioAjin = null;
        public static DIOAjin GetInstance()
        {
            if (m_bIsUseDIO)
            {
                if (m_dioAjin == null)
                {
                    m_dioAjin = new DIOAjin();
                }

                return m_dioAjin;
            }

            return null;
        }

        protected List<ModuleInfo> m_listModules = new List<ModuleInfo>();
        public List<ModuleInfo> Modules
        {
            get { return m_listModules; }
        }

        public void InitDIO()
        {
            try
            {
                // Initialize library 
                uint result = CAXL.AxlOpen(7);
                if (result == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS || result == (uint)AXT_FUNC_RESULT.AXT_RT_OPEN_ALREADY)
                {
                    uint uStatus = 0;

                    if (CAXD.AxdInfoIsDIOModule(ref uStatus) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    {
                        if ((AXT_EXISTENCE)uStatus == AXT_EXISTENCE.STATUS_EXIST)
                        {
                            int nModuleCount = 0;

                            if (CAXD.AxdInfoGetModuleCount(ref nModuleCount) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                            {
                                short i = 0;
                                int nBoardNo = 0;
                                int nModulePos = 0;
                                uint uModuleID = 0;
                                string strData = "";

                                for (i = 0; i < nModuleCount; i++)
                                {
                                    if (CAXD.AxdInfoGetModule(i, ref nBoardNo, ref nModulePos, ref uModuleID) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                                    {
                                        switch ((AXT_MODULE)uModuleID)
                                        {
                                            case AXT_MODULE.AXT_SIO_RDI32MLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DI32", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDI32MSMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32P", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDI32PMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DB32P", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDI32RTEX: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32T", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_DI32_P: strData = String.Format("[{0:D2}:{1:D2}] SIO-DB32T", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDI32: strData = String.Format("[{0:D2}:{1:D2}] SIO_RDI32", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_DI32: strData = String.Format("[{0:D2}:{1:D2}] SIO_RDO32", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDO32MLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB128MLII", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDO32AMSMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RSIMPLEIOMLII", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDO32PMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDO16AMLII", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDO16AMLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDO16BMLII", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDO16BMLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB96MLII", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDO32RTEX: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDO32RTEX", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_DO32T_P: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDI32RTEX", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDO32: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB32RTEX", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_DO32P: strData = String.Format("[{0:D2}:{1:D2}] SIO-DI32_P", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_DO32T: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32T_P", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDB32MLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB32T", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDB32PMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DI32", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDB128MLIIIAI: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32P", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDB96MLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DB32P", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDB32RTEX: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32T", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDB128MLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DB32T", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_DB32P: strData = String.Format("[{0:D2}:{1:D2}] SIO_RDI32", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RDB32T: strData = String.Format("[{0:D2}:{1:D2}] SIO_RDO32", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_DB32T: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB128MLII", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_UNDEFINEMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RSIMPLEIOMLII", nBoardNo, i); break;
                                            case AXT_MODULE.AXT_SIO_RSIMPLEIOMLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDO16AMLII", nBoardNo, i); break;
                                        }

                                        int inpCnt = 0, outCnt = 0;
                                        CAXD.AxdInfoGetInputCount(i, ref inpCnt);
                                        CAXD.AxdInfoGetOutputCount(i, ref outCnt);

                                        int inStartIndex = m_nInputCount;
                                        int outStartIndex = m_nOutputCount;

                                        m_nInputCount += inpCnt;
                                        m_nOutputCount += outCnt;

                                        int inEndIndex = m_nInputCount - 1;
                                        int outEndIndex = m_nOutputCount - 1;

                                        switch ((AXT_MODULE)uModuleID)
                                        {
                                            case AXT_MODULE.AXT_SIO_DB32P:
                                            case AXT_MODULE.AXT_SIO_RDB32T:
                                            case AXT_MODULE.AXT_SIO_DB32T:
                                            case AXT_MODULE.AXT_SIO_UNDEFINEMLIII:
                                                {
                                                    outStartIndex += 16;
                                                    outEndIndex += 16;
                                                }
                                                break;
                                        }

                                        ModuleInfo modInfo = new ModuleInfo(i, strData, inpCnt, outCnt, uModuleID, "none", inStartIndex, inEndIndex, outStartIndex, outEndIndex);
                                        m_listModules.Add(modInfo);
                                    }
                                }
                                m_nModuleCount = nModuleCount;
                            }
                        }
                    }
                }

                m_listInNames = new List<string>(m_nInputCount);
                for (int i = 0; i < m_nInputCount; i++)
                    m_listInNames.Add("");

                m_listOutNames = new List<string>(m_nOutputCount);
                for (int i = 0; i < m_nOutputCount; i++)
                    m_listOutNames.Add("");
                    
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public bool WriteOutBit(int index, uint value)
        {
            try
            {
                if (m_nModuleCount <= 0)
                {
                    return false;
                }

                if (index < m_nOutputCount)
                {
                    CAXD.AxdoWriteOutport(index, value);

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public bool ReadInBit(int index, ref uint value)
        {
            try
            {
                if (m_nModuleCount <= 0)
                {
                    return false;
                }

                if (index < m_nInputCount)
                {
                    CAXD.AxdiReadInport(index, ref value);

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception E)
            {
                throw E;
            }
        }


        public bool ReadOutBit(int index, ref uint value)
        {
            try
            {
                if (index < m_nOutputCount)
                {
                    CAXD.AxdoReadOutport(index, ref value);

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception E)
            {
                throw E;
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

        public void SaveIOMap(string path)
        {
            try
            {
                if (m_bIsUseDIO)
                {
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
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public void LoadIOMap(string path)
        {
            try
            {
                if (m_bIsUseDIO)
                {
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
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
