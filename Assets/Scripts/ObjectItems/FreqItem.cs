using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreqItem : ObjectItem
{

    protected override void Start()
    {
        base.Start();
    }
    // Start is called before the first frame update
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().playerData.freq += 1f;
            GameObject.Destroy(this.gameObject);
        }
    }
}
