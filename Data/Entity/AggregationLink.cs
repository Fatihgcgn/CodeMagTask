using Data.Enum;

namespace Data.Entity
{
    public class AggregationLink
    {
        public Guid Id { get; set; }

        public Guid ParentLogisticUnitId { get; set; }

        public AggregationChildType ChildType { get; set; }

        public Guid? ChildLogisticUnitId { get; set; }
        public Guid? ChildSerialId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public LogisticUnit ParentLogisticUnit { get; set; } = null!;
        public LogisticUnit? ChildLogisticUnit { get; set; }
        public Serial? ChildSerial { get; set; }
    }
}
