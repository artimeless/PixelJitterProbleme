using System;

namespace Unknown1.Source.Data;

internal class AudioSettings
{
    private uint _mainVolume = 50;
    private uint _musicvolume = 100;
    private uint _soundEffectsvolume = 100;


    public uint MainVolume { get => _mainVolume; set => _mainVolume = Math.Clamp(value, 0, 100); }

    public uint MusicVolume { get => _musicvolume; set => _musicvolume = Math.Clamp(value, 0, 100); }

    public uint SoundEffectsVolume { get => _soundEffectsvolume; set => _soundEffectsvolume = Math.Clamp(value, 0, 100); }
}
