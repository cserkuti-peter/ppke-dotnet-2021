using System;
using System.Diagnostics;
using System.IO;

namespace LoggingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //  Configuration, startup
            Logger.WriteLog += message => Console.WriteLine(message);
            Logger.WriteLog += message => Debug.WriteLine(message);
            Logger.WriteLog += message => {
                using (var sw = File.AppendText("output.txt"))
                {
                    sw.WriteLine(message);
                    sw.Flush();
                }
                //var sw = File.AppendText("output.txt");
                //try
                //{
                //    sw.WriteLine(message);
                //    sw.Flush();
                //}
                //finally
                //{
                //    sw.Dispose();
                //}
            };


            //  Somewhere else
            try
            {
                throw new Exception("Test");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
            }
        }
    }

    public static class Logger
    {
        public static event Action<string> WriteLog;

        public static void Log(string message)
        {
            //if (WriteLog != null)
            //    WriteLog(message);
            WriteLog?.Invoke(message);
        }
    }
}
