using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public NEAT.Person weapon;
    private Rigidbody body;
    private List<float> inputs = new List<float>(4);

    private Vector3 initPos;
    public float lifeTime = 5f;
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
    }
    float t = 0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.deltaTime;
        inputs.Add((transform.position.z - initPos.z));
        inputs.Add((transform.position.x - initPos.x));
        inputs.Add(Vector3.Distance(initPos, transform.position));
        inputs.Add(1f);
        List<float> res = weapon.network.evaluate(inputs);
        body.velocity = (new Vector3(10f * res[1], 0f, 10f * res[0]));
        if (t > lifeTime)
            GameObject.Destroy(this.gameObject);
    }
}
