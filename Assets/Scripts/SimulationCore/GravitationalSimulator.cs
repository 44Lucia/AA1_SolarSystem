using System.Collections.Generic;
using UnityEngine;

public class GravitationalSimulator : MonoBehaviour
{
    [SerializeField] private List<CelestialBody> celestialBodies;
    [SerializeField] private float timeStep = 0.01f;

    private IIntegrator integrator;

    private void Awake() { integrator = new VelocityVerletIntegrator(); }

    private void FixedUpdate()
    {
        // gravitational acceleration for each body
        foreach (CelestialBody body in celestialBodies) { body.CalculateAcceleration(celestialBodies); }

        // update positions and velocities using integrator
        integrator.Integrate(celestialBodies, timeStep);
    }
}