using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    private Plane plane;

    public NEAT.Person weapon;

    public ParticlePooling pool;
    public float freq = 10f;
    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
    }


    private float t = 0f;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            // some point of the plane was hit - get its coordinates
            Vector3 hitPoint = ray.GetPoint(distance);
            // use the hitPoint to aim your cannon
            player.transform.position = hitPoint;
        }

        if (Input.GetMouseButton(0))
        {
            if(t > 1F/freq)
            {
                pool.instantiate(transform.position, weapon);
                /*GameObject part = GameObject.Instantiate(particle, null, true);
                part.transform.position = player.transform.position;
                part.GetComponent<Particle>().weapon = weapon;
                part.GetComponent<Particle>().enabled = true;*/

                t = 0f;
            }
        }
    }
}
