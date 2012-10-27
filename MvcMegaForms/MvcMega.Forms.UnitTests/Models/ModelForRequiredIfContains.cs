using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMega.Forms.UnitTests.Models
{
    public class ModelForRequiredIfContains
    {
        public static readonly string CurrentPropertyName = "CurrentProperty";
        public static readonly string DependentPropertyName = "DependentProperty";
        public static readonly string InvalidDependentPropertyName = "InvalidDependentProperty";

        public string CurrentProperty { get; set; }

        public List<string> DependentProperty { get; set; }

        public int InvalidDependentProperty { get; set; }
    }
}
