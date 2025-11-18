using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public float speed = 2f;
    public float horizontalSpeed = 1f;
    public float direction;
    public float minX, maxX;
    public int scoreValue = 200;
    public GameObject explosionPrefab;

    void Start()
    {

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        minX = min.x + 0.25f;
        maxX = max.x - 0.25f;

        direction = Random.Range(-2f, 2f);
    }

    private void Update()
    {
        Vector2 position = transform.position;

        position.x += direction * horizontalSpeed * Time.deltaTime;
        position.y -= speed * Time.deltaTime;

        transform.position = position;

        if(position.x < minX) 
        {
            position.x = minX;
            direction = 1f;
        } else if(position.x > maxX)
        {
            position.x = maxX;
            direction = -1f;
        }

        transform.position = position;  


        Vector2 bottomScreen = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < bottomScreen.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLaser"))
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
