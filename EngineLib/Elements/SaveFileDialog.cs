using System.Windows.Automation;
using EngineLib.Core;
using EngineLib.Extensions;

namespace EngineLib.Elements
{
    public class SaveFileDialog : WindowAppElement
    {
        public SaveFileDialog(WindowAppElement element)
            : base(element)
        {
        }

        public SaveFileDialog(WindowAppElement parent, By getStrategy)
            : base(parent, getStrategy)
        {
        }


        public WindowAppElement CancelButton
        {
            get
            {
                var uid = CruciatusFactory.Settings.SaveFileDialogUid.CancelButton;
                return this.FindElement(By.Uid(TreeScope.Children, uid));
            }
        }


        public ComboBox FileNameComboBox
        {
            get
            {
                var uid = CruciatusFactory.Settings.SaveFileDialogUid.FileNameEditableComboBox;
                return this.FindElement(By.Uid(TreeScope.Subtree, uid)).ToComboBox();
            }
        }


        public ComboBox FileTypeComboBox
        {
            get
            {
                var uid = CruciatusFactory.Settings.SaveFileDialogUid.FileTypeComboBox;
                return this.FindElement(By.Uid(TreeScope.Subtree, uid)).ToComboBox();
            }
        }


        public WindowAppElement SaveButton
        {
            get
            {
                var uid = CruciatusFactory.Settings.SaveFileDialogUid.SaveButton;
                return this.FindElement(By.Uid(TreeScope.Children, uid));
            }
        }
    }
}