using System.Globalization;
using System.Windows.Automation;
using CommonLib;

namespace WindowsAppDriver.CommandExecutors
{
    internal class GetCurrentWindowHandleExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var node = AutomationElement.FocusedElement;
            var rootElement = AutomationElement.RootElement;
            var treeWalker = TreeWalker.ControlViewWalker;
            while (node != rootElement && !node.Current.ControlType.Equals(ControlType.Window))
            {
                node = treeWalker.GetParent(node);
            }

            var result = (node == rootElement)
                ? string.Empty
                : node.Current.NativeWindowHandle.ToString(CultureInfo.InvariantCulture);
            return this.JsonResponse(ResponseStatus.Success, result);
        }
    }
}