using System.Windows.Automation;
using EngineLib.Core;
using EngineLib.Extensions;

namespace EngineLib.Elements
{
    public class OpenFileDialog : WindowAppElement
    {
        public OpenFileDialog(WindowAppElement element)
            : base(element)
        {
        }


        public OpenFileDialog(WindowAppElement parent, By getStrategy)
            : base(parent, getStrategy)
        {
        }


        public WindowAppElement CancelButton
        {
            get
            {
                var uid = CruciatusFactory.Settings.OpenFileDialogUid.CancelButton;
                return this.FindElement(By.Uid(TreeScope.Children, uid));
            }
        }

        public ComboBox FileNameComboBox
        {
            get
            {
                var uid = CruciatusFactory.Settings.OpenFileDialogUid.FileNameEditableComboBox;
                return this.FindElement(By.Uid(TreeScope.Children, uid)).ToComboBox();
            }
        }


        public WindowAppElement OpenButton
        {
            get
            {
                var uid = CruciatusFactory.Settings.OpenFileDialogUid.OpenButton;
                return this.FindElement(By.Uid(TreeScope.Children, uid));
            }
        }
    }
}