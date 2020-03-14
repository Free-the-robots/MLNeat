using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    protected float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartBehaviour();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBehaviour();
    }

    void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other);
    }

    protected virtual void StartBehaviour()
    {

    }

    protected virtual void UpdateBehaviour()
    {
        t += Time.deltaTime;
        GetComponent<Rigidbody>().velocity = (new Vector3(Mathf.Sin(t) * 2, 0f, -3));
    }

    protected virtual void TriggerEnter(Collider other)
    {
        
    }
}
