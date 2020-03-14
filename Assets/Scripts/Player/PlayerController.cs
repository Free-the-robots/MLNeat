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

    public List<Transform> drone = new List<Transform>();
    public int wIndex = 0;

    public GameEvent playerHealthUpdate;
    public GameEvent hitEvent;
    public GameEventInt changeWeapon;
    public GameEvent newWeaponEvent;

    // Start is called before the first frame update
    void Start()
    {
        weapon[0] = weapon[0].Clone();
        weapon[1] = weapon[1].Clone();
        weapon[2] = weapon[2].Clone();
        playerData = playerData.Clone();
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
            player.transform.position = Vector3.MoveTowards(player.transform.position, hitPoint, Time.deltaTime * playerData.speed);
        }

        //if (Input.GetMouseButton(0))
        //{
            if(t > 1F/playerData.freq)
            {
                for(int i = 0; i< drone.Count; ++i)
                    ParticlePooling.Instance.instantiate(this.tag, drone[i], chosenW);

                chosenW.usage++;
                if (!usedWepons.Contains(chosenW))
                {
                    usedWepons.Add(chosenW);
                }
                t = 0f;
            }
        //}

        if (Input.GetMouseButtonDown(0))
        {
            wIndex = (wIndex+1) % weapon.Count;
            chosenW = weapon[wIndex];
            changeWeapon.Raise(wIndex);
            //Debug.Log(wIndex);
        }
    }

    public void loseHealth(int health)
    {
        playerData.life -= health;

        if (playerData.life <= 0)
            playerData.life = 0;

        playerHealthUpdate.Raise();
    }

    public void addHealth(int health)
    {
        playerData.life += health;

        if (playerData.life > playerData.lifeMax)
            playerData.life = playerData.lifeMax;

        playerHealthUpdate.Raise();
    }

    public void newWeapon(NEAT.Person weaponC)
    {
        weapon[wIndex] = weaponC;
        chosenW = weaponC;

        newWeaponEvent.Raise();
    }

    public void addDrone()
    {
        GameObject droneObj = GameObject.Instantiate(drone[drone.Count - 1].gameObject, this.transform);
        drone.Add(droneObj.transform);

        int nDrone = drone.Count;
        float droneSteps = Mathf.PI / (nDrone-1);
        float radius = nDrone/3f;
        for(int i = 0; i< drone.Count; ++i)
        {
            drone[i].transform.localPosition = new Vector3(-Mathf.Cos(i * droneSteps)* radius, 0f, Mathf.Sin(i * droneSteps)* radius);
        }
    }
}
