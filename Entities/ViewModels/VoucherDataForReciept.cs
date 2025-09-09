namespace Entities.ViewModels
{
    public class VoucherDataForReciept
    {
        public string SerialNo { get; set; }
        public string Status { get; set; }
        public string VoucherDate { get; set; }
        public string AccountTittle { get; set; }
        public string MobileNumber { get; set; }
        public string Cnic { get; set; }
        public string ChequeNo { get; set; }
        public string TotalAmount { get; set; }
        public string TotalDiesel { get; set; }
        public string TotalAdvance { get; set; }
        public string Deduction { get; set; }
        public string ClerkRate { get; set; }
        public string ClerkCharges { get; set; }
        public string PayableAfterDeduction { get; set; }
        public string Addition { get; set; }
        public string ExtraSlipCount { get; set; }
        public string ExtraSlipAmount { get; set; }
        public string NetPayableAmount { get; set; }
        public string PaidAmount { get; set; }
        public string Balance { get; set; }
        public string Comments { get; set; }
        public string Vehicles { get; set; }
    }
    public class VoucherBilitiesDetail
    {
        public string Qty { get; set; }
        public string Description { get; set; }
        public string SerialNos { get; set; }
        public string Weight { get; set; }
        public string Bag { get; set; }
        public string ContainerSize { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }
    public class BiltyDetailForVoucherReceipt
    {
        public int Sno { get; set; }
        public string Date { get; set; }
        public string SerialNo { get; set; }
        public string TruckNo { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string Bag { get; set; }
        public string ContainerSize { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string TotalAmount { get; set; }
        public string Advance { get; set; }
        public string Diesel { get; set; }
    }
    public class ExtraBiltyDetailForVoucherReceipt
    {
        public int Sno { get; set; }
        public string Date { get; set; }
        public string SerialNo { get; set; }
        public string TruckNo { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string Bag { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }
}
