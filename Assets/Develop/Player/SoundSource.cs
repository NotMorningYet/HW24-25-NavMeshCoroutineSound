using System.Collections;
using UnityEngine;

public class SoundSource : MonoBehaviour 
{
    [SerializeField] private AudioSource _voiceSource;
    [SerializeField] private AudioSource _stepSource;
    [SerializeField] private AudioClip _painClip;
    [SerializeField] private AudioClip _dyingClip;
    
    public void PainSoundPlay()
    {
        _voiceSource.clip = _painClip;
        PlaySound();
    }

    public void DyingSoundPlay()
    {    
        _voiceSource.clip = _dyingClip;
        PlaySound();
    }    
    
    private void PlaySound()
    {        
        _voiceSource.Play();
    }

    public void PlayStep()
    {
        _stepSource.Play();
    }
}
