using UnityEngine;
using UnityEngine.AI;

public class CharacterAgent : MonoBehaviour, IDirectionalRotatable, IExplodable, ITakeDamagable, IDying
{
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _percentToInjured;    
    [SerializeField] private AnimationCurve _jumpCurve;

    private NavMeshAgent _agent;
    private DirectionalRotator _rotator;
    private Health _health;
    private AgentMover _mover;
    private AgentJumper _jumper;
    private Explodable _explodable;
    private MasterController _masterController;


    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Vector3 CurrentPosition => transform.position;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public int HP => _health.HP;
    public bool IsInjured => _health.IsInjured;
    public bool IsDead { get; private set; }

    public void Initialize(MasterController masterController)
    {
        _masterController = masterController;

        _health = new Health(_maxHealth, _percentToInjured);
        _explodable = new Explodable();

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _normalSpeed);
        _mover.AddModifier(_health);

        _jumper = new AgentJumper(_jumpSpeed, _agent, _jumpCurve, this);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        IsDead = false;
    }

    private void Update()
    {
        if (IsDead)
            return;

        if (_health.IsDead)
        {
            Die();
            return;
        }

        UpdateControllers();
    }

    private void UpdateControllers()
    {
        _masterController.Update(Time.deltaTime);
        _rotator.SetInputDirection(_agent.desiredVelocity);
        _rotator.Update(Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (damage >= 0)
        {
            _health.ChangeHealth(-damage);
            _characterView.GetHit();
        }
    }

    public void TakeExplosionEffect(Vector3 explosionPosition, float explosionStrength, float radiusMax)
    {
        Debug.Log("Ёффект от взрыва получен");

        int explosionDamage = _explodable.CalculateDamage(transform.position, explosionPosition, explosionStrength, radiusMax);

        TakeDamage(explosionDamage);
    }

    public void Die()
    {
        IsDead = true;
        _masterController.Disable();
        _characterView.Die();
    }

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetDestination(Vector3 targetPosition) => _mover.SetDestination(targetPosition);

    public void SetRotationDirection(Vector3 inputDirection) =>_rotator.SetInputDirection(inputDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
        => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);    

    public void Jump(OffMeshLinkData offMeshLinkData) =>_jumper.Jump(offMeshLinkData);

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default(OffMeshLinkData);
        return false;
    }

    public bool IsInJump => _jumper.InProcess;

    public float JumpDuration => _jumper.JumpDuration;
}
