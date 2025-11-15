using UnityEngine;

public class Alien : MonoBehaviour
{
    public float speed = 2f;
    public float zigzagWidth = 2f;
    public float zigzagFrequency = 3f;
    public int scoreValue = 150;

    private float startX;

    private void Start()
    {
        startX = transform.position.x;
    }

    private void Update()
    {
        float xOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagWidth;
        transform.position = new Vector2(startX + xOffset, transform.position.y - speed * Time.deltaTime);

        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("PlayerLaser"))
        {
            Destroy(collision.gameObject);
            ScoreManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
