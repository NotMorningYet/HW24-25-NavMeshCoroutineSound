using UnityEngine;

public interface IExplodable
{
    public void TakeExplosionEffect(Vector3 explosionPosition, float explosionStrength, float explosionRadius, float upwardModifier);
}
