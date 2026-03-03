using NUnit.Framework;
using Ottergen;

namespace Ottergen.Tests;

[TestFixture]
public class MatugenParserGtkTests
{
    private const string GtkContent = """
@define-color borders_breeze #5e5e5e;
@define-color content_view_bg_breeze #242424;
@define-color error_color_backdrop_breeze #da4453;
@define-color theme_selected_bg_color_breeze #7157aa;
@define-color theme_selected_fg_color_breeze #ffffff;
""";

    [Test]
    public void Parse_ValidGtkContent_ReturnsCorrectColors()
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, GtkContent);
        try
        {
            var parser = new MatugenParserGtk(tempFile);
            var result = parser.Parse();

            Assert.Multiple(() =>
            {
                Assert.That(result.Borders.R, Is.EqualTo(0x5e));
                Assert.That(result.Borders.G, Is.EqualTo(0x5e));
                Assert.That(result.Borders.B, Is.EqualTo(0x5e));

                Assert.That(result.Theme.SelectedBgColor.R, Is.EqualTo(0x71));
                Assert.That(result.Theme.SelectedBgColor.G, Is.EqualTo(0x57));
                Assert.That(result.Theme.SelectedBgColor.B, Is.EqualTo(0xaa));

                Assert.That(result.Theme.SelectedFgColor.R, Is.EqualTo(0xff));
                Assert.That(result.Theme.SelectedFgColor.G, Is.EqualTo(0xff));
                Assert.That(result.Theme.SelectedFgColor.B, Is.EqualTo(0xff));
            });
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Test]
    public void Parse_ShortHex_ReturnsCorrectColors()
    {
        const string content = "@define-color borders #112233;";
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, content);
        try
        {
            var parser = new MatugenParserGtk(tempFile);
            var result = parser.Parse();

            var color = result.Borders;
            Assert.Multiple(() =>
            {
                Assert.That(color.R, Is.EqualTo(0x11));
                Assert.That(color.G, Is.EqualTo(0x22));
                Assert.That(color.B, Is.EqualTo(0x33));
            });
        }
        finally
        {
            File.Delete(tempFile);
        }
    }
}
