using System;
using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class BiltyListViewModel
    {
        public List<BiltyViewModel> bilties { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
    public class BiltyViewModel
    {
        public long Id { get; set; }
        public int MasterId { get; set; }
        public string MasterName { get; set; }
        public string SerialNo { get; set; }
        public int ContainerSize { get; set; }
        public DateTime? BiltyDate { get; set; }
        public string TruckNo { get; set; }
        public int LoadingTypeId { get; set; }
        public string LoadingType { get; set; }
        public int? OwnerId { get; set; }
        public int ConsignmentId { get; set; }
        public int ContainerOffloadingPointId { get; set; }
        public int ShipId { get; set; }
        public int ClientId { get; set; }
        public int LoadingPointId { get; set; }
        public int UnloadingPointId { get; set; }
        public int DescriptionId { get; set; }
        public decimal? Weight { get; set; }
        public int Bags { get; set; }
        public int Advance { get; set; }
        public int Diesel { get; set; }
        public string DriverName { get; set; }
        public int AgentNameId { get; set; }
        public string AgentName { get; set; }
        public decimal PaidAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool Status { get; set; }
        public int PrintCount { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Ship { get; set; }
        public string Client { get; set; }
        public string Description { get; set; }
        public string UnloadingPoint { get; set; }
        public string LoadingPoint { get; set; }
        public string ContainerNo { get; set; }
        public long VoucherId { get; set; }
        public bool IsLinkedToVoucher { get; set; }

    }
}
