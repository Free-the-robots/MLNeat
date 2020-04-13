﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public PlayerData playerData;
    private Plane plane;

    public List<Weapon.Turret> weapon = new List<Weapon.Turret>();

    public GameEvent playerHealthUpdate;
    public GameEvent hitEvent;
    public GameEventInt changeWeapon;
    public GameEvent newWeaponEvent;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        weapon.AddRange(GetComponentsInChildren<Weapon.Turret>().ToList());
        plane = new Plane(Vector3.up, Vector3.zero);

        rigidbody = GetComponent<Rigidbody>();
    }


    private float t = 0f;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (t > 1F / playerData.freq)
            {
                for(int i = 0; i < weapon.Count(); ++i)
                {
                    weapon[i].Fire();
                }
                t = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            // some point of the plane was hit - get its coordinates
            Vector3 hitPoint = ray.GetPoint(distance);
            // use the hitPoint to aim your cannon
            Vector3 forward = (hitPoint - transform.position).normalized;
            forward.Scale(new Vector3(1f, 0f, 1f));
            transform.forward = forward.normalized;
            //transform.position = Vector3.MoveTowards(player.transform.position, hitPoint, Time.deltaTime * playerData.speed);
        }

        Vector3 dir = new Vector3(-Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
        rigidbody.velocity = dir*playerData.speed;
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

        newWeaponEvent.Raise();
    }
}
