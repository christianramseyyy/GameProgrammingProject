using UnityEngine;

public class UFOGun : MonoBehaviour
{
    public GameObject UFOLaser;
   
    void Start()
    {
        InvokeRepeating("shootLaser", 1f, 1f);
    }

    
    void Update()
    {
        
    }

    void shootLaser()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            GameObject bullet = (GameObject)Instantiate(UFOLaser);

            bullet.transform.position = transform.position;

            Vector2 direction = player.transform.position - bullet.transform.position;

            bullet.GetComponent<UFOBullet>().SetDirection(direction);
        }
    }
}
