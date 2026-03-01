namespace Ottergen.ColorModelsQt;

public abstract class AbstractQtColors
{
    public RgbColor BackgroundAlternate { get; set; } = new RgbColor();
    
    public RgbColor BackgroundNormal { get; set; } = new RgbColor();
    
    public RgbColor DecorationFocus { get; set; } = new RgbColor();
    
    public RgbColor DecorationHover { get; set; } = new RgbColor();
    
    public RgbColor ForegroundActive { get; set; } = new RgbColor();
    
    public RgbColor ForegroundInactive { get; set; } = new RgbColor();
    
    public RgbColor ForegroundLink { get; set; } = new RgbColor();
    
    public RgbColor ForegroundNegative { get; set; } = new RgbColor();
    
    public RgbColor ForegroundNeutral { get; set; } = new RgbColor();
    
    public RgbColor ForegroundNormal { get; set; } = new RgbColor();
    
    public RgbColor ForegroundPositive { get; set; } = new RgbColor();
    
    public RgbColor ForegroundVisited { get; set; } = new RgbColor();
}