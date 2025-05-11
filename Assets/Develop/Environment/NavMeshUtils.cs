using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtils
{
    public static bool TryGetPath(NavMeshAgent agent, Vector3 targetPosition, NavMeshPath pathToTarget)
    {
        if (agent.CalculatePath(targetPosition, pathToTarget) && pathToTarget.status != NavMeshPathStatus.PathInvalid)
            return true;

        return false;
    }


    public static float GetPathLength(NavMeshPath path)
    {
        float pathLength = 0;

        if (path.corners.Length > 1)
            for (int i = 1; i < path.corners.Length; i++)
                pathLength += Vector3.Distance(path.corners[i - 1], path.corners[i]);

        return pathLength;
    }
}
