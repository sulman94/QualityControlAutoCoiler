using System;

namespace Entities.ViewModels
{
    public class BiltyDetailsForReceipt
    {
        public long Id { get; set; }
        public string SerialNo { get; set; }
        public string BiltyDate { get; set; }
        public string TruckNo { get; set; }
        public int LoadingTypeId { get; set; }
        public int ContainerSize { get; set; }
        public string Ship { get; set; }
        public string Client { get; set; }
        public string Description { get; set; }
        public string UnloadingPoint { get; set; }
        public string LoadingPoint { get; set; }
        public string ContainerUnloadingPoint { get; set; }
        public string ContainerNo { get; set; }
        public string DriverName { get; set; }
        public string AgentName { get; set; }
        public string QRCodeBase64 { get; set; }
        public string Weight { get; set; }
        public int Bags { get; set; }
        public decimal Advance { get; set; }
        public int Diesel { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }

    }
}
