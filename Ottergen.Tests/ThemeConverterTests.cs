using NUnit.Framework;
using Ottergen.ColorModelsGtk;
using Ottergen.ColorModelsQt;

namespace Ottergen.Tests;

[TestFixture]
public class ThemeConverterTests
{
    [Test]
    public void ToQt_MapsCorrectColors()
    {
        var gtk = new GtkColors();
        gtk.Theme.BgColor = new RgbColor(10, 20, 30);
        gtk.Theme.SelectedBgColor = new RgbColor(100, 150, 200);
        gtk.Header.Background = new RgbColor(40, 50, 60);
        gtk.Error.Color = new RgbColor(255, 0, 0);

        var qt = ThemeConverter.ToQt(gtk);

        Assert.Multiple(() =>
        {
            Assert.That(qt.ColorsWindow.BackgroundNormal, Is.EqualTo(gtk.Theme.BgColor));
            Assert.That(qt.ColorsSelection.BackgroundNormal, Is.EqualTo(gtk.Theme.SelectedBgColor));
            Assert.That(qt.Wm.ActiveBackground, Is.EqualTo(gtk.Header.Background));
            Assert.That(qt.ColorsView.ForegroundNegative, Is.EqualTo(gtk.Error.Color));
        });
    }

    [Test]
    public void ToGtk_MapsCorrectColors()
    {
        var qt = new MatugenColors();
        qt.ColorsWindow.BackgroundNormal = new RgbColor(10, 20, 30);
        qt.ColorsSelection.BackgroundNormal = new RgbColor(100, 150, 200);
        qt.Wm.ActiveBackground = new RgbColor(40, 50, 60);
        qt.ColorsView.ForegroundNegative = new RgbColor(255, 0, 0);

        var gtk = ThemeConverter.ToGtk(qt);

        Assert.Multiple(() =>
        {
            // Note: In ToGtk, gtk.Theme.BgColor maps from qt.ColorsWindow.BackgroundNormal
            Assert.That(gtk.Theme.BgColor, Is.EqualTo(qt.ColorsWindow.BackgroundNormal));
            Assert.That(gtk.Theme.SelectedBgColor, Is.EqualTo(qt.ColorsSelection.BackgroundNormal));
            // Note: In ToGtk, gtk.Titlebar.Background maps from qt.Wm.ActiveBackground
            Assert.That(gtk.Titlebar.Background, Is.EqualTo(qt.Wm.ActiveBackground));
            Assert.That(gtk.Error.Color, Is.EqualTo(qt.ColorsView.ForegroundNegative));
        });
    }

    [Test]
    public void RoundTrip_PreservesCoreColors()
    {
        var gtk = new GtkColors();
        gtk.Theme.BgColor = new RgbColor(10, 20, 30);
        gtk.Theme.BaseColor = new RgbColor(5, 10, 15);
        gtk.Theme.TextColor = new RgbColor(200, 200, 200);
        gtk.Theme.SelectedBgColor = new RgbColor(100, 150, 200);
        gtk.Theme.SelectedFgColor = new RgbColor(255, 255, 255);
        gtk.Button.BackgroundNormal = new RgbColor(50, 50, 50);
        gtk.Header.Background = new RgbColor(30, 30, 30);
        gtk.Header.Foreground = new RgbColor(220, 220, 220);

        var qt = ThemeConverter.ToQt(gtk);
        var backToGtk = ThemeConverter.ToGtk(qt);

        Assert.Multiple(() =>
        {
            Assert.That(backToGtk.Theme.BgColor, Is.EqualTo(gtk.Theme.BgColor));
            Assert.That(backToGtk.Theme.BaseColor, Is.EqualTo(gtk.Theme.BgColor)); // ToQt maps gtk.Theme to both View and Window
            Assert.That(backToGtk.Theme.SelectedBgColor, Is.EqualTo(gtk.Theme.SelectedBgColor));
            Assert.That(backToGtk.Button.BackgroundNormal, Is.EqualTo(gtk.Button.BackgroundNormal));
            // Header background might be affected by WM mapping in ToQt/ToGtk
            Assert.That(backToGtk.Header.Background, Is.EqualTo(gtk.Header.Background));
        });
    }
}
