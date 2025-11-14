using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 3f;

    void Start()
    {
        speed = 8f;
    }

    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }
}
