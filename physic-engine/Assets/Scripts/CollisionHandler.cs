using UnityEngine;
using System.Collections.Generic;

public class CollisionHandler : MonoBehaviour
{
    public List<Particle> particles = new List<Particle>();

    public void RegisterParticle(Particle p)
    {
        particles.Add(p);
    }

    public void UnregisterParticle(Particle p)
    {
        particles.Remove(p);
    }

    public void CheckCollisions()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            for (int j = i + 1; j < particles.Count; j++)
            {
                Particle a = particles[i];
                Particle b = particles[j];
                float dist = Vector2.Distance(a.transform.position, b.transform.position);

                if (dist < a.radius + b.radius)
                {
                    a.HandleCollision(b);
                    b.HandleCollision(a);
                }
            }
        }
    }
}