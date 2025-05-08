using System.Collections.Generic;

public class SpeedModifierComposite
{
    private readonly List<ISpeedModifier> _modifiers = new List<ISpeedModifier>();

    public void AddModifier(ISpeedModifier speedModifier) => _modifiers.Add(speedModifier);
    public void RemoveModifier(ISpeedModifier speedModifier) => _modifiers.Remove(speedModifier);

    public float GetTotalModifier()
    {
        float result = 1f;

        foreach(ISpeedModifier modifier in _modifiers)
            result *=modifier.GetModifier();

        return result;
    }
}
