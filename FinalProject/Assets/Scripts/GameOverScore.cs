using UnityEngine;
using TMPro; 

public class GameOverScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        int finalScore = 0;

        if (ScoreManager.Instance != null)
        {
            finalScore = ScoreManager.Instance.CurrentScore;
        }

        scoreText.text = "SCORE: " + finalScore.ToString();
    }
}
