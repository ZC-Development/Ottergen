using System;
using System.IO;
using NUnit.Framework;
using Ottergen;
using Ottergen.ColorModelsQt;

[TestFixture]
public class MatugenColorsTests
{
    private static string LoadConfFile()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            var file = Path.Combine(dir.FullName, "matugen.conf");
            if (File.Exists(file)) return File.ReadAllText(file);
            dir = dir.Parent;
        }

        return dir.ToString();
    }

    private static readonly Lazy<MatugenColors> _parsed =
        new(() =>
        {
            var content = LoadConfFile();
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, content);
            try
            {
                return new MatugenParserQt(tempFile).Parse();
            }
            finally
            {
                File.Delete(tempFile);
            }
        });

    private static MatugenColors Parsed => _parsed.Value;

    [Test]
    public void Parse_ColorEffectsDisabled_IsCorrect()
    {
        var ce = Parsed.ColorEffectsDisabled;
        Assert.Multiple(() =>
        {
            Assert.That(ce.Color.R, Is.EqualTo(255));
            Assert.That(ce.ColorAmount, Is.EqualTo(0.0));
            Assert.That(ce.ContrastAmount, Is.EqualTo(0.65));
        });
    }

    [Test]
    public void Parse_ColorEffectsInactive_IsCorrect()
    {
        var ce = Parsed.ColorEffectsInactive;
        Assert.Multiple(() =>
        {
            Assert.That(ce.ChangeSelectionColor, Is.True);
            Assert.That(ce.Color.R, Is.EqualTo(85));
            Assert.That(ce.Enable, Is.False);
        });
    }

    [Test]
    public void Parse_Wm_IsCorrect()
    {
        var wm = Parsed.Wm;
        Assert.Multiple(() =>
        {
            Assert.That(wm.ActiveBackground.R, Is.EqualTo(0));
            Assert.That(wm.ActiveBlend.R, Is.EqualTo(230));
            Assert.That(wm.ActiveForeground.R, Is.EqualTo(230));
        });
    }

    [Test]
    public void Parse_ColorsButton_IsCorrect()
    {
        var cb = Parsed.ColorsButton;
        Assert.Multiple(() =>
        {
            Assert.That(cb.BackgroundAlternate.R, Is.EqualTo(0));
            Assert.That(cb.ForegroundNormal.R, Is.EqualTo(230));
            Assert.That(cb.DecorationFocus.R, Is.EqualTo(204));
        });
    }

    [Test]
    public void Parse_ColorsHeaderInactive_IsCorrect()
    {
        var chi = Parsed.ColorsHeaderInactive;
        Assert.Multiple(() =>
        {
            Assert.That(chi.ForegroundNormal.R, Is.EqualTo(230));
        });
    }
    
    [TestFixture]
    public class RgbColorTests
    {
        [Test]
        public void Parse_ValidString_ReturnsCorrectValues()
        {
            var c = RgbColor.Parse("204,0,255");
            Assert.Multiple(() =>
            {
                Assert.That(c.R, Is.EqualTo(204));
                Assert.That(c.G, Is.EqualTo(0));
                Assert.That(c.B, Is.EqualTo(255));
            });
        }

        [Test]
        public void Parse_WithSpaces_ReturnsCorrectValues()
        {
            var c = RgbColor.Parse("204, 0, 255");
            Assert.That(c.R, Is.EqualTo(204));
            Assert.That(c.G, Is.EqualTo(0));
            Assert.That(c.B, Is.EqualTo(255));
        }

        [Test]
        public void TryParse_NullInput_ReturnsFalse() =>
            Assert.That(RgbColor.TryParse(null, out _), Is.False);

        [Test]
        public void TryParse_InvalidInput_ReturnsFalse() =>
            Assert.That(RgbColor.TryParse("not-a-color", out _), Is.False);

        [Test]
        public void TryParse_ValidInput_ReturnsTrueAndColor()
        {
            var success = RgbColor.TryParse("153,153,153", out var c);
            Assert.That(success, Is.True);
            Assert.That(c.R, Is.EqualTo(153));
        }
        
        [Test]
        public void Default_IsAllZeros()
        {
            var c = default(RgbColor);
            Assert.That(c.R, Is.EqualTo(0));
            Assert.That(c.G, Is.EqualTo(0));
            Assert.That(c.B, Is.EqualTo(0));
        }
    }
}