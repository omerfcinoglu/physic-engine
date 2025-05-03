using UnityEngine;

public class Particle : MonoBehaviour
{
    public float radius = 0.3f;
    SpriteRenderer spriteRenderer;
    public Vector2 velocity;
    private float leftBorder, rightBorder, topBorder, bottomBorder;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV();
        radius = spriteRenderer.bounds.size.x/2;
        velocity = new Vector2(20, 1);
        SetBorders();
    }

    void Update()
    {
        Move();
        CheckBorders();
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
            velocity.x *= -1;
        }
        else if (pos.x + radius > rightBorder)
        {
            pos.x = rightBorder - radius;
            velocity.x *= -1;
        }

        if (pos.y + radius > topBorder)
        {
            pos.y = topBorder - radius;
            velocity.y *= -1;
        }
        else if (pos.y - radius < bottomBorder)
        {
            pos.y = bottomBorder + radius;
            velocity.y *= -1;
        }

        transform.position = pos;
    }
}
