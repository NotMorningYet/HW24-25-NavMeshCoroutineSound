using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
{
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;
    private Health _health;

    [SerializeField] private CharacterView _characterView;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _timeBeforePatrolMax;
    [SerializeField] private float _radiusOfPatrol;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _woundPercent;
    [SerializeField] private Explodable _explodable;

    private BehaviorSelecter _behaviorSelecter;
    private Dictionary<ControllersTypes, Controller> _controllers = new Dictionary<ControllersTypes, Controller>();
    private Controller _defaultController;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Vector3 CurrentPosition => transform.position;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public int HP => _health.HP;
    public Health Health => _health;
    public Explodable Explodable => _explodable;

    public void Initialize(IMoverListnener moverListnener)
    {
        _health = new Health(_characterView, _maxHealth, _woundPercent);

        Navigator navigator = new NavMeshNavigator();
        CreateControllers(navigator, moverListnener);

        _behaviorSelecter = new BehaviorSelecter(this, _controllers, _defaultController);
        _explodable.Initialize(_health, transform);
        
        _mover = new DirectionalMover(GetComponent<CharacterController>(), _moveSpeed);
        _mover.AddModifier(new HealthSpeedModifier(_health));

        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }
        
    private void Update()
    { 
        _behaviorSelecter.Update(Time.deltaTime);
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    private void CreateControllers(Navigator navigator, IMoverListnener moverListnener)
    {
        _controllers.Add(ControllersTypes.MouseClick, new MouseClickController(this, navigator, moverListnener));
        _controllers.Add(ControllersTypes.Patrol, new PatrolController(this, navigator, moverListnener));
        _defaultController = _controllers[ControllersTypes.MouseClick];
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
}
