using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foolproof;
using MvcMega.Forms.DataAnnotations;

namespace MvcMegaFormsDemo.Models
{
    public class ComplicatedForm
    {
        [HiddenInput]
        public int ComplicatedFormId { get; set; }

        [Required]
        public string EqualToInitial { get; set; }

        [EqualTo("EqualToInitial", ErrorMessage = "EqualToSecond must be equal to EqualToInitial.")]
        public string EqualToSecond { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Disabled, "EqualToSecond", ChangeVisuallyAttribute.DisplayChangeIf.NotEquals, "", false)]
        public string EqualToShowIfOk { get; set; }

        public bool ShowNextField { get; set; }

        [RequiredIf("ShowNextField", Operator.EqualTo, true)]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "ShowNextField", ChangeVisuallyAttribute.DisplayChangeIf.Equals, false, false)]
        public string NextField { get; set; }
    }
}