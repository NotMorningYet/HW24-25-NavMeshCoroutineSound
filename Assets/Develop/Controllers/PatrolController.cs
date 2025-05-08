using UnityEngine;

public class PatrolController : MoveToPointController
{
    private Vector3 _centerOfPatrolingZone;
    private float _patrolingRadius = 10;
    private Vector3 _potentialTargetPosition;
    private float _offset = 0.1f;

    public PatrolController(Character character, Navigator navigator, IMoverListnener moverListnener)
        : base(character, navigator, moverListnener)
    {
    }

    public override void Enable()
    {
        base.Enable();
        _centerOfPatrolingZone = _character.CurrentPosition;
        _potentialTargetPosition = GetPotentialPatrolPoint();
        TrySetTargetPosition();
    }

    protected override void HandleTargetUpdate()
    {
        if (_isMoving == false)
        {
            _potentialTargetPosition = GetPotentialPatrolPoint();
            TrySetTargetPosition();
        }
    }

    protected override Ray GetRay()
    {
        Ray ray = new(_potentialTargetPosition + Vector3.up * _offset, Vector3.down);
        return ray;
    }

    private Vector3 GetPotentialPatrolPoint()
    {

        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float randomDistance = Mathf.Sqrt(Random.Range(0f, 1f)) * _patrolingRadius;

        float x = _centerOfPatrolingZone.x + randomDistance * Mathf.Cos(randomAngle);
        float z = _centerOfPatrolingZone.z + randomDistance * Mathf.Sin(randomAngle);
        float y = 0;

        return new Vector3(x, y, z);
    }
}
