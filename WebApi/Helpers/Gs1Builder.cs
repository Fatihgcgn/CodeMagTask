namespace WebApi.Helpers;

public static class Gs1Builder
{
    public static string Build(string gtin, string serialNo, DateTime expiry, string batchNo)
    {
        var exp = expiry.ToString("yyMMdd");
        return $"(01){gtin}(21){serialNo}(17){exp}(10){batchNo}";
    }
}
