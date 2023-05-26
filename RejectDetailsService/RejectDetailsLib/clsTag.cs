using LibplctagWrapper;

namespace RejectDetailsLib {
    public class clsTag {

        public int StationTagId { get; set; }

        public int StationId { get; set; }

        public string StationName { get; set; }

        public int TagId { get; set; }
        
        public string TagName { get; set; }

        //public string SubName { get; set; }

        //public string ProcessTag { get; set; }

        public string TagType { get; set; }

        public string Comment { get; set; }

        public int Read { get; set; }

        public int Write { get; set; }
        //public string IpAddress { get; set; }

        public int Output { get; set; }

        public Tag plcTag { get; set; }

        public void GenerateTag(string IpAddress) {
            //string tagName = getTagPath();

            int dataType = DataType.INT;
            if(TagType == "Bool") {
                dataType = DataType.SINT;
            } else if(TagType == "Real") {
                dataType = DataType.REAL;
            } else if(TagType == "String") {
                dataType = DataType.String;
            } else if(TagType == "Int") {
                dataType = DataType.INT;
            }

            //this.plcTag = new Tag(IpAddress, "1,0", CpuType.LGX, TagFullName, dataType, 1, 1);
            this.plcTag = new Tag(IpAddress, "1,0", CpuType.LGX, TagName, dataType, 1, 1);
        }

        public string TagFullName {
            get {
                return $@"Station0{StationName}.{TagName}";
            }
        }
    }
}
