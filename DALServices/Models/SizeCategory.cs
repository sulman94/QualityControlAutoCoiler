using System;

namespace Services.Models;

public partial class SizeCategory
{
    public int Id { get; set; }
    public string Size { get; set; }
    public bool IsActive { get; set; }
    public long CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}