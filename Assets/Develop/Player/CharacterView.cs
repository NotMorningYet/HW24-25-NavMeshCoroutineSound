using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int GetHitKey = Animator.StringToHash("GetHit");
    private readonly int DiedKey = Animator.StringToHash("Died");
    private int _injuredLayer = 1;
    private int _layerOff = 0;
    private int _layerOn = 1;

    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;

    private void Update()
    {
        if (_character.Health.Died)
            return;

        if (_character.Health.Injured)        
            SetInjuredAnimations();        
        else
            SetHealthyAnimations();

        if (_character.CurrentVelocity.magnitude > 0.05f)
            StartRunning();
        else
            StopRunning();
    }

    public void Die()
    {
        _animator.SetTrigger(DiedKey);
    }

    public void GetHit()
    {
        _animator.SetTrigger(GetHitKey);
    }

    private void SetHealthyAnimations()
    {
        _animator.SetLayerWeight(_injuredLayer, _layerOff);
    }

    private void SetInjuredAnimations()
    {
        _animator.SetLayerWeight(_injuredLayer, _layerOn);
    }

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }
}
