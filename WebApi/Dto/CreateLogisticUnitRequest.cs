using Data.Enum;
using Data;


namespace WebApi.Dtos;

public class CreateLogisticUnitRequest
{
    public LogisticUnitType Type { get; set; }
}
