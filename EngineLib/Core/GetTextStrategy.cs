using System;

namespace EngineLib.Core
{
    #region using

    #endregion


    [Flags]
    public enum GetTextStrategies
    {
        None = 0, 
        TextPattern = 1, 
        ValuePattern = 2
    }
}
