using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class OrderModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Serial No is required")]
        [Display(Name = "Serial No")]
        public long SerialNo { get; set; }
        [Required(ErrorMessage = "Customer name is required")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Number is required")]
        [Display(Name = "Phone Number")]
        public string CustomerPhoneNumber { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Delivery Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }
        public bool IsPaid { get; set; }
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Total Amount is required")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "Balance is required")]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        [Required(ErrorMessage = "Advance is required")]
        [Display(Name = "Advance")]
        public decimal Advance { get; set; }
        public int Status { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<OrderDetailModel> OrderDetailList { get; set; }

    }
    public class OrderDetailModel
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public int QTY { get; set; }
        public decimal Amount { get; set; }

    }

    public class OrderPaymentModel
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class OrderPaymentDetail
    {
        public long OrderId { get; set; }
        public long SerialNo { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal ReceiveAmount { get; set; }
        public long CreatedBy { get; set; }
    }

    public class OrderTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
