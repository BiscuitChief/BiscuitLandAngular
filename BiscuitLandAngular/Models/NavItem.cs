using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiscuitLandAngular.Models
{
    public class NavItem
    {
        public string Text { get; set; }

        public string Url { get; set; }

        public string Target { get; set; }

        public List<NavItem> SubItems { get; set; } 
    }
}
