using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : Controller
{
    private CharacterAgent _character;
    private Dictionary<ControllersTypes, Controller> _controllers;
    private Controller _currentController;
    private ControllerSwitcher _controllerSwitcher;

    private const int leftMouseButton = 0;
    private float _lazyTimeBeforePatrol = 5;
    private Coroutine _lazyCoroutine;
    private MonoBehaviour _coroutineRunner;
    private bool _isLazy;

    public MasterController(CharacterAgent character, Dictionary<ControllersTypes, Controller> controllers, Controller defaultController)
    {
        _controllers = controllers;
        _character = character;
        _currentController = defaultController;
        _coroutineRunner = character as MonoBehaviour; ;

        Initialize();
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _currentController?.Update(deltaTime);
        SwitchingLogic();
    }

    public override void Disable()
    {
        base.Disable();
        _controllerSwitcher.DisableAll();

        if (_lazyCoroutine != null)
        {
            _coroutineRunner.StopCoroutine(_lazyCoroutine);
            _lazyCoroutine = null;
        }
    }

    private void Initialize()
    {
        _controllerSwitcher = new ControllerSwitcher(_controllers);
        _currentController.Enable();
        _isLazy = true;
        StartLazyTimer();

    }

    private void SwitchingLogic()
    {
        switch (_currentController)
        {
            case NavMeshAgentMouseController:
                MouseClickLogic();
                break;
            case NavMeshPatrolController:
                PatrolLogic();
                break;
            default:
                break;
        }
    }

    private void PatrolLogic()
    {
        if (Input.GetMouseButtonDown(leftMouseButton))
        {
            NavMeshAgentMouseController controller = (NavMeshAgentMouseController)_controllers[ControllersTypes.MouseClick];

            if (controller.TryActivate())
            {
                _isLazy = true;
                StartLazyTimer();
                SetController(ControllersTypes.MouseClick);
            }
        }
    }

    private void MouseClickLogic()
    {
        if (_isLazy)
        {
            if (_character.CurrentVelocity.magnitude >= 0.05f)
            {
                _isLazy = false;

                if (_lazyCoroutine != null)
                {
                    _coroutineRunner.StopCoroutine(_lazyCoroutine);
                    _lazyCoroutine = null;
                }

                return;
            }
        }
        else
        {
            if (_character.CurrentVelocity.magnitude < 0.05f)
            {
                _isLazy = true;
                StartLazyTimer();
                return;
            }
        }
    }

    private void SetController(ControllersTypes controllerType)
    {
        _currentController = _controllerSwitcher.SetController(controllerType);
    }

    private void StartLazyTimer()
    {
        if (_lazyCoroutine != null)
            _coroutineRunner.StopCoroutine(_lazyCoroutine);

        _lazyCoroutine = _coroutineRunner.StartCoroutine(LazyTimerCoroutine());
    }

    private IEnumerator LazyTimerCoroutine()
    {
        yield return new WaitForSeconds(_lazyTimeBeforePatrol);

        if (_isLazy && _currentController is NavMeshAgentMouseController)
            SetController(ControllersTypes.Patrol);
    }
}
