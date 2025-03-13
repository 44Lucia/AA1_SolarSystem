using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestialBody : MonoBehaviour
{
    public float mass;
    public Vector3 velocity;
    public Vector3 acceleration;
    public bool isStatic = false;
    public abstract void CalculateAcceleration(List<CelestialBody> bodies);
}
