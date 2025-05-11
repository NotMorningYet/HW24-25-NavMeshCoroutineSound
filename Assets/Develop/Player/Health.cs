using UnityEngine;

public class Health : ISpeedModifier
{
    private int _health;
    private int _maxHealth;
    private float _percentToInjured;
    private bool _died;
    private float _injuredSpeedModifier = 0.4f;

    public Health( int maxHealth, int percentToInjured)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
        _percentToInjured = percentToInjured;
        _died = false;
    }

    public int HP => _health;
    public bool Injured => _health <= _maxHealth * _percentToInjured / 100;
    public bool Died => _died;

    public void ChangeHealth(int deltaHealth)
    {
        _health += deltaHealth;

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }

        if (_health > _maxHealth)
            _health = _maxHealth;        

        Debug.Log($"Çהמנמגüו: {_health}");
    }

    public float GetModifier()
    {
        if (Injured)
            return _injuredSpeedModifier;
        
        return 1f;
        
    }

    private void Die()
    {                
        _died = true;
    }
}
