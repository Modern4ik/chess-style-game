using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Pressed exit button!");
        Application.Quit();
    }
}