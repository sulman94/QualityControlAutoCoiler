using System;

namespace Entities.ViewModels
{
    public class BilitySearchParams
    {
        public string SerialNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ConsignmentId { get; set; }
        public int? OwnerId { get; set; }
        public int? Client { get; set; }
        public int? AgentNameId { get; set; }
        public int? Ship { get; set; }
        public int? LoadingPoint { get; set; }
        public int? UnLoadingPoint { get; set; }
        public int? Description { get; set; }
        public bool Bulk { get; set; }
        public bool Bags { get; set; }
        public bool Container { get; set; }
        public bool AllLoadingType { get; set; }
        public bool Paid { get; set; }
        public bool UnPaid { get; set; }
        public bool PaidUnpaidBoth { get; set; }
        public bool Diesel { get; set; }
        public bool Advance { get; set; }
        public bool DieselAdvanceBoth { get; set; }
        public string TruckNo { get; set; }
    }
}
