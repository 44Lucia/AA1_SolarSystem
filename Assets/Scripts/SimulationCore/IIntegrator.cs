using System.Collections.Generic;

public interface IIntegrator
{
    void Integrate(List<CelestialBody> bodies, float timeStep);
}
