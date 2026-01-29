namespace Application.Helpers;

public sealed class Gs1Builder
{
    public string BuildSerialGs1(string gtin, string serial, string batchNo, DateTime expiryDate)
    {
        var yyMMdd = expiryDate.ToString("yyMMdd");
        return $"(01){gtin}(21){serial}(10){batchNo}(17){yyMMdd}";
    }
}
