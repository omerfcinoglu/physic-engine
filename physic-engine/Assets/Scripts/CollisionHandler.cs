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

                Vector2 delta = b.transform.position - a.transform.position;
                float dist = delta.magnitude;
                float penetrationDepth = (a.radius + b.radius) - dist;

                if (penetrationDepth > 0)
                {
                    Vector2 collisionNormal = delta.normalized;

                    a.transform.position -= (Vector3)(collisionNormal * penetrationDepth / 2f);
                    b.transform.position += (Vector3)(collisionNormal * penetrationDepth / 2f);

                    ResolveCollision(a, b, collisionNormal);
                }
            }
        }
    }

    void ResolveCollision(Particle a, Particle b, Vector2 collisionNormal)
    {
        Vector2 relativeVelocity = b.velocity - a.velocity;
        float velocityAlongNormal = Vector2.Dot(relativeVelocity, collisionNormal);

        if (velocityAlongNormal > 0)
            return;

        float restitution = 0.8f; 

        float impulseMagnitude = -(1 + restitution) * velocityAlongNormal / 2;

        Vector2 impulse = impulseMagnitude * collisionNormal;

        a.velocity -= impulse;
        b.velocity += impulse;
    }
}