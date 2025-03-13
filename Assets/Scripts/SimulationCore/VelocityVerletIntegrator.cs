using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityVerletIntegrator : IIntegrator
{
    public void Integrate(List<CelestialBody> bodies, float timeStep)
    {
        Vector3[] oldAccelerations = new Vector3[bodies.Count];

        // 1. Save current accelerations and update positions
        for (int i = 0; i < bodies.Count; i++)
        {
            CelestialBody body = bodies[i];
            if (body.isStatic)
            {
                oldAccelerations[i] = Vector3.zero;
                continue;
            }
            oldAccelerations[i] = body.acceleration;
            Vector3 newPosition = body.transform.position + body.velocity * timeStep
                                  + 0.5f * body.acceleration * timeStep * timeStep;
            body.transform.position = newPosition;
        }

        // 2. Recalculate the accelerations with the new positions
        foreach (CelestialBody body in bodies)
        {
            if (!body.isStatic)
            {
                body.CalculateAcceleration(bodies);
            }
        }

        // 3. Update velocities using the average of the old and new acceleration
        for (int i = 0; i < bodies.Count; i++)
        {
            CelestialBody body = bodies[i];
            if (body.isStatic) continue;
            body.velocity += 0.5f * (oldAccelerations[i] + body.acceleration) * timeStep;
        }
    }
}
