using System;

namespace Entities.ViewModels
{
    public class SizeCategoryViewModel
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long CreatedById { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}