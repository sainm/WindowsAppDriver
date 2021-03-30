using System.Threading;
using WindowsInput;
using WindowsInput.Native;
using NLog;

namespace EngineLib.Core
{
    public class KeyboardSimulatorExt : IKeyboard
    {
        private readonly IKeyboardSimulator keyboardSimulator;

        private readonly Logger logger;


        internal KeyboardSimulatorExt(IKeyboardSimulator keyboardSimulator, Logger logger)
        {
            this.logger = logger;
            this.keyboardSimulator = keyboardSimulator;
        }

        public IKeyboard KeyDown(VirtualKeyCode keyCode)
        {
            this.logger.Info("Key down '{0}'", keyCode.ToString());
            this.keyboardSimulator.KeyDown(keyCode);
            Thread.Sleep(250);
            return this;
        }


        public void KeyPress(VirtualKeyCode keyCode)
        {
            this.logger.Info("Key press '{0}'", keyCode.ToString());
            this.keyboardSimulator.KeyPress(keyCode);
            Thread.Sleep(250);
        }


        public void KeyPressSimultaneous(VirtualKeyCode keyCode1, VirtualKeyCode keyCode2)
        {
            this.logger.Info("Press key combo '{0} + {1}'", keyCode1, keyCode2);
            this.keyboardSimulator.ModifiedKeyStroke(keyCode1, keyCode2);
            Thread.Sleep(250);
        }

        public void KeyPressSimultaneous(VirtualKeyCode keyCode1, VirtualKeyCode keyCode2, VirtualKeyCode keyCode3)
        {
            this.logger.Info("Press key combo '{0} + {1} + {2}'", keyCode1, keyCode2, keyCode3);
            this.keyboardSimulator.ModifiedKeyStroke(new[] {keyCode1, keyCode2}, keyCode3);
            Thread.Sleep(250);
        }


        public IKeyboard KeyUp(VirtualKeyCode keyCode)
        {
            this.logger.Info("Key up '{0}'", keyCode.ToString());
            this.keyboardSimulator.KeyUp(keyCode);
            Thread.Sleep(250);
            return this;
        }


        public IKeyboard SendBackspace()
        {
            this.KeyPress(VirtualKeyCode.BACK);
            return this;
        }


        public IKeyboard SendCtrlA()
        {
            this.KeyPressSimultaneous(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
            return this;
        }


        public IKeyboard SendCtrlC()
        {
            this.KeyPressSimultaneous(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
            return this;
        }


        public IKeyboard SendCtrlV()
        {
            this.KeyPressSimultaneous(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
            return this;
        }


        public IKeyboard SendEnter()
        {
            this.KeyPress(VirtualKeyCode.RETURN);
            return this;
        }


        public IKeyboard SendEscape()
        {
            this.KeyPress(VirtualKeyCode.ESCAPE);
            return this;
        }


        public IKeyboard SendText(string text)
        {
            this.logger.Info("Send text '{0}'", text);
            if (!string.IsNullOrEmpty(text))
            {
                this.keyboardSimulator.TextEntry(text);
                Thread.Sleep(250);
            }

            return this;
        }
    }
}