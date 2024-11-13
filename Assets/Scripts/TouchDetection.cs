using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; // Add this directive for SceneManager

public class TouchDetection : MonoBehaviour
{
    // Function to be called when the player touches the object
    public void OnPlayerTouch()
    {
        Debug.Log("Player touched " + gameObject.name);
        // Add your interaction logic here
        if (gameObject.name == "nextSceneDoor")
        {
            LoadNextScene();
        }

        if (gameObject.name == "transformer")
        {
            Debug.Log("transformator touched");
            // CAll electricity die script that is attached to player
            GameObject.Find("Player").GetComponent<ElectricityDieScript>().Die();

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            OnPlayerTouch();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerTouch();
        }
    }

    void LoadNextScene()
    {
        // Load the next scene in the build settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }
}