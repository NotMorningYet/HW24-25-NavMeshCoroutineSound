using UnityEngine;

public class Explodable
{        
    public int CalculateDamage(Vector3 position, Vector3 explosionPosition, float strength, float radiusMax)
    {
        Debug.Log("Ёффект от взрыва получен");
        Vector3 forceDirection = position - explosionPosition;
        float distanceToExplosion = forceDirection.magnitude;
        int damage = Mathf.RoundToInt(strength * (1 - distanceToExplosion / radiusMax));
        
        if (damage < 0)
            damage = 0;

        Debug.Log($"”рон = {damage}");
        return damage;
    }
}
