namespace WebApi.Dto
{
    public class UpdateCustomerRequest
    {
        public string Name { get; set; } = null!;
        public string GLN { get; set; } = null!;
        public string? Description { get; set; }
    }
}
