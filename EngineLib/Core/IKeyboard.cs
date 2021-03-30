using WindowsInput.Native;

namespace EngineLib.Core
{
    public interface IKeyboard
    {
        IKeyboard KeyDown(VirtualKeyCode keyCode);


        IKeyboard KeyUp(VirtualKeyCode keyCode);


        IKeyboard SendBackspace();


        IKeyboard SendCtrlA();


        IKeyboard SendCtrlC();


        IKeyboard SendCtrlV();


        IKeyboard SendEnter();


        IKeyboard SendEscape();


        IKeyboard SendText(string text);
    }
}