using System;
using System.IO;
using System.Text;

namespace DatasetGenerator.Logging
{
    class Logger
    {
        public static readonly FileInfo LogFile = new FileInfo("G:/gta.log");

        public string ClassName { get; private set; }

        private void PrintMessage(string level, string message)
        {
            using (var logFileStream =
                new FileStream(LogFile.FullName, FileMode.Append))
            {
                using (var logStreamWriter = new StreamWriter(logFileStream, Encoding.UTF8))
                {
                    logStreamWriter.WriteLine($"[{level}] {ClassName}: {message}");
                }
            }
        }

        public void Debug(string message)
        {
            PrintMessage("DEBUG", message);
        }

        public void Info(string message)
        {
            PrintMessage("INFO", message);
        }

        public void Error(string message)
        {
            PrintMessage("ERROR", message);
        }


        public static Logger GetLogger(Type classType)
        {
            return new Logger
            {
                ClassName = classType.Name
            };
        }
    }
}
