using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //Set this as the only instance and persist
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
