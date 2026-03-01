using System.Globalization;
using Ottergen.ColorModelsQt;

namespace Ottergen;

public class MatugenParserQt(string fileLocation)
{
    private readonly string _configPath = Path.Combine(fileLocation);

    public MatugenColors Parse()
    {
        if (!File.Exists(_configPath))
            return new MatugenColors();

        var content = File.ReadAllText(_configPath);
        var sections = ParseFile(content);
        var result = new MatugenColors();

        MapColorEffectsDisabled(sections, "ColorEffects:Disabled", result.ColorEffectsDisabled);
        MapColorEffectsInactive(sections, "ColorEffects:Inactive", result.ColorEffectsInactive);
        MapWm(sections, "WM", result.Wm);
        MapAbstractQtColors(sections, "Colors:Button", result.ColorsButton);
        MapAbstractQtColors(sections, "Colors:Complementary", result.ColorsComplementary);
        MapAbstractQtColors(sections, "Colors:Header", result.ColorsHeader);
        MapAbstractQtColors(sections, "Colors:Header][Inactive", result.ColorsHeaderInactive);
        MapAbstractQtColors(sections, "Colors:Selection", result.ColorsSelection);
        MapAbstractQtColors(sections, "Colors:Tooltip", result.ColorsTooltip);
        MapAbstractQtColors(sections, "Colors:View", result.ColorsView);
        MapAbstractQtColors(sections, "Colors:Window", result.ColorsWindow);

        return result;
    }

    private static Dictionary<string, Dictionary<string, string>> ParseFile(string content)
    {
        var sections = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
        string? currentSectionName = null;
        var currentSection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        using var reader = new StringReader(content);
        while (reader.ReadLine() is { } line)
        {
            line = line.Trim();
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith(';') || line.StartsWith('#'))
                continue;

            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                if (currentSectionName != null)
                {
                    sections[currentSectionName] = currentSection;
                }

                currentSectionName = line[1..^1];
                currentSection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                {
                    currentSection[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }

        if (currentSectionName != null)
        {
            sections[currentSectionName] = currentSection;
        }

        return sections;
    }

    private static void MapColorEffectsDisabled(Dictionary<string, Dictionary<string, string>> sections,
        string sectionName, ColorEffectsDisabled target)
    {
        if (!sections.TryGetValue(sectionName, out var v)) return;

        if (v.TryGetValue("Color", out var s) && RgbColor.TryParse(s, out var c)) target.Color = c;
        if (v.TryGetValue("ColorAmount", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out var d)) target.ColorAmount = d;
        if (v.TryGetValue("ColorEffect", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.ColorEffect = d;
        if (v.TryGetValue("ContrastAmount", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.ContrastAmount = d;
        if (v.TryGetValue("ContrastEffect", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.ContrastEffect = d;
        if (v.TryGetValue("IntensityAmount", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.IntensityAmount = d;
        if (v.TryGetValue("IntensityEffect", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.IntensityEffect = d;
    }

    private static void MapColorEffectsInactive(Dictionary<string, Dictionary<string, string>> sections,
        string sectionName, ColorEffectsInactive target)
    {
        if (!sections.TryGetValue(sectionName, out var v)) return;

        if (v.TryGetValue("ChangeSelectionColor", out var s) && bool.TryParse(s, out var b)) target.ChangeSelectionColor = b;
        if (v.TryGetValue("Color", out s) && RgbColor.TryParse(s, out var c)) target.Color = c;
        if (v.TryGetValue("ColorAmount", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out var d)) target.ColorAmount = d;
        if (v.TryGetValue("ColorEffect", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.ColorEffect = d;
        if (v.TryGetValue("ContrastAmount", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.ContrastAmount = d;
        if (v.TryGetValue("ContrastEffect", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.ContrastEffect = d;
        if (v.TryGetValue("Enable", out s) && bool.TryParse(s, out b)) target.Enable = b;
        if (v.TryGetValue("IntensityAmount", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.IntensityAmount = d;
        if (v.TryGetValue("IntensityEffect", out s) && double.TryParse(s, CultureInfo.InvariantCulture, out d)) target.IntensityEffect = d;
    }

    private static void MapWm(Dictionary<string, Dictionary<string, string>> sections, string sectionName, Wm target)
    {
        if (!sections.TryGetValue(sectionName, out var v)) return;

        if (v.TryGetValue("ActiveBackground", out var s) && RgbColor.TryParse(s, out var c)) target.ActiveBackground = c;
        if (v.TryGetValue("ActiveBlend", out s) && RgbColor.TryParse(s, out var c2)) target.ActiveBlend = c2;
        if (v.TryGetValue("ActiveForeground", out s) && RgbColor.TryParse(s, out var c3)) target.ActiveForeground = c3;
        if (v.TryGetValue("InactiveBackground", out s) && RgbColor.TryParse(s, out var c4)) target.InactiveBackground = c4;
        if (v.TryGetValue("InactiveBlend", out s) && RgbColor.TryParse(s, out var c5)) target.InactiveBlend = c5;
        if (v.TryGetValue("InactiveForeground", out s) && RgbColor.TryParse(s, out var c6)) target.InactiveForeground = c6;
    }

    private static void MapAbstractQtColors(Dictionary<string, Dictionary<string, string>> sections, string sectionName,
        AbstractQtColors target)
    {
        if (!sections.TryGetValue(sectionName, out var v)) return;

        if (v.TryGetValue("BackgroundAlternate", out var s) && RgbColor.TryParse(s, out var c)) target.BackgroundAlternate = c;
        if (v.TryGetValue("BackgroundNormal", out s) && RgbColor.TryParse(s, out var c2)) target.BackgroundNormal = c2;
        if (v.TryGetValue("DecorationFocus", out s) && RgbColor.TryParse(s, out var c3)) target.DecorationFocus = c3;
        if (v.TryGetValue("DecorationHover", out s) && RgbColor.TryParse(s, out var c4)) target.DecorationHover = c4;
        if (v.TryGetValue("ForegroundActive", out s) && RgbColor.TryParse(s, out var c5)) target.ForegroundActive = c5;
        if (v.TryGetValue("ForegroundInactive", out s) && RgbColor.TryParse(s, out var c6)) target.ForegroundInactive = c6;
        if (v.TryGetValue("ForegroundLink", out s) && RgbColor.TryParse(s, out var c7)) target.ForegroundLink = c7;
        if (v.TryGetValue("ForegroundNegative", out s) && RgbColor.TryParse(s, out var c8)) target.ForegroundNegative = c8;
        if (v.TryGetValue("ForegroundNeutral", out s) && RgbColor.TryParse(s, out var c9)) target.ForegroundNeutral = c9;
        if (v.TryGetValue("ForegroundNormal", out s) && RgbColor.TryParse(s, out var c10)) target.ForegroundNormal = c10;
        if (v.TryGetValue("ForegroundPositive", out s) && RgbColor.TryParse(s, out var c11)) target.ForegroundPositive = c11;
        if (v.TryGetValue("ForegroundVisited", out s) && RgbColor.TryParse(s, out var c12)) target.ForegroundVisited = c12;
    }
}