using Data.Enum;
using Data.Entity;

namespace Data.Entity;

public class WorkOrder
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
    public string BatchNo { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }

    public long SerialStart { get; set; }
    public WorkOrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public Product Product { get; set; } = null!;
    public ICollection<Serial> Serials { get; set; } = new List<Serial>();
    public ICollection<LogisticUnit> LogisticUnits { get; set; } = new List<LogisticUnit>();
}