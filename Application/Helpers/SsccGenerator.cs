namespace Application.Helpers;

public static class SsccHelper
{
    // input: 17 hanelik (extension + 16 hane payload) => output: 18 hanelik (check digit eklenmiş)
    public static string AppendCheckDigit(string sscc17)
    {
        if (sscc17.Length != 17 || sscc17.Any(c => c < '0' || c > '9'))
            throw new ArgumentException("SSCC payload must be 17 digits.", nameof(sscc17));

        // GS1 Mod10 (Luhn benzeri): sağdan sola 3-1 ağırlık
        int sum = 0;
        bool weight3 = true; // sağdan ilk digit *3
        for (int i = sscc17.Length - 1; i >= 0; i--)
        {
            int d = sscc17[i] - '0';
            sum += d * (weight3 ? 3 : 1);
            weight3 = !weight3;
        }

        int mod = sum % 10;
        int check = (10 - mod) % 10;
        return sscc17 + check.ToString();
    }
}
