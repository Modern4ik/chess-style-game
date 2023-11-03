using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{   
    private bool isEditorRunning;

    private void Start()
    {
        isEditorRunning = UnityEditor.EditorApplication.isPlaying;
    }
    public void RestartGame()
    {   
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {   
        /* Условие в if нужно, чтобы кнопка Exit останавливала проигрывание сцены в Unity.
         * Тем самым мы имитируем выход из игры. В else метод, который закрывает приложение,
         * но так как мы пока что не открываем игру как отдельное десктопное приложение,
         * то он не отрабатывает при нажатии.
         */
        if (isEditorRunning) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }
}
