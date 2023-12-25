using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    namespace UI
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
}