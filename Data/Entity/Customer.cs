namespace Data.Entity
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string GLN { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
