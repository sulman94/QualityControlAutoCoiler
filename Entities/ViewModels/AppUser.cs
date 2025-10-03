using QualityControlAutoCoiler.ConfigurationRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Entities.ViewModels
{
    public class AppUser
    {
        public long UserId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Email not valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        //[Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
        //public string ConfirmPassword { get; set; }
        public int Status { get; set; }
        public int RoleTemplateID { get; set; }
        public string ProfileImage { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        [Display(Name = "Father Name")]
        public string? FatherName { get; set; }
        [Display(Name = "National Identity Card #")]
        public string? CNIC { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }
        [Display(Name = "Mobile #")]
        public string? MobileNo { get; set; }
        [Display(Name = "Permanent Address")]
        public string? PermanentAddress { get; set; }
        [Display(Name = "Residential Address")]
        public string? ResidentialAddress { get; set; }
        public string? Domicile { get; set; }
        public string? Qualification { get; set; }
        [Display(Name = "Work Experience")]
        public string? WorkExperience { get; set; }
        [Display(Name = "Joining Date")]
        public DateTime? JoiningDate { get; set; }
        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }
        [Display(Name = "Reference Name")]
        public string? ReferenceName { get; set; }
        public string? Remarks { get; set; }
        [Display(Name = "Bank Name")]
        public string? BankName { get; set; }
        [Display(Name = "Account Title")]
        public string? AccountTitle { get; set; }
        [Display(Name = "Account Number / IBAN")]
        public string? AccountNumber { get; set; }
    }
}
