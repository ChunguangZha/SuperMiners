using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace SuperMinersWPF.Utility
{
    public delegate void LogAddedEventHandler(bool isError, string log);

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

        public event LogAddedEventHandler LogAdded;

        private List<string> ListErrorLogs = new List<string>();

        private object _lockError = new object();
        string LogErrorFilePath = "";
        bool InitSuceed = false;

        //Timer _timer = new Timer(1000000);

        public void Init()
        {
            try
            {
                LogErrorFilePath = GlobalData.LogFolder + "\\LogErrorFile.txt";
                if (!Directory.Exists(GlobalData.LogFolder))
                {
                    Directory.CreateDirectory(GlobalData.LogFolder);
                }
                if (File.Exists(LogErrorFilePath))
                {
                    File.Delete(LogErrorFilePath);
                }
                File.Create(LogErrorFilePath);

                InitSuceed = true;
                //_timer.Elapsed += Timer_Elapsed;
                //_timer.Start();
            }
            catch (Exception exc)
            {
                if (LogAdded != null)
                {
                    LogAdded(true, BuildErrorLog("Init LogHelper failed.", exc));
                }
                InitSuceed = false;
            }
        }

        public void Stop()
        {
            //_timer.Stop();
            //SaveLogToDisk();
            InitSuceed = false;
        }

        //void Timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    if (!InitSuceed)
        //    {
        //        return;
        //    }
        //    SaveLogToDisk();
        //}

        //private void SaveLogToDisk()
        //{
        //    lock (_lockError)
        //    {
        //        FileStream stream = null;
        //        try
        //        {
        //            stream = File.OpenWrite(this.LogErrorFilePath);
        //            if (stream != null)
        //            {
        //                foreach (var log in this.ListErrorLogs)
        //                {
        //                    StreamWriter writer = new StreamWriter(stream);
        //                    writer.WriteLine(log);
        //                }

        //                this.ListErrorLogs.Clear();
        //            }
        //        }
        //        catch (Exception exc)
        //        {
        //            if (LogAdded != null)
        //            {
        //                LogAdded(true, BuildErrorLog("Error Log Write to Disk failed.", exc));
        //            }
        //        }
        //        finally
        //        {
        //            if (stream != null)
        //            {
        //                stream.Close();
        //                stream.Dispose();
        //            }
        //        }
        //    }
        //}

        public void AddErrorLog(string log, Exception exception)
        {
            if (InitSuceed)
            {
                try
                {
                    string logFinished = BuildErrorLog(log, exception);

                    lock (_lockError)
                    {
                        this.ListErrorLogs.Add(logFinished);

                        using (StreamWriter writer = new StreamWriter(LogErrorFilePath,true))
                        {
                            writer.WriteLine(logFinished);
                        }

                        if (LogAdded != null)
                        {
                            LogAdded(true, logFinished);
                        }
                    }
                }
                catch (Exception exc)
                {
                    if (LogAdded != null)
                    {
                        LogAdded(true, BuildErrorLog("Add Error Log failed.", exc));
                    }
                }
            }
        }

        private string BuildErrorLog(string log, Exception exception)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Error:  [" + DateTime.Now.ToString() + "] ---- ");
            builder.Append("Log: " + log);
            builder.AppendLine();
            if (exception != null)
            {
                builder.Append("Exc.Message: " + exception.Message);
                builder.AppendLine("Exc.Stack: " + exception.StackTrace);
            }
            builder.AppendLine("-----------------------------------------------------");
            return builder.ToString();
        }

        private string BuildInfoLog(string log)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Info:  [" + DateTime.Now.ToString() + "] ---- ");
            builder.Append("Log: " + log);
            builder.AppendLine("-----------------------------------------------------");

            return builder.ToString();
        }

    }
}
