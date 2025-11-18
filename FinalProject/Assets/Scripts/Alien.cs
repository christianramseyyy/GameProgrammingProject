using UnityEngine;

public class Alien : MonoBehaviour
{
    public float speed;
    public int scoreValue = 50;
    public GameObject explosionPrefab;

    private void Start()
    {
        speed = 3f;
    }

    private void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2 (position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));

        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerLaser"))
        {
            Destroy(gameObject);
            playExplosion();
            ScoreManager.Instance.AddScore(scoreValue);
        }
    }

    void playExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
    }
}
