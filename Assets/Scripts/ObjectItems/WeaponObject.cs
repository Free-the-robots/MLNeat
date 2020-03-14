using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : ObjectItem
{
    public NEAT.Person contained;

    private void Start()
    {
        StartBehaviour();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateBehaviour();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().newWeapon(contained);
            GameObject.Destroy(this.gameObject);
        }
    }
}
