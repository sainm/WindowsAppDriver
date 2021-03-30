using EngineLib;

namespace WindowsAppDriver.CommandExecutors
{
    #region using


    #endregion

    internal class SubmitElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            CruciatusFactory.Keyboard.SendEnter();
            return this.JsonResponse();
        }

        #endregion
    }
}
