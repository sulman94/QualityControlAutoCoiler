using System;

namespace Entities.ViewModels;

public partial class ProductionLog
{
    public long Id { get; set; }

    public string DrumNumber { get; set; }

    public int MachineId { get; set; }

    public int SizeId { get; set; }

    public int ColorId { get; set; }

    public int? LengthMentioned { get; set; }

    public int? GoodCoils { get; set; }

    public string Bp { get; set; }

    public string Np { get; set; }

    public int? ShortLengthMeters { get; set; }

    public int? ShortLengthCoils { get; set; }

    public int? DrumWiseScrap { get; set; }

    public int? LengthRecovered { get; set; }

    public string Remarks { get; set; }

    public string Reason { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public long? ApprovedBy { get; set; }

    //public virtual AppUser ApprovedByNavigation { get; set; }

    //public virtual Color Color { get; set; }

    //public virtual AppUser CreatedByNavigation { get; set; }

    //public virtual SizeCategory Size { get; set; }
}
