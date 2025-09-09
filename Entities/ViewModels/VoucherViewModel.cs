using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class VoucherViewModel
    {
        public long VoucherId { get; set; } = 0;
        public string? VoucherSerialNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public int? VendorAccountId { get; set; }
        public int? OwnAccountId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiesel { get; set; }
        public decimal TotalAdvance { get; set; }
        public decimal Deduction { get; set; }
        public decimal ClerkRate { get; set; }
        public decimal ClerkCharges { get; set; }
        public decimal PayableAfterDeduction { get; set; }
        public decimal Addition { get; set; }
        public int ExtraSlipsCount { get; set; } = 0;
        public decimal ExtraSlipsAmount { get; set; } = 0.0m;
        public int BilitiesCount { get; set; }
        public decimal NetPayableAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal PaidAmount { get; set; }
        public string ChequeNo { get; set; }
        public string Comments { get; set; }
        public List<VoucherBilities> VoucherBilities { get; set; }
        public List<ExtraSlips> ExtraSlips { get; set; }
        public int VoucherStatus { get; set; }
        public bool GenerateVoucher { get; set; } = false;
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public string TruckNos { get; set; }

    }
    public class ExtraSlips
    {
        public string id { get; set; }
        public DateTime date { get; set; }
        public string serialno { get; set; }
        public string truckno { get; set; }
        public string client { get; set; }
        public string ship { get; set; }
        public string commodity { get; set; }
        public string loading { get; set; }
        public string unloading { get; set; }
        public int? bags { get; set; }
        public int? weight { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
    }

    public class VoucherBilities
    {
        public long BilityId { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
