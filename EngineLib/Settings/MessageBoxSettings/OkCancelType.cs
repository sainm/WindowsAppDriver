using System;

namespace EngineLib.Settings.MessageBoxSettings
{
    public class OkCancelType : ICloneable
    {
        public string Cancel { get; set; }

        public string Ok { get; set; }


        public object Clone()
        {
            return new OkCancelType {Ok = this.Ok, Cancel = this.Cancel};
        }
    }
}