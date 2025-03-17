using System.Collections.Generic;
using UnityEngine;

public class SatelliteOrbit : CelestialBody
{
    [Header("Satellite Orbit Settings")]
    [SerializeField] private CelestialBody parentBody; 
    [SerializeField] private float orbitalRadius = 0.00257f;      
    [SerializeField] private float orbitalSpeedRelative = 0.22f;    

    private Vector3 relativePosition;
    private float angularSpeed;

    protected override void Awake()
    {
        base.Awake();

        if (parentBody == null)
        {
            enabled = false;
            return;
        }

        relativePosition = new Vector3(orbitalRadius, 0f, 0f) * SimulationConstants.ScaleFactor;
        transform.position = parentBody.transform.position + relativePosition;

        float secondsPerYear = 365.25f * 24f * 3600f;
        float orbitalSpeedPerSecond = orbitalSpeedRelative / secondsPerYear;
        angularSpeed = orbitalSpeedPerSecond / relativePosition.magnitude;
    }

    public override void CalculateAcceleration(List<CelestialBody> p_bodies)
    {
        float dt = Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angularSpeed * dt * Mathf.Rad2Deg);
        relativePosition = rotation * relativePosition;

        transform.position = parentBody.transform.position + relativePosition;

        Vector3 tangentDirection = new Vector3(-relativePosition.y, relativePosition.x, 0f).normalized;
        Vector3 relativeTangentialVelocity = tangentDirection * angularSpeed * relativePosition.magnitude;

        Velocity = parentBody.Velocity + relativeTangentialVelocity;
    }
}
