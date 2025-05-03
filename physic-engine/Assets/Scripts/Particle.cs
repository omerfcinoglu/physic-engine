using UnityEngine;

public class Particle : MonoBehaviour
{
    public float radius = 1f;
    SpriteRenderer spriteRenderer;
    public Vector2 velocity;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        velocity += Physics2D.gravity * Time.deltaTime;
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }

    public void HandleCollision(Particle other)
    {
        Vector2 collisionNormal = (transform.position - other.transform.position).normalized;
        velocity = Vector2.Reflect(velocity, collisionNormal);
    }
}
