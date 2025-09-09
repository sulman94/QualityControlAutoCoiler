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
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        public int Status { get; set; }
        public int RoleTemplateID { get; set; }
        public string ProfileImage { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<ProductionLog> ProductionLogApprovedByNavigations { get; set; } = new List<ProductionLog>();
        public virtual ICollection<ProductionLog> ProductionLogCreatedByNavigations { get; set; } = new List<ProductionLog>();
    }
}
