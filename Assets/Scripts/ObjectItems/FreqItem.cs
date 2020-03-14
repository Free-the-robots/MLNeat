using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreqItem : ObjectItem
{
    // Start is called before the first frame update
    void Update()
    {
        UpdateBehaviour();
    }

    protected override void TriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().playerData.freq += 1f;
            GameObject.Destroy(this.gameObject);
        }
    }
}
