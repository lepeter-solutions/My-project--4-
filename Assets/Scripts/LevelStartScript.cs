// LevelStartScript.cs
using UnityEngine;
using System.Collections; // Add this directive for IEnumerator

public class LevelStartScript : MonoBehaviour
{
    public GameObject disableObjects;
    public GameObject showObjects;

    public GameObject afterEnd;

    public GameObject afterMicrowave;
    public AudioSource audioSource;

    void Start()
    {
        GameObject map = GameObject.Find("Map");
        disableObjects = map.transform.Find("beforeStart").gameObject;
        showObjects = map.transform.Find("afterStart").gameObject;
        afterEnd = map.transform.Find("afterEnd").gameObject;
        afterMicrowave = GameObject.Find("afterMicrowave");
        audioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();

        if (afterMicrowave == null)
        {
            afterMicrowave = null;
        }

        afterMicrowave.SetActive(false);
        afterEnd.SetActive(false);
        showObjects.SetActive(false);

    }

    void Update()
    {
        if (afterEnd == null)
        {
            Debug.Log("HOUSTON WE HAVE A PROBLEM");
        }
    }


    public IEnumerator StartLevel()
    {
        Debug.Log("Level started!");
        if (disableObjects != null)
        {
            disableObjects.SetActive(false);
        }
        audioSource.Play();
        yield return new WaitForSeconds(8.5f);


        if (showObjects != null)
        {
            showObjects.SetActive(true);
        }
    }

    public void startMicrowave()
    {
        Debug.Log("Microwave started!");
        if (afterMicrowave != null)
        {
            afterMicrowave.SetActive(true);
        }
    }

    public void EndLevel()
    {
        GameObject map = GameObject.Find("Map");
        disableObjects = map.transform.Find("beforeStart").gameObject;
        showObjects = map.transform.Find("afterStart").gameObject;
        afterEnd = map.transform.Find("afterEnd").gameObject;
        afterMicrowave = GameObject.Find("afterMicrowave");
        Debug.Log("Level ended!");
        if (disableObjects != null)
        {
            disableObjects.SetActive(false);
        }
        if (showObjects != null)
        {
            showObjects.SetActive(false);
        }
        StartDoor();

    }


    public void StartDoor()
    {
        if (afterEnd == null)
        {
            GameObject map = GameObject.Find("Map");
            if (map != null)
            {
                Transform afterEndTransform = map.transform.Find("afterEnd");
                if (afterEndTransform != null)
                {
                    afterEnd = afterEndTransform.gameObject;
                }
                else
                {
                    Debug.LogError("afterEnd GameObject not found as a child of Map.");
                }
            }
            else
            {
                Debug.LogError("Map GameObject not found in the scene.");
            }
        }
        afterEnd.SetActive(true);
    }
}