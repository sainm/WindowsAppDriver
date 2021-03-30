using System;

namespace EngineLib.Settings.MessageBoxSettings
{
    public class YesNoType : ICloneable
    {
        public string No { get; set; }

        public string Yes { get; set; }


        public object Clone()
        {
            return new YesNoType {Yes = this.Yes, No = this.No};
        }
    }
}