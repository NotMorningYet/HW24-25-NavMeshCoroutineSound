using UnityEngine;

public abstract class Navigator
{
    protected Vector3 _currentPosition;
    protected Vector3 _tartgetPositon;

    public abstract Vector3 GetDirection(Vector3 currentPosition, Vector3 targetPosition);
}
