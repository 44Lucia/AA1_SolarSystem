using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venus : CelestialBody
{
    private void Awake()
    {
        mass = 2.45e-6f;
        transform.position = new Vector3(0.72f, 0f, 0f);
        velocity = new Vector3(0f, 7.38f, 0f);
    }

    public override void CalculateAcceleration(List<CelestialBody> bodies)
    {
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
