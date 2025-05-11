using UnityEngine;
using UnityEngine.AI;

public class AgentMover
{
    private NavMeshAgent _agent;
    private float _normalSpeed;
    private SpeedModifierComposite _speedModifiers;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;

    public AgentMover(NavMeshAgent agent, float movementSpeed)
    {
        _speedModifiers = new SpeedModifierComposite();
        _agent = agent;
        _normalSpeed = movementSpeed;
        _agent.speed = _normalSpeed;
        _agent.acceleration = 999;
    }

    public void AddModifier(ISpeedModifier modifier) => _speedModifiers.AddModifier(modifier);
    public void RemoveModifier(ISpeedModifier modifier) => _speedModifiers.RemoveModifier(modifier);

    public void SetDestination(Vector3 position)
    {
        SetSpeed();
        _agent.SetDestination(position);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void Resume()
    {
        _agent.isStopped = false;
    }

    private void SetSpeed()
    {
        _agent.speed = _normalSpeed * _speedModifiers.GetTotalModifier();
    }
}
