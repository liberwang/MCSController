using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    public class clsTagValue
    {
        protected object TagValue { get; set; }
        protected clsTag TagObject { get; set; }

        public clsTagValue()
        {
        }

        public clsTagValue(clsTag tagInfo) : this()
        {
            TagObject = tagInfo;
        }

        public clsTagValue(clsTag tagInfo, object tagValue) : this(tagInfo)
        { 
            this.TagValue = tagValue; 
        }

        public override string ToString()
        {
            return TagValue.ToString();
        }

        public string GetOutputTitle()
        {
            return String.IsNullOrWhiteSpace(TagObject.TagTitle) ? TagObject.TagName : TagObject.TagTitle;    
        }
    }
}
