using System;
using System.Diagnostics;
using System.IO;
using EngineLib.Exceptions;

namespace EngineLib
{
    public class Application
    {
        private readonly string executableFilePath;

        private Process process;


        public Application(string executableFilePath)
        {
            if (executableFilePath == null)
            {
                throw new ArgumentNullException("executableFilePath");
            }

            if (Path.IsPathRooted(executableFilePath))
            {
                this.executableFilePath = executableFilePath;
            }
            else
            {
                var absolutePath = Path.Combine(Environment.CurrentDirectory, executableFilePath);
                this.executableFilePath = Path.GetFullPath((new Uri(absolutePath)).LocalPath);
            }
        }


        public bool Close()
        {
            this.process.CloseMainWindow();
            return this.process.WaitForExit(CruciatusFactory.Settings.WaitForExitTimeout);
        }


        public bool Kill()
        {
            this.process.Kill();
            return this.process.WaitForExit(CruciatusFactory.Settings.WaitForExitTimeout);
        }


        public void Start()
        {
            this.Start(string.Empty);
        }


        public void Start(string arguments)
        {
            if (!File.Exists(this.executableFilePath))
            {
                throw new CruciatusException(string.Format(@"Path ""{0}"" doesn't exists", this.executableFilePath));
            }

            var directory = Path.GetDirectoryName(this.executableFilePath);

            // ReSharper disable once AssignNullToNotNullAttribute
            // directory не может быть null, в связи с проверкой выше наличия файла executableFilePath
            var info = new ProcessStartInfo
            {
                FileName = this.executableFilePath,
                WorkingDirectory = directory,
                Arguments = arguments
            };

            this.process = Process.Start(info);
        }
    }
}