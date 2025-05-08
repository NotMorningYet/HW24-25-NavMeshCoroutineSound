using UnityEngine;

public class Explodable : MonoBehaviour, IExplodable
{
    private ITakeDamagable _healthComponent;
    private Transform _explodableTransform;
        
    public void Initialize(ITakeDamagable healthComponent, Transform explodableTransform)
    {
        _healthComponent = healthComponent;
        _explodableTransform = explodableTransform;
    }

    public void TakeExplosionEffect(Vector3 explosionPosition, float explosionStrength, float explosionRadius, float upwardModifier)
    {
        Debug.Log("Ёффект от взрыва получен");
        Vector3 forceDirection = explosionPosition - _explodableTransform.position;
        float distanceToExplosion = forceDirection.magnitude;

        int explosionForce = CalculateForce(distanceToExplosion, explosionStrength, explosionRadius);

        _healthComponent.TakeDamage(explosionForce);
    }

    private int CalculateForce(float distanceToExplosion, float explosionStrength, float explosionradius)
    {
        int force = Mathf.RoundToInt(explosionStrength * (1 - distanceToExplosion / explosionradius));
        Debug.Log($"”рон = {force}");
        return force;
    }
}
