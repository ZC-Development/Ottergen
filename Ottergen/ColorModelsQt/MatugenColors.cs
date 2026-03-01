namespace Ottergen.ColorModelsQt;

public class MatugenColors
{
    public ColorEffectsDisabled ColorEffectsDisabled { get; set; } = new ColorEffectsDisabled();

    public ColorEffectsInactive ColorEffectsInactive { get; set; } = new ColorEffectsInactive();

    public Wm Wm { get; set; } = new Wm();
    
    public ColorsButton ColorsButton { get; set; } = new ColorsButton();
    
    public ColorsComplementary ColorsComplementary { get; set; } = new ColorsComplementary();
    
    public ColorsHeader ColorsHeader { get; set; } = new ColorsHeader();
    
    public ColorsHeaderInactive ColorsHeaderInactive { get; set; } = new ColorsHeaderInactive();
    
    public ColorsSelection ColorsSelection { get; set; } = new ColorsSelection();
    
    public ColorsTooltip ColorsTooltip { get; set; } = new ColorsTooltip();
    
    public ColorsView ColorsView { get; set; } = new ColorsView();
    
    public ColorsWindow ColorsWindow { get; set; } = new ColorsWindow();
    
}