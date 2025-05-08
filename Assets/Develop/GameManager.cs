using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private TargetPointView _targetPointView;

    private void Awake()
    {
        _character.Initialize(_targetPointView);
    }
}

