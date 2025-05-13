using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterAgent _character;
    [SerializeField] private TargetPointView _targetPointView;

    private MasterController _masterController;

    private void Awake()
    {
        _masterController = new MasterController(_character, _targetPointView);
        _masterController.Enable();
        _character.Initialize();
    }

    private void Update()
    {
        _masterController.Update(Time.deltaTime);

        if (_character.IsDead)
            _masterController.Disable();
    }
}

