using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class ErrorLog
    {
        public long Id { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

        public string Level { get; set; } = default!;
        public string Message { get; set; } = default!;

        public string? Exception { get; set; }
        public string? TraceId { get; set; }
        public string? RequestPath { get; set; }
        public string? Method { get; set; }
        public int? StatusCode { get; set; }

        // JSON string (Serilog Properties vs)
        public string? Properties { get; set; }
    }
}
