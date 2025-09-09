using System;
using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class VoucherListViewModel
    {
        public List<VoucherList> vouchers { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
    public class VoucherList
    {
        public long   Id { get; set; } = 0;
        public string VoucherSNo { get; set; }
        public string VoucherDate { get; set; }
        public string CustomerAccountName { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerOwnAccount { get; set; }
        public int    VoucherStatus { get; set; }
        public decimal TotalAdvance { get; set; }
        public decimal TotalDiesel { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
