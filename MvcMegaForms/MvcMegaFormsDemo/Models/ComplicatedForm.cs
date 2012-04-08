using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foolproof;

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

        //TODO: Enable this is EqualToInitial == EqualToSecond!
        public string EqualToShowIfOk { get; set; }

        public bool ShowNextField { get; set; }

        //TODO: show this if ShowNextField == true
        [RequiredIf("ShowNextField", Operator.EqualTo, true)]
        public string NextField { get; set; }
    }
}