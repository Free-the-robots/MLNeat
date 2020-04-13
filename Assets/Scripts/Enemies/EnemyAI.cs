using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 10f;
    NavMeshAgent agent;

    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    float t = 0f;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > 0.2f)
        {
            t = 0f;
            if (Vector3.Distance(target.position, transform.position) < lookRadius)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
