using UnityEngine;

public interface IMoverListener
{
    public void OnStartMove(Vector3 targetPosition);
    public void OnStopMove();
}
