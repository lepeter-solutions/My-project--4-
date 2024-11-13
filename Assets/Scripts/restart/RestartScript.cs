using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    public void RestartPreviousScene()
    {
        // Visszanavigál az előző jelenetre
        SceneController.LoadPreviousScene();

        // Állítsd be a kurzort láthatóra és oldd fel a zárolást
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Debugging
        Debug.Log("Cursor visibility: " + Cursor.visible);
        Debug.Log("Cursor lock state: " + Cursor.lockState);
    }

    void Start()
    {
        // Ensure cursor settings are applied at the start
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Debugging
        Debug.Log("Cursor visibility at Start: " + Cursor.visible);
        Debug.Log("Cursor lock state at Start: " + Cursor.lockState);
    }
}