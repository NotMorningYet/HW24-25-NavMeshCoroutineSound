using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _delayExplosion;
    [SerializeField] private float _radiusDetection;
    [SerializeField] private float _radiusDamage;
    [SerializeField] private float _strength;
    [SerializeField] private float _upwardModifier = 2;
    [SerializeField] private LayerMask _explodableMask;
    [SerializeField] private Animator _animator;

    private string _isTickingAnimation = "IsTicking";
    private float _timeToExplode;
    private ExplosionFactory _explosionFactory;
    private bool _isTickingToExplode;

    public void Initialize(ExplosionFactory explosionFactory)
    {
        _explosionFactory = explosionFactory;
        _timeToExplode = _delayExplosion;
        _isTickingToExplode = false;
    }

    public void Explode()
    {
        Explosion explosion = _explosionFactory.Get(transform, _strength);
        explosion.Initialize(transform.position, _strength, _radiusDamage, _upwardModifier);
        Destroy(gameObject);
    }

    private void Update()
    {
        CheskExplodableInRadius();
        CheckTickingToExplode();
    }

    private void CheckTickingToExplode()
    {
        if (_isTickingToExplode)
        {
            _timeToExplode -= Time.deltaTime;

            if (_timeToExplode <= 0)
                Explode();
        }
    }

    private void CheskExplodableInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusDetection, _explodableMask);

        foreach (Collider hit in colliders)
        {
            Explodable _explodable = hit.GetComponent<Explodable>();

            if (_explodable != null)
            {
                _isTickingToExplode = true;
                AnimationTicking();
                break;
            }
        }
    }

    private void AnimationTicking()
    {
        _animator.SetBool(_isTickingAnimation, true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusDetection);
    }
}
