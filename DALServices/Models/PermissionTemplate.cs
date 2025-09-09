using System;
using System.Collections.Generic;

namespace Services.Models;

public partial class PermissionTemplate
{
    public int Id { get; set; }

    public string TemplateName { get; set; }

    public bool IsActive { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<PermissionTemplateDetail> PermissionTemplateDetails { get; set; } = new List<PermissionTemplateDetail>();
}
