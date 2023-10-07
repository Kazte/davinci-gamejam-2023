using UnityEngine;

public class SteeringBehaviour
{
    public static Vector3 Seek(Vector3 position, Vector3 targetPosition, float maxVelocity, float steeringSpeed,
        Vector3 currentVelocity)
    {
        var diff = targetPosition - position;
        var dir = Vector3.Normalize(diff);
        var desiredVelocity = dir * maxVelocity;

        var velDiff = desiredVelocity - currentVelocity;
        var velDir = Vector3.Normalize(velDiff);
        var steeringForce = velDir * steeringSpeed;
        return steeringForce;
    }

    public static Vector3 Flee(Vector3 position, Vector3 targetPosition, float maxVelocity, float steeringSpeed,
        Vector3 currentVelocity)
    {
        return Seek(targetPosition, position, maxVelocity, steeringSpeed, currentVelocity);
    }
}