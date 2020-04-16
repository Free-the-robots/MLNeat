using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private FloatingJoystick JoystickMoving;
    private Vector2 mInformationFromJoystickMoving;

    [SerializeField]
    private FloatingJoystick JoystickRotate;
    private Vector2 mInformationFromJoystickRotate;

    [SerializeField]
    private GameObject CharacterToMove;

    private Vector3 mRotation;
    private float mRotationSpeed;

    private float mAngleHeadingChar;

    private float mMaxSpeed = 10F;
    private Vector3 mNormalizedDirection;

    private void OnEnable()
    {
        JoystickRotate.OnActiveJoystick += Rotate;
    }

    // Start is called before the first frame update
    void Start()
    {
        mRotationSpeed = 45F;
    }

    private void Update()
    {
        if (JoystickMoving.IsJoystickActive)
            MoveCharacter();
    }

    private void Rotate()
    {
        mAngleHeadingChar = Mathf.Atan2(JoystickRotate.Direction.x, JoystickRotate.Direction.y);
        CharacterToMove.transform.rotation = Quaternion.Euler(0F, mAngleHeadingChar * Mathf.Rad2Deg, 0F);
    }

    private void MoveCharacter()
    {
        mNormalizedDirection.x = JoystickMoving.Direction.x;
        mNormalizedDirection.y = 0F;
        mNormalizedDirection.z = JoystickMoving.Direction.y;

        CharacterToMove.transform.position += mNormalizedDirection * mMaxSpeed * Time.deltaTime;
    }
}
