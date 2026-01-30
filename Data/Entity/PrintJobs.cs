using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class PrintJobs
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WorkOrderId { get; set; }

        public string TargetType { get; set; } = null!;
        public Guid TargetId { get; set; }

        public string Payload { get; set; } = null!;
        public string Status { get; set; } = "Queued";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? PrintedAt { get; set; }
        public string? Error { get; set; }
    }
}
