using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBobbing : MonoBehaviour
{
    public float leftArmBobbingSpeed = 0.05f;
    public float leftArmBobbingAmount = 0.05f;
    public float rightArmBobbingSpeed = 0.05f;
    public float rightArmBobbingAmount = 0.05f;
    public float leftArmPhaseOffset = 0.0f;
    public float rightArmPhaseOffset = Mathf.PI; // 180 degrees out of phase

    private float defaultPosYLeft;
    private float defaultPosYRight;
    private float timerLeft = 0.0f;
    private float timerRight = 0.0f;

    public Transform leftArm;
    public Transform rightArm;

    private CharacterController characterController;

    void Start()
    {
        if (leftArm != null)
            defaultPosYLeft = leftArm.localPosition.y;
        if (rightArm != null)
            defaultPosYRight = rightArm.localPosition.y;

        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        if (characterController == null)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timerLeft = 0.0f;
            timerRight = 0.0f;
        }
        else
        {
            timerLeft += leftArmBobbingSpeed;
            timerRight += rightArmBobbingSpeed;

            if (timerLeft > Mathf.PI * 2)
                timerLeft -= Mathf.PI * 2;
            if (timerRight > Mathf.PI * 2)
                timerRight -= Mathf.PI * 2;
        }

        ApplyBobbing(leftArm, defaultPosYLeft, timerLeft, leftArmBobbingAmount, leftArmPhaseOffset, horizontal, vertical);
        ApplyBobbing(rightArm, defaultPosYRight, timerRight, rightArmBobbingAmount, rightArmPhaseOffset, horizontal, vertical);
    }

    void ApplyBobbing(Transform arm, float defaultPosY, float timer, float bobbingAmount, float phaseOffset, float horizontal, float vertical)
    {
        if (arm == null)
            return;

        float waveslice = Mathf.Sin(timer + phaseOffset);
        float translateChange = waveslice * bobbingAmount;
        float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
        translateChange = totalAxes * translateChange;
        Vector3 localPosition = arm.localPosition;
        localPosition.y = defaultPosY + translateChange;
        arm.localPosition = localPosition;
    }
}