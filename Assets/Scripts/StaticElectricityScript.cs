using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticElectricityScript : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip[] electricitySounds; // Array of electricity sound clips
    public ParticleSystem particleSystem; // Reference to the ParticleSystem component
    public Transform parentTransform; // Reference to the parent transform
    public float minInterval = 1.0f; // Minimum interval between sounds
    public float maxInterval = 5.0f; // Maximum interval between sounds
    public float moveRadius = 0.5f; // Radius within which particles can move
    public float disappearDuration = 0.5f; // Duration for which the particle system disappears
    public float vibrationIntensity = 0.1f; // Intensity of the vibration

    private Vector3 initialPosition;
    private Vector3 parentInitialPosition;

    void Start()
    {
        // Store the initial position of the particle system and its parent
        initialPosition = particleSystem.transform.position;
        if (parentTransform != null)
        {
            parentInitialPosition = parentTransform.position;
        }

        // Start the coroutine to play sounds, move particles, and handle visibility
        StartCoroutine(PlayElectricitySounds());
    }

    IEnumerator PlayElectricitySounds()
    {
        while (true)
        {
            // Wait for a random interval
            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);

            // Play a random electricity sound
            if (electricitySounds.Length > 0)
            {
                AudioClip clip = electricitySounds[Random.Range(0, electricitySounds.Length)];
                audioSource.PlayOneShot(clip);
            }

            // Move the particle system to a random position within the radius
            Vector3 randomOffset = Random.insideUnitSphere * moveRadius;
            randomOffset.z = 0; // Keep movement in the x-y plane
            particleSystem.transform.position = initialPosition + randomOffset;

            // Vibrate the parent object
            if (parentTransform != null)
            {
                StartCoroutine(VibrateParent());
            }

            // Disable the particle system for a short duration
            particleSystem.Stop();
            yield return new WaitForSeconds(disappearDuration);
            particleSystem.Play();
        }
    }

    IEnumerator VibrateParent()
    {
        float elapsedTime = 0f;
        while (elapsedTime < disappearDuration)
        {
            Vector3 vibrationOffset = Random.insideUnitSphere * vibrationIntensity;
            vibrationOffset.z = 0; // Keep movement in the x-y plane
            parentTransform.position = parentInitialPosition + vibrationOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the parent object's position
        parentTransform.position = parentInitialPosition;
    }
}