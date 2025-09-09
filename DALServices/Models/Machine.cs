using System;

namespace Services.Models;

public partial class Machine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NameInUrdu { get; set; }
    public bool IsActive { get; set; }
    public long CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}