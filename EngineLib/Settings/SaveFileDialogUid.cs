using System;

namespace EngineLib.Settings
{
    public class SaveFileDialogUid : ICloneable
    {
        public string CancelButton { get; set; }


        public string FileNameEditableComboBox { get; set; }


        public string FileTypeComboBox { get; set; }


        public string SaveButton { get; set; }


        public object Clone()
        {
            return new SaveFileDialogUid
            {
                SaveButton = this.SaveButton,
                CancelButton = this.CancelButton,
                FileNameEditableComboBox = this.FileNameEditableComboBox,
                FileTypeComboBox = this.FileTypeComboBox
            };
        }
    }
}