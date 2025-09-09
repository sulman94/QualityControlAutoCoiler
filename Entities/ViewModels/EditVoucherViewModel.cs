using System;
using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class EditVoucherViewModel
    {
        public long VoucherId { get; set; }
        public string VoucherSerialNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public int? VendorAccountId { get; set; }
        public int? OwnAccountId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalDiesel { get; set; }
        public decimal? TotalAdvance { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? Addition { get; set; }
        public decimal? ClerkRate { get; set; }
        public decimal? ClerkCharges { get; set; }
        public decimal PayableAfterDeduction { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? Balance { get; set; }
        public decimal? PaidAmount { get; set; }
        public string ChequeNo { get; set; }
        public string Comments { get; set; }
        public string CNIC { get; set; }
        public string Title { get; set; }
        public string MobileNumber { get; set; }
        public int BilitiesCount { get; set; }
        public decimal NetPayableAmount { get; set; }
        public decimal ExtraSlipsAmount { get; set; }
        public int ExtraSlipsCount { get; set; }
        public List<GenerateVoucherGetBiltyViewModel> VoucherBilities { get; set; }
        public List<ExtraSlips> ExtraSlips { get; set; }
        public int VoucherStatus { get; set; }

    }
}
