using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIntegrator
{
    void Integrate(List<CelestialBody> bodies, float timeStep);
}
