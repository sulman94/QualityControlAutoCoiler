using System;
using System.Collections.Generic;

namespace Services.Models;

public partial class PermissionTemplateDetail
{
    public int Id { get; set; }

    public int TemplateId { get; set; }

    public int FunctionalityId { get; set; }

    public bool IsAllow { get; set; }

    public virtual PermissionTemplate Template { get; set; }
}
