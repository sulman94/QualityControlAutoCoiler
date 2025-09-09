using System;
using System.Collections.Generic;

namespace Services.Models;

public partial class ApplicationFunctionality
{
    public int Id { get; set; }

    public string FunctionalityName { get; set; }

    public int ApplicationModuleId { get; set; }

    public bool IsActive { get; set; }

    public string ActionMethodName { get; set; }

    public bool IsMenuItem { get; set; }

    public virtual ApplicationModule ApplicationModule { get; set; }
}
