using UnityEngine;
using TMPro;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private int startingLives = 3;

    private int currentLives;

    void Start()
    {
        currentLives = startingLives;
        UpdateLivesText();
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateLivesText();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void AddLife()
    {
        currentLives++;
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }

    public int GetLives()
    {
        return currentLives;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // You can add game over logic here later
        // For example: show game over screen, restart button, etc.
    }
}