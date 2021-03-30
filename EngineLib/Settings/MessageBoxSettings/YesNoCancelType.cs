using System;

namespace EngineLib.Settings.MessageBoxSettings
{
    public class YesNoCancelType : ICloneable
    {
        public string Cancel { get; set; }


        public string No { get; set; }

        public string Yes { get; set; }

        public object Clone()
        {
            return new YesNoCancelType {Yes = this.Yes, No = this.No, Cancel = this.Cancel};
        }
    }
}