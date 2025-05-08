using UnityEngine;

public abstract class MoveToPointController : Controller
{
    private readonly string _walkableMask = "Walkable";
    private Vector3 _targetPosition;
    private float _reachingDistance = 0.2f;
    private Navigator _navigator;
    private bool _isInvalidTarget;

    protected Character _character;
    protected bool _isMoving;
    protected IMoverListnener _moverListnener;

    protected abstract Ray GetRay();

    public MoveToPointController(Character character, Navigator navigator, IMoverListnener moverListnener)
    {
        _character = character;
        _navigator = navigator;
        _targetPosition = character.CurrentPosition;
        _moverListnener = moverListnener;
        _isMoving = false;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        HandleTargetUpdate();
        HandleMovement();
    }

    protected virtual void HandleMovement()
    {
        if (_isMoving)
        {
            if (IsTargetReached())
            {
                SetDirection(Vector3.zero);
                _isMoving = false;
                _targetPosition = _character.CurrentPosition;
                _moverListnener.OnStopMove();
                return;
            }

            SetDirection(GetTargetDirection());
        }
    }

    protected virtual void HandleTargetUpdate()
    {

    }

    protected bool TrySetTargetPosition()
    {
        _targetPosition = GetTargetWorldPosition(out _isInvalidTarget);
        if (_isInvalidTarget)
            return false;

        Vector3 direction = GetTargetDirection();
        
        if (direction != Vector3.zero) 
        {
            _isMoving = true;
            _moverListnener.OnStartMove(_targetPosition);
            return true;
        }

        return false;
    }

    private Vector3 GetTargetDirection()
    {
        return _navigator.GetDirection(_character.CurrentPosition, _targetPosition).normalized;
    }

    private bool IsTargetReached()
    {
        Vector3 toTarget = _character.CurrentPosition - _targetPosition;
        toTarget.y = 0;
        float distance = toTarget.magnitude;

        return distance <= _reachingDistance;
    }

    private void SetDirection(Vector3 direction)
    {
        _character.SetMoveDirection(direction);
        _character.SetRotationDirection(direction);
    }

    private Vector3 GetTargetWorldPosition(out bool _isInvalidTarget)
    {        
        _isInvalidTarget = false;
        Vector3 position = new();
        Ray ray = GetRay();

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask(_walkableMask)))
            position = hit.point;
        else 
            _isInvalidTarget = true;

        return position;
    }
}
