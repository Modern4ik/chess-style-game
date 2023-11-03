using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{   
    // Временное поле для того, чтобы останавливать UnityEditor
    private bool isEditorRunning;

    private void Start()
    {
        isEditorRunning = UnityEditor.EditorApplication.isPlaying;
    }
    public void RestartGame()
    {   
        //Тут мы просто загружаем нашу сцену заново.
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {   
        //Какая логика, условие в if нужно, чтобы кнопка Exit при нажатии останавливала
        //проигрывание сцены в Unity, тем самым мы имитируем выход из игры. Это временно сделано.
        //В else метод, который закрывает приложение, но так как мы пока что не открываем игру как 
        //отдельное десктопное приложение, то он не отрабатывает при нажатии.
        if (isEditorRunning) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }
}
