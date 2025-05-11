using UnityEngine;

public class TargetPointView : MonoBehaviour, IMoverListener
{
    [SerializeField] private Animator _animator;
    private Vector3 _position;
    
    public void Enable()
    {
        transform.position = _position;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnStartMove(Vector3 targetPosition)
    {
        _position = targetPosition;
        Enable();
    }

    public void OnStopMove()
    {
        Disable();
    }
}
