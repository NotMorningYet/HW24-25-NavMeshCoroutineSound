using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] private SoundSource _soundSource;    

    public void StepEvent()
    {
        _soundSource.PlayStep();
    }

    public void Dying()
    {
        _soundSource.DyingSoundPlay();
    }

    public void Hitted()
    {
        _soundSource.PainSoundPlay();
    }

}
