using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Called by restart button
    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Called by exit button
    public void ExitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}