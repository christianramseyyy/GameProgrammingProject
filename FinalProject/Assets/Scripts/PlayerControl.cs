using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour 
{
    public float moveSpeed = 4f;

    public GameObject laserPrefab;
    public GameObject laserPos;
    public float fireRate = 0.25f;

    //private float nextFireTime = 0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    //Sound effects
    public AudioClip laserSfx;
    public AudioClip damageSfx;

    private AudioSource audioSource;

    // screen bounds
    private Vector2 minBounds;
    private Vector2 maxBounds;

    // explosion prefab
    public GameObject explosionPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject laser = (GameObject)Instantiate(laserPrefab);
            laser.transform.position = laserPos.transform.position;
            audioSource.PlayOneShot(laserSfx);
        }        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minBounds.x + 0.5f, maxBounds.x - 0.5f);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + 0.5f, maxBounds.y - 0.5f);

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Alien") || collision.CompareTag("EnemyLaser") ||
            collision.CompareTag("UFO") || collision.CompareTag("PurpleComet"))
        {
            // Debug.Log("Player collided with alien!");
            AudioSource.PlayClipAtPoint(damageSfx, transform.position);

            Destroy(gameObject);
            playExplosion();
            SceneManager.LoadScene("GameOver");
        }
    }

    void playExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
    }
}