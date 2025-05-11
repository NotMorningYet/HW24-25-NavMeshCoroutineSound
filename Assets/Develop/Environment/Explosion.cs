using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private LayerMask _explodableMask;
    [SerializeField] private float _lifeTime;
    [SerializeField] private AudioSource _audioSource;

    private float _strength;
    private float _radius;
    private Vector3 _position;

    public void Initialize(Vector3 position, float strength, float radius)
    {
        _strength = strength;
        _position = position;
        _radius = radius;
        PlayEffects();
        MakeDamage();
        DelayedDestroy();
    }

    private void PlayEffects()
    {
        _audioSource.Play();
        _explosionEffect.transform.position = transform.position;
        _explosionEffect.Play();
    }

    public void MakeDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(_position, _radius, _explodableMask);

        foreach (Collider hit in colliders)
        {
            Debug.Log(hit.gameObject.name.ToString());
            IExplodable explodable = hit.GetComponent<IExplodable>();
            explodable?.TakeExplosionEffect(transform.position, _strength, _radius);
        }
    }

    private void DelayedDestroy()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }


}
