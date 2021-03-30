using EngineLib.Core;
using EngineLib.Settings.MessageBoxSettings;


namespace EngineLib.Settings
{
    public class CruciatusSettings
    {
        private const MouseButton DefaultClickButton = MouseButton.Left;

        private const int DefaultScrollBarHeight = 18;

        private const int DefaultScrollBarWidth = 18;

        private const int DefaultSearchTimeout = 10000;

        private const int DefaultWaitForExitTimeout = 10000;


        private static readonly MessageBoxButtonUid DefaultMessageBoxButtonUid = new MessageBoxButtonUid();

        private static readonly OpenFileDialogUid DefaultOpenFileDialogUid = new OpenFileDialogUid();

        private static readonly SaveFileDialogUid DefaultSaveFileDialogUid = new SaveFileDialogUid();

        private static CruciatusSettings instance;


        private CruciatusSettings()
        {
            DefaultMessageBoxButtonUid.CloseButton = "Close";
            DefaultMessageBoxButtonUid.OkType = new OkType {Ok = "2"};
            DefaultMessageBoxButtonUid.OkCancelType = new OkCancelType {Ok = "1", Cancel = "2"};
            DefaultMessageBoxButtonUid.YesNoType = new YesNoType {Yes = "6", No = "7"};
            DefaultMessageBoxButtonUid.YesNoCancelType = new YesNoCancelType {Yes = "6", No = "7", Cancel = "2"};

            DefaultOpenFileDialogUid.OpenButton = "1";
            DefaultOpenFileDialogUid.CancelButton = "2";
            DefaultOpenFileDialogUid.FileNameEditableComboBox = "1148";

            DefaultSaveFileDialogUid.SaveButton = "1";
            DefaultSaveFileDialogUid.CancelButton = "2";
            DefaultSaveFileDialogUid.FileNameEditableComboBox = "FileNameControlHost";
            DefaultSaveFileDialogUid.FileTypeComboBox = "FileTypeControlHost";

            this.ResetToDefault();
        }


        public bool AutomaticScreenshotCapture { get; set; }


        public MouseButton ClickButton { get; set; }


        public KeyboardSimulatorType KeyboardSimulatorType { get; set; }


        public MessageBoxButtonUid MessageBoxButtonUid { get; set; }


        public OpenFileDialogUid OpenFileDialogUid { get; set; }


        public SaveFileDialogUid SaveFileDialogUid { get; set; }


        public string ScreenshotsPath { get; set; }


        public int ScrollBarHeight { get; set; }


        public int ScrollBarWidth { get; set; }


        public int SearchTimeout { get; set; }


        public int WaitForExitTimeout { get; set; }


        internal static CruciatusSettings Instance
        {
            get { return instance ?? (instance = new CruciatusSettings()); }
        }


        public void ResetToDefault()
        {
            this.SearchTimeout = DefaultSearchTimeout;
            this.WaitForExitTimeout = DefaultWaitForExitTimeout;
            this.ScrollBarWidth = DefaultScrollBarWidth;
            this.ScrollBarHeight = DefaultScrollBarHeight;
            this.ClickButton = DefaultClickButton;
            this.KeyboardSimulatorType = KeyboardSimulatorType.BasedOnWindowsFormsSendKeysClass;
            this.ScreenshotsPath = "Screenshots";
            this.AutomaticScreenshotCapture = false;

            this.MessageBoxButtonUid = (MessageBoxButtonUid) DefaultMessageBoxButtonUid.Clone();
            this.OpenFileDialogUid = (OpenFileDialogUid) DefaultOpenFileDialogUid.Clone();
            this.SaveFileDialogUid = (SaveFileDialogUid) DefaultSaveFileDialogUid.Clone();
        }
    }
}