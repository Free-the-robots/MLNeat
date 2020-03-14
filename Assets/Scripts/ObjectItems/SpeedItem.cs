using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : ObjectItem
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
            other.GetComponent<PlayerController>().playerData.speed += 5;
            GameObject.Destroy(this.gameObject);
        }
    }
}
