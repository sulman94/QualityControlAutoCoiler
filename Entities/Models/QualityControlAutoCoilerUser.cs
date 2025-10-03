using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Add profile data for application users by adding properties to the ProjectXUser class
    public class QualityControlAutoCoilerUser : IdentityUser<long>
    {
        [PersonalData]
        //[Required]
        [Column(TypeName = "VARCHAR(250)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "VARCHAR(250)")]
        public string LastName { get; set; }

        [PersonalData]
        public int Status { get; set; }
        public string ProfileImage { get; set; }
        public int RoleTemplateID { get; set; }        
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string? FullName { get; set; }
        public string? FatherName { get; set; }
        public string? CNIC { get; set; }
        public DateTime? DOB { get; set; }
        public string? PermanentAddress { get; set; }
        public string? ResidentialAddress { get; set; }
        public string? Domicile { get; set; }
        public string? Qualification { get; set; }
        public string? WorkExperience { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public string? ReferenceName { get; set; }
        public string? Remarks { get; set; }
        public string? BankName { get; set; }
        public string? AccountTitle { get; set; }
        public string? AccountNumber { get; set; }
    }
}
