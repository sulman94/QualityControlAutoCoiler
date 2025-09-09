using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Logs
    {
        public Guid LogId { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }
        public string RequestBody { get; set; }
        public string QueryString { get; set; }
        public bool IsAjax { get; set; }
        public bool IsFormPost { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double RequestDurationMs { get; set; }
        public bool IsException { get; set; }
        public string ExceptionDetails { get; set; }
        public string ResponseBody { get; set; }
        public string HttpMethod { get; set; }
        public string IpAddress { get; set; }
        public int StatusCode { get; set; }
        public string RequestHeaders { get; set; }
        public string ResponseHeaders { get; set; }
    }
}
