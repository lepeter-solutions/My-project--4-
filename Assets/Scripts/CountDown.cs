using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public float countdownTime = 10.0f; // Total countdown time in seconds
    private float currentTime;
    private TextMeshProUGUI countdownText;
    private LevelStartScript levelStartScript;

    private AudioSource audioSource;


    void Start()
    {
        // Initialize the countdown time
        currentTime = countdownTime;

        // Get the TextMeshProUGUI component
        countdownText = GetComponent<TextMeshProUGUI>();

        audioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();


        if (countdownText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
        }

        // Get the LevelStartScript component
        levelStartScript = FindObjectOfType<LevelStartScript>();

        if (levelStartScript == null)
        {
            Debug.LogError("LevelStartScript component not found in the scene.");
        }
    }

    void Update()
    {
        if (currentTime > 0)
        {
            // Decrease the current time
            currentTime -= Time.deltaTime;

            // Update the countdown text
            countdownText.text = Mathf.Ceil(currentTime).ToString();
        }
        else
        {
            // Countdown has finished
            countdownText.text = "0";
            Debug.Log("Countdown finished!");
            MakeDoorToHeaven();
        }
    }

    void MakeDoorToHeaven()
    {
        if (levelStartScript != null)
        {
            audioSource.Stop();
            levelStartScript.EndLevel();
        }
    }
}