namespace Ottergen.ColorModelsQt;

public class Wm
{
    public RgbColor ActiveBackground { get; set; } = new RgbColor();
    
    public RgbColor ActiveBlend { get; set; } = new RgbColor();
    
    public RgbColor ActiveForeground { get; set; } = new RgbColor();
    
    public RgbColor InactiveBackground { get; set; } = new RgbColor();
    
    public RgbColor InactiveBlend { get; set; } = new RgbColor();
    
    public RgbColor InactiveForeground { get; set; } = new RgbColor();
}