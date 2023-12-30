using LibplctagWrapper;
using System.Collections.Generic;
using System.Data.Common;

namespace RejectDetailsLib {
    public class clsTag {
        public const string BOOL_TYPE_STR = "Bool";
        public const string REAL_TYPE_STR = "Real";
        public const string STRING_TYPE_STR = "String";
        public const string INT_TYPE_STR = "Int";

        public const string SERIAL_NUMBER_STR = "SerialNumber";
        public clsTag()
        {

        }

        public clsTag(int id, string name) : this()
        {
            TagId = id;
            TagName = name;
        }

        public int TagId { get; set; }
        
        public string TagName { get; set; }

        public string TagType { get; set; }

        public string Description { get; set; }

        public int Read { get; set; }

        public int Write { get; set; }

        public int Output { get; set; }

        public int RejectType { get; set; }

        public string TagTitle { get; set; }    

        public Tag plcTag { get; set; }

        public void GenerateTag(string IpAddress) {

            int dataType = DataType.INT;
            if(TagType == BOOL_TYPE_STR) {
                dataType = DataType.SINT;
            } else if(TagType == REAL_TYPE_STR) {
                dataType = DataType.REAL;
            } else if(TagType == STRING_TYPE_STR) {
                dataType = DataType.String;
            } else if(TagType == INT_TYPE_STR) {
                dataType = DataType.INT;
            }

            this.plcTag = new Tag(IpAddress, "1,0", CpuType.LGX, TagName, dataType, 1);
        }

        public static Dictionary<string, string> GetTagNameTitlePair()
        {
            return new Database().GetTagOutputNamePair();
        }
    }
}
