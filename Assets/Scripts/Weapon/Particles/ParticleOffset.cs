﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOffset : Particle
{
    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        evaluate();

        List<float> res = weapon.evaluate(inputs);
        Vector3 vel = new Vector3(res[1] * 50f, 0f, ((res[0]+1f)/2f)*50f);

        apply(vel);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void apply(Vector3 vel)
    {
        vel.x = Mathf.Sin(vel.z*Mathf.PI/2f) * vel.x;
        body.velocity = transform.TransformDirection(vel);

        if (t > lifeTime)
        {
            ParticlePooling.Instance.destroy(this.gameObject);
        }
    }
}
