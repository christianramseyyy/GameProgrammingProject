using UnityEngine;

public class Alien : MonoBehaviour
{
    public float speed;

    private float startX;

    private void Start()
    {
        speed = 2f;
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //    else if (collision.CompareTag("PlayerLaser"))
    //    {
    //        Destroy(collision.gameObject);
    //        ScoreManager.Instance.AddScore(scoreValue);
    //        Destroy(gameObject);
    //    }
    //}
}
