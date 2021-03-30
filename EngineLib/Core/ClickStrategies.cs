using System;

namespace EngineLib.Core
{
    [Flags]
    public enum ClickStrategies
    {
        None = 0,


        ClickablePoint = 1,


        BoundingRectangleCenter = 2,


        InvokePattern = 4
    }
}