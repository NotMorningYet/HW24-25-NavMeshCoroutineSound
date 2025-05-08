using UnityEngine;

public class DirectionalMover
{
    private CharacterController _characterController;
    private float _movementSpeed;
    private Vector3 _currentDirection;
    private SpeedModifierComposite _speedModifiers;

    public Vector3 CurrentVelocity { get; private set; }

    public DirectionalMover(CharacterController characterController, float movementSpeed)
    {
        _characterController = characterController;
        _movementSpeed = movementSpeed;
        _speedModifiers = new SpeedModifierComposite();
    }

    public void AddModifier(ISpeedModifier modifier) => _speedModifiers.AddModifier(modifier);
    public void RemoveModifier(ISpeedModifier modifier) => _speedModifiers.RemoveModifier(modifier);

    public void SetInputDirection(Vector3 direction) => _currentDirection = direction;

    public void Update(float deltaTime)
    {
        CurrentVelocity = _currentDirection.normalized * _movementSpeed * _speedModifiers.GetTotalModifier();
        _characterController.Move(CurrentVelocity * deltaTime);
    }
}
