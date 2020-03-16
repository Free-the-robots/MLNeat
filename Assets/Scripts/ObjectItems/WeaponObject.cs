using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : ObjectItem
{
    public NEAT.Person contained;

    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().newWeapon(contained);
            GameObject.Destroy(this.gameObject);
        }
    }
}
