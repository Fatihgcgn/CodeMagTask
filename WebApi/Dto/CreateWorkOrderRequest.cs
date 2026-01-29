using Data.Enum;

namespace WebApi.Dtos;

public class CreateWorkOrderRequest
{
    public int Quantity { get; set; }
    public string BatchNo { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }
    public long SerialStart { get; set; }

    // Opsiyonel: default olarak "Ready" başlatsın
    public WorkOrderStatus? Status { get; set; }
}