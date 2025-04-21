namespace Unknown1.Source.Data;

internal class Settings
{
    public InputSettings Input { get; set; } = new();

    public GraphicsSettings Graphics { get; set; } = new();

    public AudioSettings Audio { get; set; } = new();
}
