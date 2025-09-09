using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class OwnerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameUr { get; set; }
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long CreatedById { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedByID { get; set; }
    }
}
