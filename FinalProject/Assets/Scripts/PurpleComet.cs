using UnityEngine;

public class PurpleComet : MonoBehaviour
{
    public float speed = 5f;
    public int scoreValue = 150;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

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
