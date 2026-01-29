namespace WebApi.Dto;

public sealed class WorkOrderSnapshotDto
{
    public WorkOrderDto WorkOrder { get; set; } = null!;
    public ProductDto Product { get; set; } = null!;
    public List<SerialDto> Serials { get; set; } = new();
    public List<LogisticUnitDto> LogisticUnits { get; set; } = new();
    public List<AggregationLinkDto> AggregationLinks { get; set; } = new();
}

public sealed class WorkOrderDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string BatchNo { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }
    public int Quantity { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class ProductDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string GTIN { get; set; } = null!;
    public string Name { get; set; } = null!;
}


public sealed class LogisticUnitDto
{
    public Guid Id { get; set; }
    public Guid WorkOrderId { get; set; }
    public string SSCC { get; set; } = null!;
    public int Type { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class AggregationLinkDto
{
    public Guid Id { get; set; }
    public Guid ParentLogisticUnitId { get; set; }
    public int ChildType { get; set; }
    public Guid? ChildLogisticUnitId { get; set; }
    public Guid? ChildSerialId { get; set; }
    public DateTime CreatedAt { get; set; }
}
