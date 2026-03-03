using Ottergen.ColorModelsGtk;

namespace Ottergen;

public class MatugenParserGtk(string fileLocation)
{
    private readonly string _configPath = fileLocation;

    public GtkColors Parse()
    {
        var result = new GtkColors();
        if (!File.Exists(_configPath))
            return result;

        var rawColors = new Dictionary<string, RgbColor>(StringComparer.OrdinalIgnoreCase);
        var content = File.ReadAllLines(_configPath);

        foreach (var line in content)
        {
            var trimmedLine = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmedLine) || !trimmedLine.StartsWith("@define-color"))
                continue;

            var parts = trimmedLine.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length < 3)
                continue;

            var name = parts[1];
            var colorPart = parts[2];
            if (colorPart.EndsWith(';'))
                colorPart = colorPart[..^1];

            if (RgbColor.TryParse(colorPart, out var color))
            {
                rawColors[name] = color;
            }
        }

        MapGtkColors(rawColors, result);
        return result;
    }

    private static void MapGtkColors(Dictionary<string, RgbColor> raw, GtkColors target)
    {
        target.Borders = GetColor(raw, "borders");
        target.ContentViewBg = GetColor(raw, "content_view_bg");
        target.LinkColor = GetColor(raw, "link_color");
        target.LinkVisitedColor = GetColor(raw, "link_visited_color");
        target.UnfocusedBorders = GetColor(raw, "unfocused_borders");
        target.UnfocusedInsensitiveBorders = GetColor(raw, "unfocused_insensitive_borders");

        MapTheme(raw, target.Theme);
        MapButton(raw, target.Button);
        MapHeader(raw, target.Header, "theme_header");
        MapHeader(raw, target.Titlebar, "theme_titlebar");
        MapInsensitive(raw, target.Insensitive);
        MapTooltip(raw, target.Tooltip);
        MapStatus(raw, target.Success, "success_color");
        MapStatus(raw, target.Warning, "warning_color");
        MapStatus(raw, target.Error, "error_color");
    }

    private static void MapTheme(Dictionary<string, RgbColor> raw, GtkThemeColors t)
    {
        t.BaseColor = GetColor(raw, "theme_base_color");
        t.BgColor = GetColor(raw, "theme_bg_color");
        t.FgColor = GetColor(raw, "theme_fg_color");
        t.TextColor = GetColor(raw, "theme_text_color");
        t.SelectedBgColor = GetColor(raw, "theme_selected_bg_color");
        t.SelectedFgColor = GetColor(raw, "theme_selected_fg_color");
        t.HoveringSelectedBgColor = GetColor(raw, "theme_hovering_selected_bg_color");
        t.UnfocusedBaseColor = GetColor(raw, "theme_unfocused_base_color");
        t.UnfocusedBgColor = GetColor(raw, "theme_unfocused_bg_color");
        t.UnfocusedFgColor = GetColor(raw, "theme_unfocused_fg_color");
        t.UnfocusedTextColor = GetColor(raw, "theme_unfocused_text_color");
        t.UnfocusedSelectedBgColor = GetColor(raw, "theme_unfocused_selected_bg_color");
        t.UnfocusedSelectedFgColor = GetColor(raw, "theme_unfocused_selected_fg_color");
        t.UnfocusedSelectedBgColorAlt = GetColor(raw, "theme_unfocused_selected_bg_color_alt");
        t.UnfocusedViewBgColor = GetColor(raw, "theme_unfocused_view_bg_color");
        t.UnfocusedViewTextColor = GetColor(raw, "theme_unfocused_view_text_color");
        t.ViewActiveDecorationColor = GetColor(raw, "theme_view_active_decoration_color");
        t.ViewHoverDecorationColor = GetColor(raw, "theme_view_hover_decoration_color");
    }

    private static void MapButton(Dictionary<string, RgbColor> raw, GtkButtonColors b)
    {
        b.BackgroundNormal = GetColor(raw, "theme_button_background_normal");
        b.BackgroundBackdrop = GetColor(raw, "theme_button_background_backdrop");
        b.BackgroundInsensitive = GetColor(raw, "theme_button_background_insensitive");
        b.BackgroundBackdropInsensitive = GetColor(raw, "theme_button_background_backdrop_insensitive");
        b.ForegroundNormal = GetColor(raw, "theme_button_foreground_normal");
        b.ForegroundBackdrop = GetColor(raw, "theme_button_foreground_backdrop");
        b.ForegroundInsensitive = GetColor(raw, "theme_button_foreground_insensitive");
        b.ForegroundBackdropInsensitive = GetColor(raw, "theme_button_foreground_backdrop_insensitive");
        b.ForegroundActive = GetColor(raw, "theme_button_foreground_active");
        b.ForegroundActiveBackdrop = GetColor(raw, "theme_button_foreground_active_backdrop");
        b.ForegroundActiveInsensitive = GetColor(raw, "theme_button_foreground_active_insensitive");
        b.ForegroundActiveBackdropInsensitive = GetColor(raw, "theme_button_foreground_active_backdrop_insensitive");
        b.DecorationFocus = GetColor(raw, "theme_button_decoration_focus");
        b.DecorationFocusBackdrop = GetColor(raw, "theme_button_decoration_focus_backdrop");
        b.DecorationFocusInsensitive = GetColor(raw, "theme_button_decoration_focus_insensitive");
        b.DecorationFocusBackdropInsensitive = GetColor(raw, "theme_button_decoration_focus_backdrop_insensitive");
        b.DecorationHover = GetColor(raw, "theme_button_decoration_hover");
        b.DecorationHoverBackdrop = GetColor(raw, "theme_button_decoration_hover_backdrop");
        b.DecorationHoverInsensitive = GetColor(raw, "theme_button_decoration_hover_insensitive");
        b.DecorationHoverBackdropInsensitive = GetColor(raw, "theme_button_decoration_hover_backdrop_insensitive");
    }

    private static void MapHeader(Dictionary<string, RgbColor> raw, GtkHeaderColors h, string prefix)
    {
        h.Background = GetColor(raw, $"{prefix}_background");
        h.BackgroundBackdrop = GetColor(raw, $"{prefix}_background_backdrop");
        h.BackgroundLight = GetColor(raw, $"{prefix}_background_light");
        h.Foreground = GetColor(raw, $"{prefix}_foreground");
        h.ForegroundBackdrop = GetColor(raw, $"{prefix}_foreground_backdrop");
        h.ForegroundInsensitive = GetColor(raw, $"{prefix}_foreground_insensitive");
        h.ForegroundInsensitiveBackdrop = GetColor(raw, $"{prefix}_foreground_insensitive_backdrop");
    }

    private static void MapInsensitive(Dictionary<string, RgbColor> raw, GtkInsensitiveColors i)
    {
        i.BaseColor = GetColor(raw, "insensitive_base_color");
        i.BaseFgColor = GetColor(raw, "insensitive_base_fg_color");
        i.BgColor = GetColor(raw, "insensitive_bg_color");
        i.FgColor = GetColor(raw, "insensitive_fg_color");
        i.Borders = GetColor(raw, "insensitive_borders");
        i.SelectedBgColor = GetColor(raw, "insensitive_selected_bg_color");
        i.SelectedFgColor = GetColor(raw, "insensitive_selected_fg_color");
        i.UnfocusedBgColor = GetColor(raw, "insensitive_unfocused_bg_color");
        i.UnfocusedFgColor = GetColor(raw, "insensitive_unfocused_fg_color");
        i.UnfocusedSelectedBgColor = GetColor(raw, "insensitive_unfocused_selected_bg_color");
        i.UnfocusedSelectedFgColor = GetColor(raw, "insensitive_unfocused_selected_fg_color");
    }

    private static void MapTooltip(Dictionary<string, RgbColor> raw, GtkTooltipColors t)
    {
        t.Background = GetColor(raw, "tooltip_background");
        t.Border = GetColor(raw, "tooltip_border");
        t.Text = GetColor(raw, "tooltip_text");
    }

    private static void MapStatus(Dictionary<string, RgbColor> raw, GtkStatusColors s, string prefix)
    {
        s.Color = GetColor(raw, prefix);
        s.Backdrop = GetColor(raw, $"{prefix}_backdrop");
        s.Insensitive = GetColor(raw, $"{prefix}_insensitive");
        s.InsensitiveBackdrop = GetColor(raw, $"{prefix}_insensitive_backdrop");
    }

    private static RgbColor GetColor(Dictionary<string, RgbColor> raw, string name)
    {
        if (raw.TryGetValue(name, out var color))
            return color;
        //lowkey dont know if _breeze is a thing with just mine so will try booooth
        return raw.TryGetValue($"{name}_breeze", out color) ? color : default;
    }
}
