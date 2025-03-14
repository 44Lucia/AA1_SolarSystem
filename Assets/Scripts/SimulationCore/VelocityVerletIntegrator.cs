using System.Collections.Generic;
using UnityEngine;

public class VelocityVerletIntegrator : IIntegrator
{
    public void Integrate(List<CelestialBody> p_bodies, float p_timeStep)
    {
        Vector3[] oldAccelerations = new Vector3[p_bodies.Count];

        // 1. save current accelerations and update positions
        for (int i = 0; i < p_bodies.Count; i++)
        {
            CelestialBody body = p_bodies[i];
            if (body.IsStatic)
            {
                oldAccelerations[i] = Vector3.zero;
                continue;
            }

            oldAccelerations[i] = body.Acceleration;
            Vector3 newPosition = body.transform.position + body.Velocity * p_timeStep
                                  + 0.5f * body.Acceleration * p_timeStep * p_timeStep;
            body.transform.position = newPosition;
        }

        // 2. recalculate the accelerations with the new positions
        foreach (CelestialBody body in p_bodies)
        {
            if (!body.IsStatic) { body.CalculateAcceleration(p_bodies); }
        }

        // 3. update velocities using the average of the old and new acceleration
        for (int i = 0; i < p_bodies.Count; i++)
        {
            CelestialBody body = p_bodies[i];
            if (body.IsStatic) { continue; }

            body.Velocity = body.Velocity + 0.5f * (oldAccelerations[i] + body.Acceleration) * p_timeStep;
        }
    }
}