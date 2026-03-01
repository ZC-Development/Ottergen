using System.Globalization;
using System.Reflection;
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

        MapSection(sections, "ColorEffects:Disabled", result.ColorEffectsDisabled);
        MapSection(sections, "ColorEffects:Inactive", result.ColorEffectsInactive);
        MapSection(sections, "WM", result.Wm);
        MapSection(sections, "Colors:Button", result.ColorsButton);
        MapSection(sections, "Colors:Complementary", result.ColorsComplementary);
        MapSection(sections, "Colors:Header", result.ColorsHeader);
        MapSection(sections, "Colors:Header][Inactive", result.ColorsHeaderInactive);
        MapSection(sections, "Colors:Selection", result.ColorsSelection);
        MapSection(sections, "Colors:Tooltip", result.ColorsTooltip);
        MapSection(sections, "Colors:View", result.ColorsView);
        MapSection(sections, "Colors:Window", result.ColorsWindow);

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

    private static void MapSection(Dictionary<string, Dictionary<string, string>> sections, string sectionName,
        object target)
    {
        if (!sections.TryGetValue(sectionName, out var values))
            return;

        var type = target.GetType();
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            if (!values.TryGetValue(prop.Name, out var value))
                continue;

            if (prop.PropertyType == typeof(RgbColor))
            {
                var color = RgbColor.TryParse(value);
                if (color != null)
                    prop.SetValue(target, color.Value);
            }
            else if (prop.PropertyType == typeof(double))
            {
                if (double.TryParse(value, CultureInfo.InvariantCulture, out var d))
                    prop.SetValue(target, d);
            }
            else if (prop.PropertyType == typeof(bool))
            {
                if (bool.TryParse(value, out var b))
                    prop.SetValue(target, b);
            }
            else if (prop.PropertyType == typeof(int))
            {
                if (int.TryParse(value, out var i))
                    prop.SetValue(target, i);
            }
        }
    }
}