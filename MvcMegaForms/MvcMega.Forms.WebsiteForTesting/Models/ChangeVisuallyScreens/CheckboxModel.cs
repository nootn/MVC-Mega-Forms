using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMega.Forms.DataAnnotations;

namespace MvcMega.Forms.WebsiteForTesting.Models.ChangeVisuallyScreens
{
    public class CheckboxModel
    {
        public bool TickCheckboxToShowNextField { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "TickCheckboxToShowNextField", ChangeVisuallyAttribute.DisplayChangeIf.Equals, false, false)]
        public string NextField { get; set; }
    }
}