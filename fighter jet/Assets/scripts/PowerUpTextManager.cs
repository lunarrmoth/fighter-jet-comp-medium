using System.Collections;
using UnityEngine;
using TMPro;

public class PowerUpTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI powerUpText;
    [SerializeField] private float displayTime = 2f;

    void Start()
    {
        // Hide the text at the start
        if (powerUpText != null)
        {
            powerUpText.text = "";
        }
    }

    public void ShowPowerUpText(string message)
    {
        if (powerUpText != null)
        {
            powerUpText.text = message;
            StopAllCoroutines(); // Stop any existing fade
            StartCoroutine(HideTextAfterDelay());
        }
    }

    private IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        powerUpText.text = "";
    }
}