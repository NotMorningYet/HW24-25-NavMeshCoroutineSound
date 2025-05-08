using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private LayerMask _explodableMask;
    [SerializeField] private float _lifeTime;

    private float _strength;
    private float _radius;
    private float _upwardModifier;
    private Vector3 _position;
    private float _timeToDestroy;

    public void Initialize(Vector3 position, float strength, float radius, float upwardModifier)
    {
        _strength = strength;
        _position = position;
        _radius = radius;
        _upwardModifier = upwardModifier;
        _timeToDestroy = _lifeTime;
        MakeDamage();
    }

    private void Update()
    {
        _timeToDestroy -= Time.deltaTime;

        if (_timeToDestroy <= 0 )
            Destroy(gameObject);
    }

    public void MakeDamage()
    {
        {
            _explosionEffect.transform.position = transform.position;
            _explosionEffect.Play();

            Collider[] colliders = Physics.OverlapSphere(_position, _radius, _explodableMask);
  
            foreach (Collider hit in colliders)
            {
                Explodable explodable = hit.GetComponent<Explodable>();
            
                explodable?.TakeExplosionEffect(_position, _strength, _radius, _upwardModifier);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
