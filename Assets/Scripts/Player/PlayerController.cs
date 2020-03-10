using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;

    public GameObject player;
    private Plane plane;

    public List<NEAT.Person> weapon = new List<NEAT.Person>();
    public List<NEAT.Person> usedWepons = new List<NEAT.Person>();
    public NEAT.Person chosenW = null;
    public int wIndex = 0;

    public ParticlePooling pool;
    public float freq = 10f;

    public GameEvent playerHealthUp;
    public GameEvent playerHealthDown;
    public GameEvent hitEvent;

    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
        chosenW = weapon[wIndex];
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
                pool.instantiate(transform.position, chosenW);
                chosenW.usage++;
                if (!usedWepons.Contains(chosenW))
                {
                    usedWepons.Add(chosenW);
                    Debug.Log("Added chosenW");
                }

                hitEvent.Raise();

                t = 0f;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            wIndex = (wIndex+1) % weapon.Count;
            chosenW = weapon[wIndex];
            Debug.Log(wIndex);
        }
    }

    public void Hitting()
    {
        Debug.Log("yo");
    }
}
