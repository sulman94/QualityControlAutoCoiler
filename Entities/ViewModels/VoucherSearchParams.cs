using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class VoucherSearchParams
    {
        public int? ConsignmentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? RevenueAccountId { get; set; }
        public int? StatusId { get; set; }
        public int? VendorAccountId { get; set; }
        public string VendorCnic { get; set; }
        public string VendorName { get; set; }
        public string VendorPhoneNumber { get; set; }
        public string TruckNo { get; set; }
        public string VoucherSerialNo { get; set; }


    }
}
