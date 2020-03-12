using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public NEAT.Person weapon;
    public EnemyPattern pattern;

    public float freq = 10f;
    float pathTime = 0f;
    float waitTime = 0f;

    int periodOn = 0;
    int periodOff = 0;

    Vector3 bezierCurve(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        if (t > 1f)
            t = 1f;
        return (1f - t) * (1f - t) * a + 2 * (1f - t) * t * b + t * t * c;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pattern.Start;
    }

    float t = 0f;
    int iSteps = 0;
    // Update is called once per frame
    void Update()
    {
        pathTime += Time.deltaTime;

        transform.position = pattern.Start + bezierCurve(pattern.path[iSteps], pattern.path[iSteps+1], pattern.path[iSteps+2], pathTime);// + Vector3.Lerp(pattern.path[iSteps], pattern.path[iSteps+1], pathTime / pattern.time[iSteps]);

        if (pathTime / pattern.time[iSteps] >= 1f)
        {
            pathTime = pattern.time[iSteps];

            waitTime += Time.deltaTime;
            if(waitTime >= pattern.waitTime[iSteps])
            {
                pathTime = 0f;
                waitTime = 0f;
                iSteps+=2;
                if (iSteps >= pattern.path.Count-2)
                    GameObject.Destroy(this.gameObject);
            }
        }

        t += Time.deltaTime;
        if (t > 1f / freq)
        {
            shootingPattern();
            t = 0f;
        }
    }

    public virtual void shootingPattern()
    {
        if (periodOff > pattern.periodOff)
        {
            if (periodOn > pattern.periodOn)
            {
                periodOff = 0;
                periodOn = 0;
            }

            ParticlePooling.Instance.instantiate(transform.position, weapon, true, true);

            periodOn++;
        }
        else
        {
            periodOff++;
        }
    }
}
