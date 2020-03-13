using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public NEAT.Person weapon;
    public EnemyPattern pattern;
    public Vector3 offset;
    public bool flippedX = false;

    public int life = 100;

    float pathTime = 0f;
    float waitTime = 0f;

    int periodOn = 0;
    int periodOff = 0;

    Vector3 StartV;


    Vector3 bezierCurve(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        if (t > 1f)
            t = 1f;
        return (1f - t) * (1f - t) * a + 2 * (1f - t) * t * b + t * t * c;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartV = pattern.Start;
        transform.position = offset + pattern.Start;
        if (flippedX)
            StartV.x = -StartV.x;
    }

    float t = 0f;
    int iSteps = 0;
    // Update is called once per frame
    void Update()
    {
        pathTime += Time.deltaTime;

        Vector3 path = bezierCurve(pattern.path[iSteps], pattern.path[iSteps + 1], pattern.path[iSteps + 2], pathTime);
        if (flippedX)
            path.x = -path.x;
        transform.position = offset + StartV + path;// + Vector3.Lerp(pattern.path[iSteps], pattern.path[iSteps+1], pathTime / pattern.time[iSteps]);

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
        if (t > 1f / pattern.particleFreq)
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

    public void loseHealth(int health)
    {
        life -= health;
        if(life <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
