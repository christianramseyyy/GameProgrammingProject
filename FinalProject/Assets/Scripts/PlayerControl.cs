using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;

    public GameObject laserPrefab;
    public GameObject laserPos;
    public float fireRate = 0.25f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Sound effects
    public AudioClip laserSfx;
    public AudioClip damageSfx;
    private AudioSource audioSource;

    // screen bounds
    private Vector2 minBounds;
    private Vector2 maxBounds;

    // explosion prefab
    public GameObject explosionPrefab;

    // Lives
    public int maxLives = 3;
    public TextMeshProUGUI livesText;
    public float respawnDelay = 1f;
    public float invincibilityDuration = 1.5f;

    private int currentLives;
    private bool isRespawning = false;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private Vector3 spawnPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();

        spawnPosition = transform.position;

        minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        currentLives = maxLives;
        UpdateLivesUI();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject laser = Instantiate(laserPrefab);
            laser.transform.position = laserPos.transform.position;

            if (laserSfx != null)
                audioSource.PlayOneShot(laserSfx);
        }
    }

    private void FixedUpdate()
    {
        if (!isRespawning)
            rb.linearVelocity = moveInput * moveSpeed;
        else
            rb.linearVelocity = Vector2.zero;

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minBounds.x + 0.5f, maxBounds.x - 0.5f);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + 0.5f, maxBounds.y - 0.5f);

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Don’t take damage if we’re respawning or invincible
        if (isRespawning || isInvincible)
            return;

        if (collision.CompareTag("Alien") ||
            collision.CompareTag("EnemyLaser") ||
            collision.CompareTag("UFO") ||
            collision.CompareTag("PurpleComet"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (isRespawning || isInvincible)
            return;

        currentLives--;
        UpdateLivesUI();

        if (damageSfx != null)
            audioSource.PlayOneShot(damageSfx);

        if (currentLives > 0)
        {
            StartCoroutine(RespawnRoutine());
        }
        else
        {
            PlayExplosion();
            GameOver();
        }
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;
        isInvincible = true;

        // "Die"
        spriteRenderer.enabled = false;
        playerCollider.enabled = false;
        rb.linearVelocity = Vector2.zero;

        // Wait before respawn
        yield return new WaitForSeconds(respawnDelay);

        // Respawn at spawn point
        transform.position = spawnPosition;
        spriteRenderer.enabled = true;
        playerCollider.enabled = true;

        isRespawning = false;

        // Flash during invincibility
        float elapsed = 0f;
        float flashInterval = 0.15f;

        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        // Make sure we end visible
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void PlayExplosion()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = $"LIVES: {currentLives}";
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
