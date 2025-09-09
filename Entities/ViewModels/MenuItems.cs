using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class MenuItems
    {
        public class Items
        {
            public string ItemName { get; set; }
            public string Icon { get; set; }
            public List<SubMenuItems> SubMenuItems { get; set; }
        }
        public class SubMenuItems
        {
            public string submenuItem { get; set; }
            public string NavigationLink { get; set; }
        }
    }
}
