namespace Unknown1.Source.Data;

internal class SettingsManager : FileManager<Settings>
{
    public SettingsManager() : base("user_settings.json") { }

    protected override void CleanNullProperties(Settings Data)
    {
        Data.Input ??= new();
        Data.Graphics ??= new();
        Data.Audio ??= new();
    }
}
