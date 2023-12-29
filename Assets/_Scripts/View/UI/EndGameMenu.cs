using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    namespace UI
    {
        public class EndGameMenu : MonoBehaviour
        {
            public void RestartGame() => SceneManager.LoadScene("GameScene");
            public void ReturnToMainMenu() => SceneManager.LoadScene("MainMenuScene");
        }
    }
}