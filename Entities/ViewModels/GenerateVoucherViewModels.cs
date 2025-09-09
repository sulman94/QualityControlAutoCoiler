using System;

namespace Entities.ViewModels
{
    internal class GenerateVoucherViewModels
    {
    }
    public class GenerateVoucherGetBiltyViewModel
    {
        public long Id { get; set; }
        public DateTime? BiltyDate { get; set; }
        public string SerialNo { get; set; }
        public int DescriptionId { get; set; }
        public string Particular { get; set; }
        public string ParticularUr { get; set; }
        public string TruckNo { get; set; }
        public string ContainerNo { get; set; }
        public int? ContainerOffloadingPointId { get; set; }
        public string Consignment { get; set; }
        public int ConsignmentId { get; set; }
        public string Owner { get; set; }
        public int? OwnerId { get; set; }
        public int Qty { get; set; }
        public string LoadingPoint { get; set; }
        public int LoadingPointId { get; set; }
        public string UnloadingPoint { get; set; }
        public int UnloadingPointId { get; set; }
        public string Ship { get; set; }
        public int ShipId { get; set; }
        public string Client { get; set; }
        public int ClientId { get; set; }
        public decimal? Weight { get; set; }
        public int Bags { get; set; }
        public int Diesel { get; set; }
        public decimal Advance { get; set; }
        public int LoadingTypeId { get; set; }
        public int LoadingType { get; set; }
        public string AgentName { get; set; }
        public int AgentNameId { get; set; }
        public int ContainerSize { get; set; }
        public DateTime? ContainerOffLoadingDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
