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
        if (isEditorRunning) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }
}
