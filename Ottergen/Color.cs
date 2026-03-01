namespace Ottergen;

public struct RgbColor(int r, int g, int b)
{
    public readonly int R = r;
    public readonly int G = g;
    public readonly int B = b;

    public static RgbColor Parse(string raw)
    {
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
