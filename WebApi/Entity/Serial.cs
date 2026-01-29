namespace WebApi.Entity
{
    public class Serial
    {
        public Guid Id { get; set; }

        public Guid WorkOrderId { get; set; }

        public string GTIN { get; set; } = null!;
        public string SerialNo { get; set; } = null!;
        public string BatchNo { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }

        public string Gs1String { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public WorkOrder WorkOrder { get; set; } = null!;
    }
}
