namespace WebApi.Dto
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } = null!;
        public string GLN { get; set; } = null!;
        public string? Description { get; set; }
    }
}
