using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public NEAT.Person contained;

    // Update is called once per frame
    float t = 0f;
    void Update()
    {
        t += Time.deltaTime;
        GetComponent<Rigidbody>().velocity = (new Vector3(Mathf.Sin(t)*2, 0f, -3));
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
