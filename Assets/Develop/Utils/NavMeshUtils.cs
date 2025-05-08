using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtils 
{
    public static float GetPathLength(NavMeshPath path)
    {
        float pathLength = 0;

        if (path.corners.Length > 1)
            for (int i = 1; i < path.corners.Length; i++)
                pathLength += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        
        return pathLength;
    }

    public static bool TryGetPath(Vector3 sourcePosition, Vector3 targetPoaisition, NavMeshQueryFilter queryFilter, NavMeshPath pathToTarget)
    {
        if (NavMesh.CalculatePath(sourcePosition, targetPoaisition, queryFilter, pathToTarget)) 
            return true;

        return false;
    }

}
