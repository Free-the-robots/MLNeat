using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    protected float t = 0f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        t += Time.deltaTime;
        GetComponent<Rigidbody>().velocity = (new Vector3(Mathf.Sin(t) * 2, 0f, -3));
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
    }

}
