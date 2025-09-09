using System;

namespace Entities.ViewModels
{
    public class ConsignmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Ship { get; set; }
        public string Client { get; set; }
        public string LoadingPoint { get; set; }
        public string UnLoadingPoint { get; set; }
        public string Description { get; set; }
        public string AgentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long CreatedById { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
