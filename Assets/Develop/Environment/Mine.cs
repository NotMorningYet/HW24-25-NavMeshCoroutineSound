using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _delayExplosion;
    [SerializeField] private float _radiusDetection;
    [SerializeField] private float _radiusDamage;
    [SerializeField] private float _strength;
    [SerializeField] private LayerMask _explodableMask;
    [SerializeField] private Animator _animator;

    private string _isTickingAnimation = "IsTicking";    
    private ExplosionFactory _explosionFactory;

    public void Initialize(ExplosionFactory explosionFactory)
    {
        _explosionFactory = explosionFactory;
    }

    public void CreateExplosion()
    {
        Explosion explosion = _explosionFactory.Get(transform, _strength);
        explosion.Initialize(transform.position, _strength, _radiusDamage);
        Destroy(gameObject);
    }

    private void Update()
    {
        CheskExplodableInRadius();
    }

    private IEnumerator DelayBeforeExplosion()
    {
        yield return new WaitForSeconds(_delayExplosion);
        CreateExplosion();
    }

    private void CheskExplodableInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusDetection, _explodableMask);

        foreach (Collider hit in colliders)
        {
            IExplodable _explodable = hit.GetComponent<IExplodable>();

            if (_explodable != null)
            {                
                StartReactionToExplodable();
                break;
            }
        }
    }

    private void StartReactionToExplodable()
    {
        StartCoroutine(DelayBeforeExplosion());
        AnimationTicking();
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
