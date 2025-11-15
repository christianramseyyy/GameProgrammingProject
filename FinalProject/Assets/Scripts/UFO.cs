using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;
    public float fireIntervalMin = 1.5f;
    public float fireIntervalMax = 3.5f;
    public GameObject enemyLaserPrefab;
    public int scoreValue = 200;

    private float startX;
    private float nextFireTime;
    private int direction = 1;

    private void Start()
    {
        startX = transform.position.x;
        ScheduleNextShot();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - startX) >= moveDistance)
        {
            direction *= -1; 
        }

        if (Time.time > nextFireTime)
        {
            //FireLaser();
            ScheduleNextShot();
        }

        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    //private void FireLaser()
    //{
    //    Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);
    //}

    private void ScheduleNextShot()
    {
        nextFireTime = Time.time + Random.Range(fireIntervalMin, fireIntervalMax);
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
