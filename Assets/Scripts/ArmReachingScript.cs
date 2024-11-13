using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmReachingScript : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    public float reachDistance = 1.0f;
    public float reachSpeed = 5.0f;
    public Camera playerCamera;

    private Vector3 leftHandOriginalLocalPosition;
    private Vector3 rightHandOriginalLocalPosition;
    private bool isLeftHandReaching = false;
    private bool isRightHandReaching = false;

    private Transform mainBody;

    void Start()
    {
        if (leftHand != null)
            leftHandOriginalLocalPosition = leftHand.localPosition;
        if (rightHand != null)
            rightHandOriginalLocalPosition = rightHand.localPosition;

        mainBody = transform.parent.parent;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            isLeftHandReaching = true;
        }
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            isRightHandReaching = true;
        }

        Vector3 reachDirection = playerCamera.transform.forward;

        if (isLeftHandReaching)
        {
            Vector3 targetPosition = mainBody.TransformPoint(leftHandOriginalLocalPosition) + reachDirection * reachDistance;
            leftHand.position = Vector3.MoveTowards(leftHand.position, targetPosition, Time.deltaTime * reachSpeed);
            if (Vector3.Distance(leftHand.position, targetPosition) < 0.01f)
            {
                isLeftHandReaching = false;
            }
        }
        else
        {
            Vector3 originalPosition = mainBody.TransformPoint(leftHandOriginalLocalPosition);
            leftHand.position = Vector3.MoveTowards(leftHand.position, originalPosition, Time.deltaTime * reachSpeed);
        }

        if (isRightHandReaching)
        {
            Vector3 targetPosition = mainBody.TransformPoint(rightHandOriginalLocalPosition) + reachDirection * reachDistance;
            rightHand.position = Vector3.MoveTowards(rightHand.position, targetPosition, Time.deltaTime * reachSpeed);
            if (Vector3.Distance(rightHand.position, targetPosition) < 0.01f)
            {
                isRightHandReaching = false;
            }
        }
        else
        {
            Vector3 originalPosition = mainBody.TransformPoint(rightHandOriginalLocalPosition);
            rightHand.position = Vector3.MoveTowards(rightHand.position, originalPosition, Time.deltaTime * reachSpeed);
        }
    }
}