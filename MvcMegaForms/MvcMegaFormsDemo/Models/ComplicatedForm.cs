using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcMegaFormsDemo.Models
{
    public class ComplicatedForm
    {
        public int ComplicatedFormId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}