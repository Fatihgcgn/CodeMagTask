namespace WebApi.Dto
{
    public class GenerateSerialsResponse
    {
        public Guid WorkOrderId { get; set; }
        public int GeneratedCount { get; set; }
        public List<SerialDto> Serials { get; set; } = new();
    }
}
