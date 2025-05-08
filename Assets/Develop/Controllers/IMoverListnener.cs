using UnityEngine;

public interface IMoverListnener
{
    public void OnStartMove(Vector3 targetPosition);
    public void OnStopMove();
}
