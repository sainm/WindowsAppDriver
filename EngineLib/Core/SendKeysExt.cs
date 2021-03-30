using System;
using System.Threading;
using System.Windows.Forms;
using WindowsInput.Native;
using NLog;

namespace EngineLib.Core
{
    public class SendKeysExt : IKeyboard
    {
        public const char Alt = '%';


        public const string Backspace = "{BACKSPACE}";


        public const char Ctrl = '^';


        public const string Enter = "{ENTER}";


        public const string Escape = "{ESCAPE}";


        public const char Shift = '+';


        private readonly Logger logger;


        internal SendKeysExt(Logger logger)
        {
            this.logger = logger;
        }


        public IKeyboard KeyDown(VirtualKeyCode keyCode)
        {
            throw new NotImplementedException();
        }


        public IKeyboard KeyUp(VirtualKeyCode keyCode)
        {
            throw new NotImplementedException();
        }


        public IKeyboard SendBackspace()
        {
            return this.SendKeysPrivate(Backspace);
        }

        public IKeyboard SendCtrlA()
        {
            return this.SendKeysPrivate(Ctrl + "a");
        }


        public IKeyboard SendCtrlC()
        {
            return this.SendKeysPrivate(Ctrl + "c");
        }


        public IKeyboard SendCtrlV()
        {
            return this.SendKeysPrivate(Ctrl + "v");
        }


        public IKeyboard SendEnter()
        {
            return this.SendKeysPrivate(Enter);
        }


        public IKeyboard SendEscape()
        {
            return this.SendKeysPrivate(Escape);
        }


        public IKeyboard SendText(string text)
        {
            this.logger.Info("Send text '{0}'", text);
            return this.SendWaitPrivate(text);
        }


        private IKeyboard SendKeysPrivate(string keys)
        {
            this.logger.Info("Send keys '{0}'", keys);
            return this.SendWaitPrivate(keys);
        }

        private IKeyboard SendWaitPrivate(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                SendKeys.SendWait(text);
                Thread.Sleep(250);
            }

            return this;
        }
    }
}