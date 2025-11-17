using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Called by play button
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //Called by quit button
    public void QuitGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}