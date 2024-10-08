using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperUserInput
{
    public class PauseMenu : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1f;
        }
        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("MainMenuScene");
            Time.timeScale = 1f;
        }
    }
}