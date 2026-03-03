using System.Globalization;

namespace Ottergen;

public struct RgbColor(int r, int g, int b)
{
    public readonly int R = r;
    public readonly int G = g;
    public readonly int B = b;

    public static RgbColor Parse(string raw)
    {
        raw = raw.Trim();
        if (raw.StartsWith('#'))
        {
            var hex = raw[1..];
            if (hex.Length == 6)
            {
                var r = int.Parse(hex[..2], NumberStyles.HexNumber);
                var g = int.Parse(hex[2..4], NumberStyles.HexNumber);
                var b = int.Parse(hex[4..6], NumberStyles.HexNumber);
                return new RgbColor(r, g, b);
            }
        }

        var p = raw.Split(',');
        return new RgbColor(int.Parse(p[0].Trim()), int.Parse(p[1].Trim()), int.Parse(p[2].Trim()));
    }
    
    public static bool TryParse(string? raw, out RgbColor result)
    {
        result = default;
        if (raw == null) return false;
        try
        {
            result = Parse(raw);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
