using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    public class clsTagValue
    {

        private const char TAG_DELIMITER = '.';

        public string tagFullName { get;set; }

        public string tagValue { get;set; }    


        /// <summary>
        /// return last part of tag name after last point for output. 
        /// </summary>
        /// <returns>last part of tag </returns>
        public string GetTagOutputName()
        {
            int ind = tagFullName.LastIndexOf(TAG_DELIMITER); ;
            if (ind < 0)
                return tagFullName;
            else
                return tagFullName.Substring(ind + 1);
        }
    }
}
