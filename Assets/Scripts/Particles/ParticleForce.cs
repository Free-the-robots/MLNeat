using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleForce : Particle
{
    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        t += Time.deltaTime;
        inputs[0] = ((transform.position.z - initPos.z) * 1f);
        inputs[1] = ((transform.position.x - initPos.x) * 1f);
        inputs[2] = (Vector3.Distance(initPos, transform.position));
        List<float> res = weapon.network.evaluate(inputs);

        Vector3 ForceFront = new Vector3(0f, 0, 1f);

        Vector3 vel = new Vector3(50f * res[1], 0f, 50f * res[0]) + ForceFront;
        body.velocity = transform.TransformDirection(vel);

        if (t > lifeTime)
        {
            ParticlePooling.Instance.destroy(this.gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
