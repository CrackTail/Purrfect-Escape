using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

   
    [SerializeField] private GameObject gameWinBackground;
    [SerializeField] private GameObject gameOverBackground;

    public void ShowGameOver(bool hasWon)
    {
        gameOverPanel.SetActive(true);
        gameOverBackground.SetActive(!hasWon);
        gameWinBackground.SetActive(hasWon);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
