using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : CelestialBody
{
    private void Awake()
    {
        mass = 1f;
        velocity = Vector3.zero;
        isStatic = true;
    }

    public override void CalculateAcceleration(List<CelestialBody> bodies)
    {
        // If the Sun is static, its acceleration is not updated.
        if (isStatic)
        {
            acceleration = Vector3.zero;
            return;
        }

        // In case you want to allow movement, you could calculate the acceleration
        acceleration = Vector3.zero;
        foreach (CelestialBody body in bodies)
        {
            if (body != this)
            {
                Vector3 direction = body.transform.position - transform.position;
                float distanceSqr = direction.sqrMagnitude;
                if (distanceSqr < 0.0001f) continue;
                float forceMagnitude = SimulationConstants.GravitationalConstant * body.mass / distanceSqr;
                acceleration += direction.normalized * forceMagnitude;
            }
        }
    }
}
