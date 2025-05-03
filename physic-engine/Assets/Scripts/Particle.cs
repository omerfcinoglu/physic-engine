using UnityEngine;

public class Particle : MonoBehaviour
{
    public float radius = 0.3f;
    public Vector2 velocity;

    SpriteRenderer spriteRenderer;
    float leftBorder, rightBorder, topBorder, bottomBorder;

    [Range(0f, 2f)] public float drag = 0.2f; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;
        radius = spriteRenderer.bounds.size.x / 2;

        velocity = new Vector2(20f,3f);
        SetBorders();
    }

    void Update()
    {
        ApplyPhysics();
        CheckBorders();
    }

    void ApplyPhysics()
    {
        velocity += Physics2D.gravity * Time.deltaTime;
        velocity *= (1 - drag * Time.deltaTime);
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }

    void SetBorders()
    {
        Transform borders = GameObject.Find("Borders").transform;

        leftBorder = borders.Find("Left").position.x;
        rightBorder = borders.Find("Right").position.x;
        topBorder = borders.Find("Top").position.y;
        bottomBorder = borders.Find("Bot").position.y;
    }

    void CheckBorders()
    {
        Vector2 pos = transform.position;

        if (pos.x - radius < leftBorder)
        {
            pos.x = leftBorder + radius;
            velocity.x *= -0.8f; 
        }
        else if (pos.x + radius > rightBorder)
        {
            pos.x = rightBorder - radius;
            velocity.x *= -0.8f;
        }

        if (pos.y + radius > topBorder)
        {
            pos.y = topBorder - radius;
            velocity.y *= -0.8f;
        }
        else if (pos.y - radius < bottomBorder)
        {
            pos.y = bottomBorder + radius;
            velocity.y *= -0.8f;
        }

        transform.position = pos;
    }

    public void HandleCollision(Particle other)
    {
        Vector2 collisionNormal = (transform.position - other.transform.position).normalized;
        velocity = Vector2.Reflect(velocity, collisionNormal);
    }
}
