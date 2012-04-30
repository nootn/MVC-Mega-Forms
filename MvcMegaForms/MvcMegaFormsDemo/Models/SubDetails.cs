using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMega.Forms.DataAnnotations;

namespace MvcMegaFormsDemo.Models
{
    public class SubDetails
    {
        public bool HasValue { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "HasValue", ChangeVisuallyAttribute.DisplayChangeIf.Equals, false, true)]
        public string TheValue { get; set; }
    }
}