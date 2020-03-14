using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public NEAT.Person weapon;
    public string shooterTag = "";
    private Rigidbody body;
    private List<float> inputs = new List<float>(4) { 0f, 0f, 0f, 1f };

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

        inputs[0] = (transform.position.z - initPos.z) / 10f;
        inputs[1] = (transform.position.x - initPos.x) / 10f;
        inputs[2] = Vector3.Distance(initPos, transform.position);
        inputs[3] = 1f;

        t = 0f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.deltaTime;
        inputs[0] = ((transform.position.z - initPos.z)*1f);
        inputs[1] = ((transform.position.x - initPos.x)*1f);
        inputs[2] = (Vector3.Distance(initPos, transform.position));
        List<float> res = weapon.network.evaluate(inputs);
        Vector3 vel = (new Vector3(50f * res[1], 0f, 50f * res[0]));
        body.velocity = transform.TransformDirection(vel);

        if (t > lifeTime)
        {
            ParticlePooling.Instance.destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != shooterTag)
        {
            if (other.GetComponent<PlayerController>() != null)
            {
                other.GetComponent<PlayerController>().loseHealth(10);
                GameObject.Destroy(this.gameObject);
            }
            else if(other.GetComponent<EnemyController>() != null)
            {
                other.GetComponent<EnemyController>().loseHealth(10);
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
