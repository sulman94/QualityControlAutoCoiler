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
    }
}
