using UnityEngine;

public class UFOBullet : MonoBehaviour
{
    float speed;
    Vector2 direction;
    bool isReady;

    private void Awake()
    {
        speed = 5f;
        isReady = false;
    }
    void Start()
    {
        
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        isReady = true;
    }

    
    void Update()
    {
        if(isReady)
        {
            Vector2 position = transform.position;

            position += direction * speed * Time.deltaTime;

            transform.position = position;

            // bottom left
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            // top right
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if((transform.position.x < min.x) ||  (transform.position.x > max.x) ||
                (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage();
        }
    }
}
