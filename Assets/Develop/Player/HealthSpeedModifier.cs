public class HealthSpeedModifier : ISpeedModifier
{
    private float _injuredMultiplier = 0.5f;
    private float _healthyMultiplier = 1f;
    private Health _health;
    
    public HealthSpeedModifier(Health health)
    {
        _health = health;
    }

    public float GetModifier()
    {
        if (_health.IsInjured)
            return _injuredMultiplier;
        else 
            return _healthyMultiplier;
    }
}
