using UnityEngine;

public static class VectorUtils
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="inputVector">The position of the object to check against the boundaries.</param>
    /// <param name="boundaries">The boundaries defined as limit for operation.</param>
    /// <returns></returns>
    public static bool IsWithinBounds(this Vector3 inputVector, Boundaries boundaries)
    {
        //it is not less than all bounds, return false
        if (inputVector.y > boundaries.BoundaryNorth.y
            || inputVector.y < boundaries.BoundarySouth.y
            || inputVector.x > boundaries.BoundaryEast.x
            || inputVector.x < boundaries.BoundaryWest.x)
            return false;

        //if is in bounds, but out of maximum radius for corners
        var boundaryXRadius = Vector3.Distance(boundaries.BoundaryEast, boundaries.BoundaryWest) / 2;
        var xCenter = boundaries.BoundaryEast.x - boundaryXRadius;
        var boundaryYRadius = Vector3.Distance(boundaries.BoundaryNorth, boundaries.BoundarySouth) / 2;
        var yCenter = boundaries.BoundaryNorth.y - boundaryYRadius;
        var maxDistance = Mathf.Max(boundaryXRadius, boundaryYRadius);


        //add maxDistance to center, check by factor of -1 foreach direction
        if (inputVector.x > xCenter + maxDistance
            || inputVector.x < xCenter - maxDistance
            || inputVector.y > yCenter + maxDistance
            || inputVector.y < yCenter - maxDistance)
            return false;

        return true;
    }
}