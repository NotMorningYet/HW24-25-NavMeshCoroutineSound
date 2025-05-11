using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper 
{
    private float _speed;
    private float _jumpDuration;
    private NavMeshAgent _agent;
    private AnimationCurve _yOffsetCurve;
    private MonoBehaviour _coroutineRunner;
    private CharacterAgent _character;

    private Coroutine _jumpProcess;

    public AgentJumper(float speed, NavMeshAgent agent, AnimationCurve yOffsetCurve, CharacterAgent character)
    {
        _speed = speed;
        _agent = agent;
        _coroutineRunner = character as MonoBehaviour;
        _yOffsetCurve = yOffsetCurve;
        _character = character;
    }

    public float JumpDuration => _jumpDuration;
    public bool InProcess => _jumpProcess != null;

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        if (InProcess)
            return;

        _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));
    }

    private IEnumerator JumpProcess (OffMeshLinkData offMeshLinkData)
    {
        Vector3 startPosition = _character.CurrentPosition;//offMeshLinkData.startPos;
        Vector3 endPosition = offMeshLinkData.endPos;

        _jumpDuration = Vector3.Distance(startPosition, endPosition) / _speed;
        float progress = 0;

        while (progress < _jumpDuration)
        {
            float yOffset = _yOffsetCurve.Evaluate(progress/_jumpDuration);
            _agent.transform.position = Vector3.Lerp(startPosition, endPosition, progress/_jumpDuration) + Vector3.up * yOffset;
            progress +=Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _jumpProcess = null;
    }
}
