using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerTurning : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        StartBehaviour();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBehaviour();
    }

    float timeTurn = 0f;
    protected override void shooterBehaviour()
    {
        timeTurn += Time.deltaTime;
        for (int i = 0; i < shooters.Count; ++i)
        {
            shooters[i].Rotate(0, Mathf.Sin(timeTurn)*2, 0);
        }
    }
}
