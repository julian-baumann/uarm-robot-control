using System.Globalization;

namespace UArmSDK.Extensions;

public static class FloatExtensions
{
    public static string ToInvariantString(this float value)
    {
        return value.ToString("F2", CultureInfo.InvariantCulture);
    }
}
