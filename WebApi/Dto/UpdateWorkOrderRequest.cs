using WebApi.Enum;

namespace WebApi.Dtos;

public class UpdateWorkOrderRequest
{
    public int Quantity { get; set; }
    public string BatchNo { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }
    public long SerialStart { get; set; }
    public WorkOrderStatus Status { get; set; }
}
