using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveScript : MonoBehaviour
{
    public Transform plateTransform; // Reference to the plate's transform
    public Animator microwaveAnimator; // Reference to the microwave's animator

    private bool isMicrowaveOpen = false; // Flag to check if the microwave is open

    private bool isMicroOccupied = false; // Flag to check if the microwave is occupied

    private AudioSource[] audiosource;

    public GameObject thunder;

    public ParticleSystem fire;

    public AudioClip alarmSound;

    public AudioSource netfit;

    public bool isFork = false;



    // Start is called before the first frame update
    void Start()
    {
        netfit = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        if (microwaveAnimator == null)
        {
            microwaveAnimator = GetComponent<Animator>();
        }

        if (audiosource == null)
        {
            audiosource = GetComponents<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "WholePlate")
        {
            foreach (Transform child in other.gameObject.transform)
            {
                if (child.name == "Fork")
                {
                    child.gameObject.tag = "Untagged";
                }
            }
        }
        if (other.CompareTag("Interactable"))
        {
            if (microwaveAnimator.GetBool("Nyitva"))
            {
                if (!isMicroOccupied)
                {
                    PlaceItemOnPlate(other.gameObject);
                }

            }
        }
    }

    void PlaceItemOnPlate(GameObject item)
    {
        // make the item smaller relative to its current size
        // make it smaller by 50%

        //LeftHandItem or RightHandItem 

        PickupScript pickupScript = GameObject.Find("Player").GetComponent<PickupScript>();
        if (pickupScript.leftHandItem == item || pickupScript.rightHandItem == item)
        {
            return;
        }
        item.transform.localScale = new Vector3(item.transform.localScale.x * 0.5f, item.transform.localScale.y * 0.5f, item.transform.localScale.z * 0.5f);

        item.tag = "Untagged";
        item.transform.position = plateTransform.position;
        item.transform.rotation = plateTransform.rotation;
        item.transform.SetParent(plateTransform);
        item.GetComponent<Rigidbody>().isKinematic = true;
        isMicroOccupied = true;
        microwaveAnimator.SetBool("Nyitva", false);
        // make microwave not tagged

        gameObject.tag = "Untagged";

        // LevleStartscript call function


        if (ContainsFork(item))
        {
            Debug.Log("Ez ez gecire villa");
            StartCoroutine(DESTROYTHEWORLD());
            isFork = true;

        }
        else
        {
            Debug.Log("Ez nem villa");
            isFork = false;
        }

        LevelStartScript levelStartScript = GameObject.Find("Player").GetComponent<LevelStartScript>();
        if (levelStartScript != null)
        {
            levelStartScript.startMicrowave();
            audiosource[0].Play();
        }


    }


    bool ContainsFork(GameObject item)
    {
        if (item.name == "Fork")
        {
            return true;
        }

        foreach (Transform child in item.transform)
        {
            if (child.name == "Fork")
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator DESTROYTHEWORLD()
    {
        yield return new WaitForSeconds(3);
        netfit.PlayOneShot(alarmSound);
        thunder.SetActive(true);
        yield return new WaitForSeconds(1);
        fire.Play();
        yield return new WaitForSeconds(1);
        thunder.SetActive(false);

    }
}