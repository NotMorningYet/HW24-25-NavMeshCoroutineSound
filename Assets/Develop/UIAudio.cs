using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class UIAudio : MonoBehaviour
{
    [SerializeField] private TMP_Text _musicButtonText;
    [SerializeField] private TMP_Text _soundsButtonText;
    [SerializeField] private AudioMixer _audioMixer;

    private AudioHandler _audioHandler;

    private bool _musicIsOn;
    private bool _soundsIsOn;
    
    private const string _musicOn = "Music ON";
    private const string _musicOff = "Music OFF";
    private const string _soundsOn = "Sounds ON";
    private const string _soundsOff = "Sounds OFF";

    private void Awake()
    {
        _audioHandler = new AudioHandler(_audioMixer);
        _musicIsOn = true;
        _soundsIsOn = true;
        _musicButtonText.text = _musicOn;
        _soundsButtonText.text = _soundsOn;
    }

    public void SwitchMusicOnOff()
    {
        if (_musicIsOn)
        {
            _audioHandler.OffMusic();
            _musicButtonText.text = _musicOff;
            _musicIsOn = false;
        }
        else
        {
            _audioHandler.OnMusic();
            _musicButtonText.text = _musicOn;
            _musicIsOn = true;
        }
    }

    public void SwitchSoundsOnOff()
    {
        if (_soundsIsOn)
        {
            _audioHandler.OffSounds();
            _soundsButtonText.text = _soundsOff;
            _soundsIsOn= false;
        }
        else
        {
            _audioHandler.OnSounds();
            _soundsButtonText.text = _soundsOn;
            _soundsIsOn = true;
        }
    }
}
