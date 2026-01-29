namespace WebApi.Dto
{
    public class WorkOrderListItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string GTIN { get; set; } = null!;
        public string BatchNo { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public int ProducedCount { get; set; }
        public int Status { get; set; }
        public int SerialStart { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
