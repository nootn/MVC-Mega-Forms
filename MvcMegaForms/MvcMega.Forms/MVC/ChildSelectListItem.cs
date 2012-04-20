using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcMega.Forms.MVC
{
    public class ChildSelectListItem : SelectListItem
    {
        public string ParentValue { get; set; }
    }
}
