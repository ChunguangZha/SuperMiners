using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SuperMinersWeb.Utility
{
    internal class LogHelper
    {
        #region Single

        private static LogHelper _instance = new LogHelper();
        public static LogHelper Instance
        {
            get
            {
                return _instance;
            }
        }

        private LogHelper()
        {

        }

        #endregion

        string LogFilePath = "";
        bool InitSuceed = false;

        public void Init()
        {
            try
            {
                string path = HttpRuntime.AppDomainAppPath + "Logs";
                LogFilePath = path + "\\LogFile.txt";
                if (File.Exists(LogFilePath))
                {
                    InitSuceed = true;
                    return;
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path + "\\Logs");
                }

                File.Create(LogFilePath);
                InitSuceed = true;
            }
            catch (Exception exc)
            {
                InitSuceed = false;
                Console.WriteLine(exc);
            }
        }

        public void AddErrorLog(string log, Exception exception)
        {
            if (InitSuceed)
            {
                FileStream stream = null;
                try
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("Error:  [" + DateTime.Now.ToString() + "] ---- ");
                    builder.Append("Log: " + log);
                    builder.AppendLine();
                    builder.Append("Exc.Message: " + exception.Message);
                    builder.AppendLine("Exc.Stack: " + exception.StackTrace);
                    builder.AppendLine("-----------------------------------------------------");

                    stream = File.OpenWrite(this.LogFilePath);
                    if (stream != null)
                    {
                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine(builder.ToString());
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
            }
        }

        public void AddInfoLog(string log)
        {
            if (InitSuceed)
            {
                FileStream stream = null;
                try
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("Info:  [" + DateTime.Now.ToString() + "] ---- ");
                    builder.Append("Log: " + log);
                    builder.AppendLine("-----------------------------------------------------");

                    stream = File.Open(this.LogFilePath, FileMode.Append, FileAccess.Write);
                    if (stream != null)
                    {
                        StreamWriter writer = new StreamWriter(stream, Encoding.Unicode);
                        writer.WriteLine(builder.ToString());
                        writer.Flush();
                        writer.Close();
                        writer.Dispose();
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
            }
        }
    }
}