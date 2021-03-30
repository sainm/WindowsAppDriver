using System;

namespace EngineLib.Settings
{

    public class OpenFileDialogUid : ICloneable
    {

        public string CancelButton { get; set; }

        public string FileNameEditableComboBox { get; set; }

        public string OpenButton { get; set; }


        #region Public Methods and Operators

        public object Clone()
        {
            return new OpenFileDialogUid
                       {
                           OpenButton = this.OpenButton, 
                           CancelButton = this.CancelButton, 
                           FileNameEditableComboBox = this.FileNameEditableComboBox
                       };
        }

        #endregion
    }
}
