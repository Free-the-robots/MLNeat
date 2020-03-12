
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScrolling : MonoBehaviour
{
    public float theScrollSpeed = 0.025f;

    Transform theCamera;

    void Start()
    {
        theCamera = Camera.main.transform;
    }

    void Update()
    {
        theCamera.position = new Vector3(theCamera.position.x + theScrollSpeed, theCamera.position.y + theScrollSpeed, theCamera.position.z);
    }
}
