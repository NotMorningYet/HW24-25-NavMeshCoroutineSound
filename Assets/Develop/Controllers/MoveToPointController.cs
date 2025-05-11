using UnityEngine;
using UnityEngine.AI;

public abstract class MoveToPointController : Controller
{
    private readonly string _walkableMask = "Walkable";
    private Vector3 _targetPosition;
    private float _reachingDistance = 0.2f;
    private bool _isInvalidTarget;
    private NavMeshPath _pathToTarget = new NavMeshPath();

    protected CharacterAgent _character;
    protected bool _isMoving;
    protected IMoverListener _moverListnener;

    protected abstract Ray GetRay();

    public MoveToPointController(CharacterAgent character,  IMoverListener moverListnener)
    {
        _character = character;
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
            if (_character.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
            {
                if (_character.IsInJump == false)
                {
                    _character.SetRotationDirection(offMeshLinkData.endPos - _character.CurrentPosition);
                    _character.Jump(offMeshLinkData);
                }
                return;
            }

            _character.TryGetPath(_targetPosition, _pathToTarget);

            if (IsTargetReached())
            {
                _isMoving = false;
                _character.StopMove();
                _moverListnener.OnStopMove();
                return;
            }

            _character.SetDestination(_targetPosition);
        }
    }

    protected virtual void HandleTargetUpdate() { }

    protected bool TrySetTargetPosition()
    {
        Vector3 potentialTargetPosition = GetTargetWorldPosition(out _isInvalidTarget);        

        if (_isInvalidTarget)
            return false;
        else 
            _targetPosition = potentialTargetPosition;
                
        if (_character.TryGetPath(_targetPosition, _pathToTarget)) 
        {
            _isMoving = true;
            _moverListnener.OnStartMove(_targetPosition);
            _character.ResumeMove();
            return true;
        }

        return false;
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

    private bool IsTargetReached() 
        => NavMeshUtils.GetPathLength(_pathToTarget) <= _reachingDistance;
}
