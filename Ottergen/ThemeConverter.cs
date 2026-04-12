using Ottergen.ColorModelsGtk;
using Ottergen.ColorModelsQt;

namespace Ottergen;

public static class ThemeConverter
{
    public static MatugenColors ToQt(GtkColors gtk)
    {
        var qt = new MatugenColors();
        
        MapGtkToQt(gtk.Theme, qt.ColorsWindow);
        MapGtkToQt(gtk.Theme, qt.ColorsView);
        
        MapGtkToQt(gtk.Button, qt.ColorsButton);
        
        MapGtkToQt(gtk.Header, qt.ColorsHeader);
        
        qt.ColorsSelection.BackgroundNormal = gtk.Theme.SelectedBgColor;
        qt.ColorsSelection.ForegroundNormal = gtk.Theme.SelectedFgColor;
        
        qt.ColorsTooltip.BackgroundNormal = gtk.Tooltip.Background;
        qt.ColorsTooltip.ForegroundNormal = gtk.Tooltip.Text;
        
        qt.Wm.ActiveBackground = gtk.Header.Background;
        qt.Wm.ActiveForeground = gtk.Header.Foreground;
        qt.Wm.InactiveBackground = gtk.Header.BackgroundBackdrop;
        qt.Wm.InactiveForeground = gtk.Header.ForegroundBackdrop;
        
        qt.ColorsView.ForegroundNegative = gtk.Error.Color;
        qt.ColorsView.ForegroundNeutral = gtk.Warning.Color;
        qt.ColorsView.ForegroundPositive = gtk.Success.Color;
        qt.ColorsWindow.ForegroundNegative = gtk.Error.Color;
        qt.ColorsWindow.ForegroundNeutral = gtk.Warning.Color;
        qt.ColorsWindow.ForegroundPositive = gtk.Success.Color;

        return qt;
    }

    private static void MapGtkToQt(GtkThemeColors gtk, AbstractQtColors qt)
    {
        qt.BackgroundNormal = gtk.BgColor;
        qt.BackgroundAlternate = gtk.BaseColor;
        qt.ForegroundNormal = gtk.TextColor;
        qt.ForegroundInactive = gtk.UnfocusedTextColor;
        qt.ForegroundActive = gtk.SelectedFgColor;
        qt.ForegroundLink = gtk.ViewActiveDecorationColor; 
        qt.DecorationFocus = gtk.ViewActiveDecorationColor;
        qt.DecorationHover = gtk.ViewHoverDecorationColor;
    }

    private static void MapGtkToQt(GtkButtonColors gtk, AbstractQtColors qt)
    {
        qt.BackgroundNormal = gtk.BackgroundNormal;
        qt.ForegroundNormal = gtk.ForegroundNormal;
        qt.ForegroundActive = gtk.ForegroundActive;
        qt.ForegroundInactive = gtk.ForegroundInsensitive;
        qt.DecorationFocus = gtk.DecorationFocus;
        qt.DecorationHover = gtk.DecorationHover;
    }

    private static void MapGtkToQt(GtkHeaderColors gtk, AbstractQtColors qt)
    {
        qt.BackgroundNormal = gtk.Background;
        qt.ForegroundNormal = gtk.Foreground;
        qt.ForegroundInactive = gtk.ForegroundInsensitive;
    }

    public static GtkColors ToGtk(MatugenColors qt)
    {
        var gtk = new GtkColors
        {
            Theme =
            {
                BgColor = qt.ColorsWindow.BackgroundNormal,
                BaseColor = qt.ColorsView.BackgroundNormal,
                TextColor = qt.ColorsView.ForegroundNormal,
                FgColor = qt.ColorsWindow.ForegroundNormal,
                SelectedBgColor = qt.ColorsSelection.BackgroundNormal,
                SelectedFgColor = qt.ColorsSelection.ForegroundNormal,
                UnfocusedBgColor = qt.ColorsWindow.BackgroundNormal, 
                UnfocusedTextColor = qt.ColorsView.ForegroundInactive,
                ViewActiveDecorationColor = qt.ColorsView.DecorationFocus,
                ViewHoverDecorationColor = qt.ColorsView.DecorationHover
            },
            Button =
            {
                BackgroundNormal = qt.ColorsButton.BackgroundNormal,
                ForegroundNormal = qt.ColorsButton.ForegroundNormal,
                ForegroundActive = qt.ColorsButton.ForegroundActive,
                ForegroundInsensitive = qt.ColorsButton.ForegroundInactive,
                DecorationFocus = qt.ColorsButton.DecorationFocus,
                DecorationHover = qt.ColorsButton.DecorationHover
            },
            Header =
            {
                Background = qt.ColorsHeader.BackgroundNormal,
                Foreground = qt.ColorsHeader.ForegroundNormal,
                ForegroundInsensitive = qt.ColorsHeader.ForegroundInactive,
                BackgroundBackdrop = qt.Wm.InactiveBackground,
                ForegroundBackdrop = qt.Wm.InactiveForeground
            },
            Titlebar =
            {
                Background = qt.Wm.ActiveBackground,
                Foreground = qt.Wm.ActiveForeground,
                BackgroundBackdrop = qt.Wm.InactiveBackground,
                ForegroundBackdrop = qt.Wm.InactiveForeground
            },
            Tooltip =
            {
                Background = qt.ColorsTooltip.BackgroundNormal,
                Text = qt.ColorsTooltip.ForegroundNormal
            },
            Error =
            {
                Color = qt.ColorsView.ForegroundNegative
            },
            Warning =
            {
                Color = qt.ColorsView.ForegroundNeutral
            },
            Success =
            {
                Color = qt.ColorsView.ForegroundPositive
            },
            LinkColor = qt.ColorsView.ForegroundLink
        };

        return gtk;
    }
}
