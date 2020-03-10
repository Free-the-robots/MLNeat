﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public NEAT.Person weapon;
    private Rigidbody body;
    private List<float> inputs = new List<float>(4);

    public ParticlePooling pool;

    private Vector3 initPos;
    public float lifeTime = 5f;
    float t = 0f;
    // Start is called before the first frame update
    void OnEnable()
    {
        body = GetComponent<Rigidbody>();
        if(weapon != null)
        {
            if (weapon.network == null)
                weapon.buildModel();
        }
        initPos = transform.position;

        inputs.Add((transform.position.z - initPos.z) / 10f);
        inputs.Add((transform.position.x - initPos.x) / 10f);
        inputs.Add(Vector3.Distance(initPos, transform.position));
        inputs.Add(1f);

        t = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        inputs[0] = ((transform.position.z - initPos.z)*1f);
        inputs[1] = ((transform.position.x - initPos.x)*1f);
        inputs[2] = (Vector3.Distance(initPos, transform.position));
        List<float> res = weapon.network.evaluate(inputs);
        body.velocity = (new Vector3(50f * res[1], 0f, 50f * res[0]));
        if (t > lifeTime)
            pool.destroy(this.gameObject);
    }
}