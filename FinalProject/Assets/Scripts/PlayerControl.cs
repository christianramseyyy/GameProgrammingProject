using UnityEngine;

public class Player : MonoBehaviour 
{
    public float moveSpeed = 4f;

    public GameObject laserPrefab;
    public GameObject laserPos;
    public float fireRate = 0.25f;

    private float nextFireTime = 0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}