namespace WebApi.Helpers;

public static class SsccGenerator
{
    public static int CalculateCheckDigit(string sscc17)
    {
        int sum = 0;
        bool weightThree = true;

        for (int i = sscc17.Length - 1; i >= 0; i--)
        {
            int digit = sscc17[i] - '0';
            sum += digit * (weightThree ? 3 : 1);
            weightThree = !weightThree;
        }

        int mod = sum % 10;
        return mod == 0 ? 0 : 10 - mod;
    }

    public static string BuildSscc(string extensionDigit, string companyPrefix, string serialRef)
    {
        var sscc17 = extensionDigit + companyPrefix + serialRef;

        if (sscc17.Length != 17)
            throw new ArgumentException("SSCC17 en fazla 17 karakter olabilir lütfen kontrol ediniz !! ");

        int check = CalculateCheckDigit(sscc17);
        return sscc17 + check;
    }
}
