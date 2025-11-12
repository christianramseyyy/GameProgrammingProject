using UnityEngine;

public class Laser
{
    public float speed = 12f;
    public float lifetime = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }
}
