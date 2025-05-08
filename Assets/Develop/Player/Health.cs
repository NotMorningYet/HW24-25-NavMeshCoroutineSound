using UnityEngine;

public class Health: ITakeDamagable
{
    private int _health;
    private int _maxHealth;
    private float _injuredPercent;
    private CharacterView _characterView;
    private bool _died;

    public Health(CharacterView characterView, int maxHealth, int injuredPercent)
    {
        _characterView = characterView;
        _maxHealth = maxHealth;
        _health = maxHealth;
        _injuredPercent = injuredPercent;
        _died = false;
    }

    public int HP =>_health;
    public bool Injured => _health <=_maxHealth * _injuredPercent / 100;
    public bool Died => _died;

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _health -= damage;
            _characterView.GetHit();
        }

        if (_health <= 0)
        {
            _health = 0;
            _characterView.Die();
            Die();
        }

        Debug.Log($"Çהמנמגüו: {_health}");
    }

    private void Die()
    {
        _died = true;
    }
}
