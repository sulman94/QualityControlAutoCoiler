using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class PermissionTemplateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Template Name is required")]
        public string TemplateName { get; set; }
        public bool IsActive { get; set; }
        public List<PermissionTemplateDetails> permissionTemplates { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class PermissionTemplateDetails
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public int ModuleId { get; set; }
        public string ControllerName { get; set; }
        public string ModuleDisplayName { get; set; }
        public int FunctionalityId { get; set; }
        public string FunctionalityName { get; set; }
        public bool IsAllow { get; set; }

    }
}
