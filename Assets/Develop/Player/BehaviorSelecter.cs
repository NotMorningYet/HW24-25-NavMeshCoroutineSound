using System.Collections.Generic;
using UnityEngine;

public class BehaviorSelecter
{
    private Character _character;
    private Dictionary<ControllersTypes, Controller> _controllers;
    private Controller _currentController;
    private ControllerSwitcher _controllerSwitcher;    

    private float _deltaTime;

    private float _lazyTimeBeforePatrol = 5;
    private float _lazyTime;
    private bool _isLazy;
    private readonly int leftMouseButton = 0;

    public BehaviorSelecter(Character character, Dictionary<ControllersTypes, Controller> controllers, Controller defaultController)
    {
        _controllers = controllers;
        _character = character;
        _currentController = defaultController;

        Initialize();
    }

    private void Initialize()
    {
        _controllerSwitcher = new ControllerSwitcher(_controllers);
        _currentController.Enable();
        _isLazy = true;
        _lazyTime = 0;
    }

    public void Update(float deltaTime)
    {
        if (_character.Health.Died)
        {
            _controllerSwitcher.DisableAll();
            return;
        }

        _deltaTime = deltaTime;
        _currentController?.Update(_deltaTime);
        SwitchingBehaviorLogic();
    }

    private void SwitchingBehaviorLogic()
    {
        switch (_currentController)
        {
            case MouseClickController:
                MouseClickLogic();
                break;
            case PatrolController:
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
            MouseClickController controller = (MouseClickController)_controllers[ControllersTypes.MouseClick];

            if (controller.TryActivate())
            {
                _isLazy = true;
                _lazyTime = 0;
                SetBehavior(ControllersTypes.MouseClick);
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
                _lazyTime = 0;
                return;
            }

            _lazyTime += _deltaTime;

            if (_lazyTime >= _lazyTimeBeforePatrol)
            {
                SetBehavior(ControllersTypes.Patrol);
            }
        }
        else
        {
            if (_character.CurrentVelocity.magnitude < 0.05f)
            {
                _isLazy = true;
                return;
            }
        }
    }

    private void SetBehavior(ControllersTypes controllerType)
    {
        _currentController = _controllerSwitcher.SetController(controllerType);
    }
}
