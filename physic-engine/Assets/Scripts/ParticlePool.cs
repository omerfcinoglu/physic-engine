using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public GameObject particlePrefab;
    public int poolSize = 1000;

    private Queue<GameObject> particleQueue;

    void Awake()
    {
        particleQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject particle = Instantiate(particlePrefab);
            particle.SetActive(false);
            particleQueue.Enqueue(particle);
        }
    }

    public GameObject SpawnParticle(Vector2 position)
    {
        if (particleQueue.Count == 0) return null;

        GameObject particle = particleQueue.Dequeue();
        particle.transform.position = position;
        particle.SetActive(true);

        return particle;
    }

    public void ReturnToPool(GameObject particle)
    {
        particle.SetActive(false);
        particleQueue.Enqueue(particle);
    }
}
