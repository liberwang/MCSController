using LibplctagWrapper;

namespace RejectDetailsLib {
    public class clsTag {
        protected const string BOOL_STR = "Bool";
        protected const string REAL_STR = "Real";
        protected const string STRING_STR = "String";
        protected const string INT_STR = "Int";

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

        public Tag plcTag { get; set; }

        public void GenerateTag(string IpAddress) {

            int dataType = DataType.INT;
            if(TagType == BOOL_STR) {
                dataType = DataType.SINT;
            } else if(TagType == REAL_STR) {
                dataType = DataType.REAL;
            } else if(TagType == STRING_STR) {
                dataType = DataType.String;
            } else if(TagType == INT_STR) {
                dataType = DataType.INT;
            }

            this.plcTag = new Tag(IpAddress, "1,0", CpuType.LGX, TagName, dataType, 1, 1);
        }

        
    }
}
