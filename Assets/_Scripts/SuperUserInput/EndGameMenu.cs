using GameSettings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperUserInput
{
    public class EndGameMenu : MonoBehaviour
    {
        public void RestartGame() => SceneManager.LoadScene("GameScene");
        public void ReturnToMainMenu() => SceneManager.LoadScene("MainMenuScene");
    }
}