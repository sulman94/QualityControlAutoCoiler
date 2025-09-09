using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ConsignmentDropdowns
    {
        public List<DropdownModel> clients { get; set; }
        public List<DropdownModel> ships { get; set; }
        public List<DropdownModel> agentnames { get; set; }
        public List<DropdownModel> descriptions { get; set; }
        public List<DropdownModel> loadingPoints { get; set; }
        public List<DropdownModel> unloadingPoints { get; set; }

    }
}
