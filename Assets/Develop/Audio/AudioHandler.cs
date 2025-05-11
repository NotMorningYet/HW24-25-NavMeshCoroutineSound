using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler
{
    private AudioMixer _audioMixer;

    private const float OffVolumeValue = -80;
    private const float OnVolumeValue = 0;

    private const string MusicKey = "MusicVolume";
    private const string SoundsKey = "SoundsVolume";

    public AudioHandler(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    private bool IsVolumeOn(string key)
        => _audioMixer.GetFloat(key, out float volume) && Mathf.Abs(volume - OnVolumeValue) <= 0.01f;

    private void OnVolume (string key) => _audioMixer.SetFloat(key, OnVolumeValue);
    private void OffVolume (string key) => _audioMixer.SetFloat(key, OffVolumeValue);

    public bool IsMusicOn() => IsVolumeOn(MusicKey);
    public bool IsSoundsOn() => IsVolumeOn(SoundsKey);

    public void OffMusic() => OffVolume(MusicKey);
    public void OnMusic() => OnVolume(MusicKey);
    
    public void OffSounds() => OffVolume(SoundsKey);
    public void OnSounds() => OnVolume(SoundsKey);



}
