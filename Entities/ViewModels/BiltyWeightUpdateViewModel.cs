using System;

namespace Entities.ViewModels
{
    public class BiltyWeightUpdateViewModel
    {
        public long Id { get; set; }
        public string SerialNo { get; set; }
        public string TruckNo { get; set; }
        public string LoadingPoint { get; set; }
        public string UnloadingPoint { get; set; }
        public string Ship { get; set; }
        public string Client { get; set; }
        public decimal Weight { get; set; } = 0.0m;
        public int Bags { get; set; } = 0;
        public int LoadingTypeId { get; set; }
        public DateTime? ContainerOffLoadingDate { get; set; }
        public long CreatedBy { get; set; }
        public int AdvanceAmount { get; set; }
        public int DieselAmount { get; set; }
    }  
}