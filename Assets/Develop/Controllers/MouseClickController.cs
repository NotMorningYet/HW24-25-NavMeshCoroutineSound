using UnityEngine;

public class MouseClickController : MoveToPointController
{
    private const int _leftMouseButton = 0;

    public MouseClickController(Character character, Navigator navigator, IMoverListnener moverListnener)
       : base(character, navigator, moverListnener)
    {

    }

    public bool TryActivate()
    {
        if (TrySetTargetPosition())
            return true;
        
        return false;
    }

    protected override void HandleTargetUpdate()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
            TrySetTargetPosition();
    }

    protected override Ray GetRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray;
    }
}
