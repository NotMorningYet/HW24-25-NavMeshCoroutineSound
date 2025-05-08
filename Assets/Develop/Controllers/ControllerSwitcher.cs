using System.Collections.Generic;

public class ControllerSwitcher
{
    private Dictionary<ControllersTypes, Controller> _availableControllers;

    public ControllerSwitcher(Dictionary<ControllersTypes, Controller> availableControllers)
    {
        _availableControllers = availableControllers;
    }

    public Controller SetController(ControllersTypes controllerType)
    {
        Controller selectedController = null;

        foreach (var controller in _availableControllers)
        {
            controller.Value.Disable();

            if (controller.Key == controllerType)
            {
                selectedController = controller.Value;
                selectedController.Enable();
            }
        }

        return selectedController;
    }

    public void DisableAll()
    {
        foreach (var controller in _availableControllers)
            controller.Value.Disable();
    }   
}
