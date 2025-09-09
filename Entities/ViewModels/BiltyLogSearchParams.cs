using System;

namespace Entities.ViewModels
{
    public class BiltyLogSearchParams
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MasterID { get; set; }

    }
}
