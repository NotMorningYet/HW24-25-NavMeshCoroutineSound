using UnityEngine;
using UnityEngine.AI;

public class NavMeshNavigator : Navigator
{
    private NavMeshPath _path;
    private NavMeshQueryFilter _queryFilter;

    public NavMeshNavigator()
    {
        _queryFilter = new NavMeshQueryFilter();
        _queryFilter.agentTypeID = 0;
        _queryFilter.areaMask = NavMesh.AllAreas;
        _path = new NavMeshPath();
    }

    public override Vector3 GetDirection(Vector3 currentPosition, Vector3 targetPosition)
    {
        Vector3 direction = new Vector3();
        NavMesh.CalculatePath(currentPosition, targetPosition, _queryFilter, _path);

        if (_path.status != NavMeshPathStatus.PathInvalid && _path.corners.Length>1)
            direction = _path.corners[1] - _path.corners[0];
        else
            direction = Vector3.zero;        

        return direction;
    }
}
