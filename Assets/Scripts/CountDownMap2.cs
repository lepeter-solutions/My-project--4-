using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownMap2 : MonoBehaviour
{
    public float countdownTime = 20.0f; // Total countdown time in seconds
    private float currentTime;
    private TextMeshProUGUI countdownText;
    private LevelStartScript levelStartScript;

    private AudioSource audioSource;

    public AudioSource audioSourceMicro; // Reference to the AudioSource component
    public AudioClip microBeep; // Reference to the beep sound clip

    private bool onceTriggered = false;

    void Start()
    {
        // Initialize the countdown time
        currentTime = countdownTime;

        // Get the TextMeshProUGUI component
        countdownText = GetComponent<TextMeshProUGUI>();

        audioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();

        audioSourceMicro = GameObject.Find("Microwave_Oven").GetComponent<AudioSource>();

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
            MicroWave();
            StartCoroutine(WaitAndCheckFork());
        }
    }

    IEnumerator WaitAndCheckFork()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5.0f);

        // Check the isFork variable from MicrowaveScript
        MicrowaveScript microwaveScript = FindObjectOfType<MicrowaveScript>();
        if (microwaveScript != null)
        {
            if (microwaveScript.isFork)
            {
                Debug.Log("Fork detected in the microwave.");
                LoadRestartScene();

            }
            else
            {
                Debug.Log("No fork detected in the microwave.");
                MakeDoorToHeaven();
            }
        }
        else
        {
            Debug.LogError("MicrowaveScript component not found in the scene.");

        }
    }

    void MicroWave()
    {
        if (onceTriggered)
        {
            return;
        }
        onceTriggered = true;
        if (levelStartScript != null)
        {
            Debug.Log("MicroWave method called.");

            // Stop the original sound
            audioSourceMicro.Stop();

            // Set the AudioClip to the beep sound
            audioSourceMicro.clip = microBeep;

            // Play the beep sound
            audioSourceMicro.Play();
            audioSourceMicro.loop = false;

            Debug.Log("Playing microBeep sound in MicroWave method.");
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

    public void LoadRestartScene()
    {
        // Betölti a restart jelenetet
        SceneController.LoadScene("GameOver"); // Cseréld le a "RestartScene"-t a restart jelenet nevére
    }
}