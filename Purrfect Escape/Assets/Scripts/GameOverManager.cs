using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    // Call this when the player dies or the game ends
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Optional: freeze time
    }

    // Called by "Play Again" button
    public void PlayAgain()
    {
        Time.timeScale = 1f; // Reset time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Called by "Exit Game" button
    public void ExitGame()
    {
        Time.timeScale = 1f; // Reset time just in case
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Allows exiting in Editor
#endif
    }
}
