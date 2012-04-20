using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcMega.Forms.MVC
{
    public class CascadingSelectList : List<ChildSelectListItem>
    {
        public string ParentSelectListPropertyName { get; set; }
    }
}
