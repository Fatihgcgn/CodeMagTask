namespace WebApi.Dto
{
    public class UpdateProductRequest
    {
        public string GTIN { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
