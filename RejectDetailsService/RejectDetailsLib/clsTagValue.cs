using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    public class clsTagValue : clsTag
    {
        private const char TAG_DELIMITER = '.';

        public string TagValue { get;set; }    

        /// <summary>
        /// return last part of tag name after last point for output. 
        /// </summary>
        /// <returns>last part of tag </returns>
        public string GetTagOutputName()
        {
            int ind = TagName.LastIndexOf(TAG_DELIMITER);
            if (ind < 0)
                return TagName;
            else
                return TagName.Substring(ind + 1);
        }

        public static string GetTagOutputName(string pTagName)
        {
            int ind = pTagName.LastIndexOf(TAG_DELIMITER);
            if (ind < 0)
                return pTagName;
            else
                return pTagName.Substring(ind + 1);
        }
    }
}
