using LibplctagWrapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    public class clsHierarchyTag : clsTag 
    {
        public int ParentTagId { get; set; } = -1;

        public clsHierarchyTag ()
        {

        }

        public clsHierarchyTag( int parentTagId )
        {
            this.ParentTagId = parentTagId;
        }

        public List<clsHierarchyTag> ChildrenTags { get; set; } = new List<clsHierarchyTag>();

        public void AddChild( clsHierarchyTag child )
        {
            ChildrenTags.Add( child );
        }
        
        /// <summary>
        /// Add all new tags from database, return the all new ones that are not existing. 
        /// </summary>
        /// <returns></returns>
        public List<clsHierarchyTag> AddChildren()
        {
            List<clsHierarchyTag > children = new Database().GetChildrenTags( this.TagId );
            List<clsHierarchyTag> result = new List<clsHierarchyTag> ();
            if (this.ChildrenTags.Any())
            {
                foreach (clsHierarchyTag child in children)
                {
                    if (!this.ChildrenTags.Where( x => x.TagId == child.TagId ).Any())
                    {
                        this.ChildrenTags.Add( child );
                        result.Add( child );
                    }
                }
            } else
            {
                this.ChildrenTags.AddRange( children );
                result = children;
            }
            return result;
        }

        public static List<clsHierarchyTag> GenerateHierarchyTags(int controllerId, string ipAddress = "")
        {
            Dictionary<int, clsHierarchyTag> dic = new Dictionary<int, clsHierarchyTag>();
            List<clsHierarchyTag> listData = new Database().GetAlarmFullTags(controllerId);
            List<clsHierarchyTag> result = new List<clsHierarchyTag>();

            foreach (clsHierarchyTag tag in listData )
            {
                if (! string.IsNullOrWhiteSpace(ipAddress))
                {
                    tag.GenerateTag(ipAddress);
                }

                //ft.tagId, ft.tagName, ft.tagType, tt.typeName, ISNULL(ft.tagRead, 0) tagRead, ISNULL(ft.tagWrite, 0) AS tagWrite, ft.tagDescription, tagTitle, parentTagId 
                if (tag.ParentTagId < 0 )
                {
                    result.Add(tag);
                } else
                {
                    if ( dic.ContainsKey(tag.ParentTagId) )
                    {
                        dic[tag.ParentTagId].ChildrenTags.Add(tag);
                    }
                }

                dic.Add(tag.TagId, tag);
            }

            return result; 
        }
    }
}
