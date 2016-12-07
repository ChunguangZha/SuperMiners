using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SuperMinersWeiXin.Utility
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
        object _lockWrite = new object();

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

        public FileStream OpenLogFileStream()
        {
            DateTime timenow = DateTime.Now;
            string fileName = timenow.ToString("yyyMMdd");
            FileStream stream = null;
            string path = HttpRuntime.AppDomainAppPath + "Logs" + "\\LogFile_" + fileName + ".txt";
            stream = File.Open(path, FileMode.Append, FileAccess.Write);
            return stream;
        }

        public void AddErrorLog(string log, Exception exception)
        {
            if (InitSuceed)
            {
                lock (_lockWrite)
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

                        stream = OpenLogFileStream();
                        if (stream != null)
                        {
                            stream.Position = stream.Length;
                            StreamWriter writer = new StreamWriter(stream, Encoding.Unicode);
                            
                            writer.WriteLine(builder.ToString());
                            writer.Flush();
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

        public void AddInfoLog(string log)
        {
            if (InitSuceed)
            {
                lock (_lockWrite)
                {
                    FileStream stream = null;
                    try
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append("Info:  [" + DateTime.Now.ToString() + "] ---- ");
                        builder.Append("Log: " + log);
                        builder.AppendLine("-----------------------------------------------------");

                        stream = OpenLogFileStream();
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
}