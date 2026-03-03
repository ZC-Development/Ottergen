namespace Ottergen.ColorModelsGtk;

public class GtkColors
{
    public GtkThemeColors Theme { get; set; } = new();
    public GtkButtonColors Button { get; set; } = new();
    public GtkHeaderColors Header { get; set; } = new();
    public GtkTitlebarColors Titlebar { get; set; } = new();
    public GtkInsensitiveColors Insensitive { get; set; } = new();
    public GtkTooltipColors Tooltip { get; set; } = new();
    public GtkStatusColors Success { get; set; } = new();
    public GtkStatusColors Warning { get; set; } = new();
    public GtkStatusColors Error { get; set; } = new();

    public RgbColor Borders { get; set; }
    public RgbColor ContentViewBg { get; set; }
    public RgbColor LinkColor { get; set; }
    public RgbColor LinkVisitedColor { get; set; }
    public RgbColor UnfocusedBorders { get; set; }
    public RgbColor UnfocusedInsensitiveBorders { get; set; }
}


public class GtkTitlebarColors : GtkHeaderColors;






