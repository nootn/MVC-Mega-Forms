using MvcMega.Forms.DataAnnotations;

namespace MvcMega.Forms.WebsiteForTesting.Models
{
    public class SubDetails
    {
        public bool HasValue { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "HasValue", ChangeVisuallyAttribute.DisplayChangeIf.Equals, false, true)]
        public string TheValue { get; set; }
    }
}