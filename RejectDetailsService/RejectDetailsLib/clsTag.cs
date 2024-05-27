using LibplctagWrapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http.Headers;

namespace RejectDetailsLib
{
    public class clsTag
    {
        public const string BOOL_TYPE_STR = "Bool";         // 1 byte
        public const string REAL_TYPE_STR = "Real";         // 4 byte
        public const string STRING_TYPE_STR = "String";     // 88
        public const string INT_TYPE_STR = "Int";           // 2 byte, int16
        public const string DINT_TYPE_STR = "DINT";         // 4 byte, int32
        public const string SINT_TYPE_STR = "SINT";         // 1 byte
        public const string LINT_TYPE_STR = "LINT";        // 8 byte, long
        public const string FLOAT_TYPE_STR = "FLOAT64";      // 8 byte, float 

        public const string SERIAL_NUMBER_STR = "SerialNumber";
        public clsTag()
        {
        }

        public clsTag(int id, string name) : this()
        {
            TagId = id;
            TagName = name;
        }

        public string IPAddress { get; set; }

        public int TagId { get; set; }

        public string TagName { get; set; }

        public string TagType { get; set; }

        public int TagTypeId { get; set; }

        public string Description { get; set; }

        public int Read { get; set; }

        public int Write { get; set; }

        public int Output { get; set; }

        public int RejectType { get; set; }

        public string TagTitle { get; set; }

        //public int ParentTagId { get; set; } = -1;

        public Tag plcTag { get; set; }

        public void GenerateTag(string ipAddress)
        {
            int dataType = DataType.INT;
            switch (TagType)
            {
                case BOOL_TYPE_STR:
                    dataType = DataType.SINT;
                    break;
                case REAL_TYPE_STR:
                    dataType = DataType.REAL;
                    break;
                case STRING_TYPE_STR:
                    dataType = DataType.String;
                    break;
                case INT_TYPE_STR:
                    dataType = DataType.INT;
                    break;
                case DINT_TYPE_STR:
                    dataType = DataType.DINT;
                    break;
                case SINT_TYPE_STR:
                    dataType = DataType.SINT;
                    break;
                case LINT_TYPE_STR:
                    dataType = DataType.LINT;
                    break;
                case FLOAT_TYPE_STR:
                    dataType = DataType.Float64;
                    break;
                default:
                    dataType = DataType.INT;
                    break;
            }

            //if(TagType == BOOL_TYPE_STR) {
            //    dataType = DataType.SINT;
            //} else if(TagType == REAL_TYPE_STR) {
            //    dataType = DataType.REAL;
            //} else if(TagType == STRING_TYPE_STR) {
            //    dataType = DataType.String;
            //} else if(TagType == INT_TYPE_STR) {
            //    dataType = DataType.INT;
            //}

            this.IPAddress = ipAddress;

            this.plcTag = new Tag(ipAddress, "1,0", CpuType.LGX, TagName, dataType, 1);
        }

        public static Dictionary<string, string> GetTagNameTitlePair()
        {
            return new Database().GetTagOutputNamePair();
        }
    }
}
