using System;
using System.Collections.Generic;

namespace Services.Models;

public partial class ApplicationModule
{
    public int Id { get; set; }

    public string ControllerName { get; set; }

    public bool IsActive { get; set; }

    public string DisplayName { get; set; }

    public string IconCode { get; set; }
    public string MenuHeader { get; set; }
    
    public int DisplayOrder { get; set; }

    public virtual ICollection<ApplicationFunctionality> ApplicationFunctionalities { get; set; } = new List<ApplicationFunctionality>();
}
