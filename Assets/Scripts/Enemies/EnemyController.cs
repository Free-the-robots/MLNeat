using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public NEAT.Person weapon;
    public EnemyPattern pattern;
    public Vector3 offset;
    public bool flippedX = false;

    public List<Transform> shooters = new List<Transform>();

    public int life = 100;

    protected float pathTime = 0f;
    protected float waitTime = 0f;

    protected int periodOn = 0;
    protected int periodOff = 0;

    protected Vector3 StartV;


    Vector3 bezierCurve(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        if (t > 1f)
            t = 1f;
        return (1f - t) * (1f - t) * a + 2 * (1f - t) * t * b + t * t * c;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartBehaviour();
    }

    float t = 0f;
    int iSteps = 0;
    // Update is called once per frame
    void Update()
    {
        UpdateBehaviour();
    }

    protected virtual void StartBehaviour()
    {
        StartV = pattern.Start;
        transform.position = offset + pattern.Start;
        if (flippedX)
            StartV.x = -StartV.x;

        //TODO because enabled later
        for (int i = 0; i < transform.childCount; ++i)
            shooters.Add(transform.GetChild(i));
    }

    protected virtual void UpdateBehaviour()
    {
        movingBehaviour();

        t += Time.deltaTime;
        if (t > 1f / pattern.particleFreq)
        {
            shootingPattern();
            t = 0f;
        }

        shooterBehaviour();
    }

    protected virtual void movingBehaviour()
    {
        pathTime += Time.deltaTime;

        Vector3 path = bezierCurve(pattern.path[iSteps], pattern.path[iSteps + 1], pattern.path[iSteps + 2], pathTime / pattern.time[iSteps]);
        if (flippedX)
            path.x = -path.x;
        transform.position = offset + StartV + path;// + Vector3.Lerp(pattern.path[iSteps], pattern.path[iSteps+1], pathTime / pattern.time[iSteps]);

        if (pathTime / pattern.time[iSteps] >= 1f)
        {
            pathTime = pattern.time[iSteps];

            waitTime += Time.deltaTime;
            if (waitTime >= pattern.waitTime[iSteps])
            {
                pathTime = 0f;
                waitTime = 0f;
                iSteps += 2;
                if (iSteps >= pattern.path.Count - 2)
                    GameObject.Destroy(this.gameObject);
            }
        }
    }

    protected virtual void shootingPattern()
    {
        if (periodOff > pattern.periodOff)
        {
            if (periodOn > pattern.periodOn)
            {
                periodOff = 0;
                periodOn = 0;
            }
            for(int i = 0; i < shooters.Count; ++i)
                ParticlePooling.Instance.instantiate(shooters[i], weapon, true);

            periodOn++;
        }
        else
        {
            periodOff++;
        }
    }

    protected virtual void shooterBehaviour()
    {
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
