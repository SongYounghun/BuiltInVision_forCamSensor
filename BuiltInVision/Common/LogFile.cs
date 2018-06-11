using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalvoScanner
{
    static public class LogFile
    {
        static private string GetDataTime()
        {
            DateTime NowData = DateTime.Now;
            return NowData.ToString("yyyy-MM-dd HH:mm:ss") + ":" + NowData.Millisecond.ToString("000");
        }

        static public void LogExceptionErr(string str)
        {
            string FilePath = Application.StartupPath + @"\Logs\ExceptionErro\Log" + DateTime.Today.ToString("yyyyMMdd") + ".log";
            string DirPath = Application.StartupPath + @"\Logs\ExceptionErro\";
            string temp;

            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);

            try
            {
                if (di.Exists == false) Directory.CreateDirectory(DirPath);

                temp = string.Format("[{0}] : {1}", GetDataTime(), str);
                if (fi.Exists == false)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {                        
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }
    }
}
