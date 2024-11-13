using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public GameObject leftHandItem;
    public GameObject rightHandItem;
    public Transform leftHand;
    public Transform rightHand;
    public float throwForce = 10.0f; // Force applied when throwing the item
    public Transform playerCamera; // Reference to the player's camera
    public float stopThreshold = 0.1f; // Threshold for considering the item as stopped
    public float checkDuration = 0.5f; // Duration to check if the item has stopped
    public float initialCheckDelay = 1.0f; // Initial delay before starting to check if the item has stopped

    public bool leftHandFull;
    public bool rightHandFull;

    void Start()
    {
        // Initialize hand states
        leftHandFull = leftHandItem != null;
        rightHandFull = rightHandItem != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHandFull)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ThrowItem(leftHandItem);
                leftHandItem = null;
                leftHandFull = false;
            }
        }

        if (rightHandFull)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ThrowItem(rightHandItem);
                rightHandItem = null;
                rightHandFull = false;
            }
        }
    }

    void ThrowItem(GameObject item)
    {
        // Ensure the item has a Rigidbody component
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody>();
        }

        // Detach the item from the player's hand
        item.transform.SetParent(null);

        // Apply force to throw the item
        rb.AddForce(playerCamera.forward * throwForce, ForceMode.VelocityChange);

        // Start coroutine to check if the Rigidbody has stopped moving
        StartCoroutine(CheckIfStopped(item, rb));
    }

    IEnumerator CheckIfStopped(GameObject item, Rigidbody rb)
    {
        // Wait for the initial delay before starting to check
        yield return new WaitForSeconds(initialCheckDelay);

        // Continuously check if the Rigidbody has stopped moving
        while (rb.velocity.magnitude > stopThreshold)
        {
            yield return new WaitForSeconds(checkDuration);
        }

        // Remove the Rigidbody component once it has stopped moving
        Destroy(rb);
    }

    public void ItemPickedUp(GameObject item, string hand)
    {
        if (hand == "left" && !leftHandFull)
        {
            leftHandItem = item;
            leftHandItem.transform.SetParent(leftHand);
            leftHandItem.transform.localPosition = Vector3.zero;
            leftHandFull = true;

            // Ensure the item does not have a Rigidbody while being held
            Rigidbody rb = leftHandItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
        }
        else if (hand == "right" && !rightHandFull)
        {
            rightHandItem = item;
            rightHandItem.transform.SetParent(rightHand);
            rightHandItem.transform.localPosition = Vector3.zero;
            rightHandFull = true;

            // Ensure the item does not have a Rigidbody while being held
            Rigidbody rb = rightHandItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
        }
    }
}