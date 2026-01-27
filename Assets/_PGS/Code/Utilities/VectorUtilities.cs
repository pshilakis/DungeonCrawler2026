using UnityEngine;

namespace PGS.Utilities
{
    public static class VectorUtilities
    {
        public static bool IsWithinRangeSqrMagnitude(Vector2 pointA, Vector2 pointB, float distance)
        {
            return (pointB - pointA).SqrMagnitude() <= distance;
        }
    }
}
