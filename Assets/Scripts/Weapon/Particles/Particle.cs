using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public NN.Net weapon;
    public string shooterTag = "";
    protected Rigidbody body;
    protected List<float> inputs = new List<float>(4) { 0f, 0f, 0f, 1f };

    protected Vector3 initPos;
    public float lifeTime = 5f;


    protected float t = 0f;
    // Start is called before the first frame update
    protected virtual void OnEnable()
    {
        body = GetComponent<Rigidbody>();

        initPos = transform.position;

        inputs[0] = (transform.position.z - initPos.z) / 10f;
        inputs[1] = (transform.position.x - initPos.x) / 10f;
        inputs[2] = Vector3.Distance(initPos, transform.position);
        inputs[3] = 1f;

        t = 0f;
    }
    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        evaluate();

        List<float> res = weapon.evaluate(inputs);
        Vector3 vel = new Vector3(50f * res[1], 0f, 50f * res[0]);

        apply(vel);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != shooterTag)
        {
            if (other.transform.GetComponent<PlayerController>() != null)
            {
                other.transform.GetComponent<PlayerController>().loseHealth(10);
                GameObject.Destroy(this.gameObject);
            }
            else if (other.transform.GetComponent<EnemyController>() != null)
            {
                other.transform.GetComponent<EnemyController>().loseHealth(10);
                GameObject.Destroy(this.gameObject);
            }

            if (other.transform.tag.Equals("Obstacles"))
            {
                transform.forward = Vector3.Reflect(transform.forward,other.GetContact(0).normal);
            }
        }
    }

    protected virtual void evaluate()
    {
        t += Time.deltaTime;
        inputs[0] = ((transform.position.z - initPos.z) * 1f);
        inputs[1] = ((transform.position.x - initPos.x) * 1f);
        inputs[2] = (Vector3.Distance(initPos, transform.position));
    }

    protected virtual void apply(Vector3 vel)
    {
        body.velocity = transform.TransformDirection(vel);
        //body.velocity = transform.forward * 5f;

        if (t > lifeTime)
        {
            ParticlePooling.Instance.destroy(this.gameObject);
        }
    }
}
