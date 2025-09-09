using System;
using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class MasterBilityLogViewModel
    {
        public List<MasterBilityLog> masterBilityLogs { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
    public class MasterBilityLog
    {
        public int Id { get; set; }
        public string MasterID { get; set; }
        public int RecordCount { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedDate { get; set; }

    }
}
