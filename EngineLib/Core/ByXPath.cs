using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using EngineLib.Helpers;

namespace EngineLib.Core
{
    public class ByXPath : By
    {
        private readonly string xpath;


        internal ByXPath(string xpath)
        {
            this.xpath = xpath;
        }


        public override string ToString()
        {
            return this.xpath;
        }


        internal override IEnumerable<AutomationElement> FindAll(AutomationElement parent, int timeout)
        {
            return AutomationElementHelper.FindAll(parent, this.xpath, timeout);
        }

        internal override AutomationElement FindFirst(AutomationElement parent, int timeout)
        {
            return AutomationElementHelper.FindAll(parent, this.xpath, timeout).FirstOrDefault();
        }
    }
}