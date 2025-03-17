using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] protected float mass = 1f;
    [SerializeField] protected Vector3 initialPosition = Vector3.zero;
    [SerializeField] private Vector3 initialVelocity = Vector3.zero;
    [SerializeField] private bool isStatic = false;

    private Vector3 velocity;
    private Vector3 acceleration;

    protected virtual void Awake()
    {
        transform.position = initialPosition * SimulationConstants.ScaleFactor;
        velocity = initialVelocity * Mathf.Sqrt(1 / SimulationConstants.ScaleFactor);

        if (!TryGetComponent(out TrailRenderer trail)) { return; }
        trail.enabled = true;
    }

    public virtual void CalculateAcceleration(List<CelestialBody> p_bodies)
    {
        acceleration = Vector3.zero;
        foreach (CelestialBody body in p_bodies)
        { 
            Vector3 direction = body.transform.position - transform.position;
            float distanceSqr = direction.sqrMagnitude;

            if (distanceSqr < 0.0001f) { continue; }

            float forceMagnitude = SimulationConstants.GravitationalConstant * body.mass / distanceSqr;
            acceleration += direction.normalized * forceMagnitude;
        }
    }

    public bool IsStatic { get { return isStatic; } set { isStatic = value; } }
    public Vector3 Velocity { get { return velocity; } set { velocity = value; } }
    public Vector3 Acceleration { get { return acceleration; } set { acceleration = value; } }
}