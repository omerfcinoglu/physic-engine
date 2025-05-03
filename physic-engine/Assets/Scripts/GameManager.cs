using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ParticlePool particlePool;
    [SerializeField] private CollisionHandler collisionHandler;
    public Transform spawnPoint;

    public float spawnInterval = 5f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnParticle();
            timer = 0;
        }

        collisionHandler.CheckCollisions();
    }

    void SpawnParticle()
    {
        var particleObj = particlePool.SpawnParticle(spawnPoint.position);
        if (particleObj == null) return;
        collisionHandler.RegisterParticle(particleObj.GetComponent<Particle>());
    }
}