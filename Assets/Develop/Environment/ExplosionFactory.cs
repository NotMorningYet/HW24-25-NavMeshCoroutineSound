using UnityEngine;

public class ExplosionFactory : MonoBehaviour
{
    [SerializeField] private Explosion _explosionPrefab;

    public Explosion Get(Transform transform, float maxDamage)
    {
        Explosion explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        return explosion;
    }
}
