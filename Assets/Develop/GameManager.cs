using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterAgent _character;
    [SerializeField] private TargetPointView _targetPointView;

    private void Awake()
    {
        MasterController _masterController = new MasterController(_character, _targetPointView);
        _masterController.Enable();
        _character.Initialize(_masterController);
    }
}

