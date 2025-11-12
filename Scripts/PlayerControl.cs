using UnityEngine;

public class Player
{
    public float moveSpeed = 8f;

    public GameObject laserPrefab;
    public Transform firePoint;
    public float fireRate = 0.25f;

    private float nextFireTime = 0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        nextFireTime = Time.time + fireRate;
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
    }
}