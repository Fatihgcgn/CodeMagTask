namespace WebApi.Entity
{
    public class Product
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public string GTIN { get; set; } = null!;
        public string Name { get; set; } = null!;

        public Customer Customer { get; set; } = null!;
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

    }
}
