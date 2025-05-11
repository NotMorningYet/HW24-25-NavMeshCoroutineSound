using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavMeshAgentMouseController : MoveToPointController
{
    private const int _leftMouseButton = 0;

    public NavMeshAgentMouseController(CharacterAgent character, IMoverListener moverListnener)
       : base(character, moverListnener)
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
            if (IsPointerOverUIObject() == false)
                TrySetTargetPosition();
    }

    protected override Ray GetRay() => Camera.main.ScreenPointToRay(Input.mousePosition);

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}

