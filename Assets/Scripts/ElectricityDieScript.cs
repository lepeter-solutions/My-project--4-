using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityDieScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject arms;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        // When player dies, disable movement and camera
        Debug.Log("Player died!");
        GetComponent<CharacterController>().enabled = false;
        GetComponent<SC_FPSController>().enabled = false;
        arms.GetComponent<ArmBobbing>().enabled = false;
        arms.GetComponent<ArmReachingScript>().enabled = false;
        // wait for 2 seconds
        //Add die animation here

        StartCoroutine(WaitForRestart(2.0f));
    }

    IEnumerator WaitForRestart(float time)
    {
        yield return new WaitForSeconds(time);
        LoadRestartScene();
    }


    public void LoadRestartScene()
    {
        // Betölti a restart jelenetet
        SceneController.LoadScene("GameOver"); // Cseréld le a "RestartScene"-t a restart jelenet nevére
    }
}
