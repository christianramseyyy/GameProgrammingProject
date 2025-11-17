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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Alien") || collision.CompareTag("EnemyLaser") ||
            collision.CompareTag("UFO"))
        {
            Debug.Log("Player collided with alien!");
            AudioSource.PlayClipAtPoint(damageSfx, transform.position);

            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}