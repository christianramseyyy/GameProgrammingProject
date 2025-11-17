using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour 
{
    [Header("Movement")]
    public float moveSpeed = 4f;

    [Header("Shooting")]
    public GameObject laserPrefab;
    public GameObject laserPos;
    public float fireRate = 0.25f;

    //private float nextFireTime = 0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Sound Effects")]
    public AudioClip laserSfx;
    public AudioClip damageSfx;
    private AudioSource audioSource;

    [Header("Lives")]
    public int maxLives = 3;            // starting lives
    public float respawnDelay = 1f;     // delay before respawn
    public float invincibilityDuration = 1.5f; // time player is invincible after respawn

    private int currentLives;
    private bool isRespawning = false;
    private bool isInvincible = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private Vector3 spawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();

        spawnPosition = transform.position;
        currentLives = maxLives;
    }

    void Update()
    {
        // movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject laser = Instantiate(laserPrefab);
            laser.transform.position = laserPos.transform.position;
            audioSource.PlayOneShot(laserSfx);
        }
    }

    void FixedUpdate()
    {
        if (!isRespawning)
        {

            rb.linearVelocity = moveInput * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Alien") || 
            collision.CompareTag("EnemyLaser") ||
            collision.CompareTag("UFO"))
        {
            Debug.Log("Player collided with damage source: " + collision.tag);
            TakeDamage();

            if (collision.CompareTag("EnemyLaser"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    public void TakeDamage()
    {
        if (isRespawning || isInvincible)
            return;

        currentLives--;
        Debug.Log("Player took damage. Lives remaining: " + currentLives);

        if (damageSfx != null)
        {
            AudioSource.PlayClipAtPoint(damageSfx, transform.position);
        }

        if (currentLives > 0)
        {
            StartCoroutine(RespawnRoutine());
        }
        else
        {
            Die();
        }
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;
        isInvincible = true;

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
        if (playerCollider != null)
            playerCollider.enabled = false;

        rb.linearVelocity = Vector2.zero;

        // wait, respawn
        yield return new WaitForSeconds(respawnDelay);

        transform.position = spawnPosition;

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;
        if (playerCollider != null)
            playerCollider.enabled = true;

        isRespawning = false;

        // flash sprite to show temp invincibility
        float elapsed = 0f;
        float flashInterval = 0.15f;

        while (elapsed < invincibilityDuration)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("Player died - out of lives.");
        Destroy(gameObject);
    }
}
