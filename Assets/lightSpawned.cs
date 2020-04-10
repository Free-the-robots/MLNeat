using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSpawned : MonoBehaviour
{
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float t = 0f;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        transform.localPosition = new Vector3(Mathf.Sin((t + i) / 20f * Mathf.PI) * (i % 10) / 2f * (Mathf.Sin((i / 10) / 5f * 2f * Mathf.PI) + 1f), 0f, (i % 10) / 2f * Mathf.Cos((i / 10) / 5f * 2 * Mathf.PI));
    }
}
