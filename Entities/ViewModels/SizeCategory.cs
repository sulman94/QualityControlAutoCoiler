using Entities.ViewModels;
using System;
using System.Collections.Generic;

namespace QualityControlAutoCoiler.ConfigurationRepositories;

public partial class SizeCategory
{
    public int Id { get; set; }

    public string Size { get; set; }

    public bool IsActive { get; set; }

    public long CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<ProductionLog> ProductionLogs { get; set; } = new List<ProductionLog>();
}
