using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStencil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().clearStencilAfterLightingPass = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
