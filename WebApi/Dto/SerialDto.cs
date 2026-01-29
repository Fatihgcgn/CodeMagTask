namespace WebApi.Dto
{
    public class SerialDto
    {
        public Guid Id { get; set; }
        public string Gtin { get; set; } = null!;
        public string SerialNo { get; set; } = null!;
        public string BatchNo { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
        public string Gs1String { get; set; } = null!;
    }
}
