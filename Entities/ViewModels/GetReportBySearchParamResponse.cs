namespace Entities.ViewModels
{
    public class GetReportBySearchParamResponse
    {
        public int TotalTrips { get; set; }
        public int TotalAdvance { get; set; }
        public int TotalDiesel { get; set; }
        public int TotalBags { get; set; }
        public int TotalWeight { get; set; }
        public int TotalNoOfBagsTrip { get; set; }
        public int TotalNoOfBulkTrip { get; set; }
        public int TotalUniqueTrucks { get; set; }
        public int NoOfPaidBilities { get; set; }
    }
}
