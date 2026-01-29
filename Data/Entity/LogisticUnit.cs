using Data.Enum;

namespace Data.Entity
{
    public class LogisticUnit
    {
        public Guid Id { get; set; }

        public Guid WorkOrderId { get; set; }

        public string SSCC { get; set; } = null!;
        public LogisticUnitType Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public WorkOrder WorkOrder { get; set; } = null!;
        public ICollection<AggregationLink> ParentLinks { get; set; } = new List<AggregationLink>();
        public ICollection<AggregationLink> ChildLinks { get; set; } = new List<AggregationLink>();
    }
}
