using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour
{

    private static LevelManager instance;

    public static LevelManager Instance { get { return instance; } }

    Level.Levels level = null;
    public string levelName = "";

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (levelName != "")
        {
            level = JsonUtility.FromJson<Level.Levels>(Resources.Load<TextAsset>("Levels/level").text);
        }

        //level = new Level.Levels();
        ////First Swarm
        //level.periods.Add(10f);
        //Level.Patterns a = new Level.Patterns();
        //a.enemyNB.Add(6);
        //a.enemyType.Add("Enemy");
        //a.enemyPattern.Add("Pattern1");
        //a.enemyWeapon.Add("BasicStraight");

        //a.enemyLife.Add(100);
        //a.enemyLife.Add(100);
        //a.enemyLife.Add(100);
        //a.enemyLife.Add(100);
        //a.enemyLife.Add(100);
        //a.enemyLife.Add(100);

        //a.enemyFlipped.Add(false);
        //a.enemyFlipped.Add(false);
        //a.enemyFlipped.Add(false);
        //a.enemyFlipped.Add(true);
        //a.enemyFlipped.Add(true);
        //a.enemyFlipped.Add(true);

        //a.enemyOffset.Add(new Vector3(0f, 0f, 1f));
        //a.enemyOffset.Add(new Vector3(-2f, 0f, 2f));
        //a.enemyOffset.Add(new Vector3(-4f, 0f, 3f));

        //a.enemyOffset.Add(new Vector3(0f, 0f, 1f));
        //a.enemyOffset.Add(new Vector3(2f, 0f, 2f));
        //a.enemyOffset.Add(new Vector3(4f, 0f, 3f));

        //level.patterns.Add(a);

        ////Second Swarm
        //level.periods.Add(5f);
        //Level.Patterns b = new Level.Patterns();
        //b.enemyNB.Add(4);
        //b.enemyType.Add("EnemyTurning");
        //b.enemyPattern.Add("Pattern1");
        //b.enemyWeapon.Add("BasicStraight");

        //b.enemyLife.Add(100);
        //b.enemyLife.Add(100);
        //b.enemyLife.Add(100);
        //b.enemyLife.Add(100);

        //b.enemyFlipped.Add(false);
        //b.enemyFlipped.Add(false);
        //b.enemyFlipped.Add(true);
        //b.enemyFlipped.Add(true);

        //b.enemyOffset.Add(new Vector3(4f, 0f, -4f));
        //b.enemyOffset.Add(new Vector3(0f, 0f, 0f));

        //b.enemyOffset.Add(new Vector3(-4f, 0f, -4f));
        //b.enemyOffset.Add(new Vector3(0f, 0f, 0f));

        //level.patterns.Add(b);


        ////third Swarm
        //level.periods.Add(5f);
        //Level.Patterns c = new Level.Patterns();
        //c.enemyNB.Add(2);
        //c.enemyType.Add("Enemy");
        //c.enemyPattern.Add("Pattern2");
        //c.enemyWeapon.Add("BasicStraight");

        //c.enemyLife.Add(100);
        //c.enemyLife.Add(100);

        //c.enemyFlipped.Add(false);
        //c.enemyFlipped.Add(false);

        //c.enemyOffset.Add(new Vector3(5f, 0f, -4f));
        //c.enemyOffset.Add(new Vector3(2f, 0f, 0f));

        //level.patterns.Add(c);

        //level.periods.Add(3f);
        //Level.Patterns d = new Level.Patterns();
        //d.enemyNB.Add(2);
        //d.enemyType.Add("Enemy");
        //d.enemyPattern.Add("Pattern2");
        //d.enemyWeapon.Add("BasicStraight");

        //d.enemyLife.Add(100);
        //d.enemyLife.Add(100);

        //d.enemyFlipped.Add(true);
        //d.enemyFlipped.Add(true);

        //d.enemyOffset.Add(new Vector3(-5f, 0f, -4f));
        //d.enemyOffset.Add(new Vector3(-2f, 0f, 0f));

        //level.patterns.Add(d);

        //File.WriteAllText(Application.dataPath + Path.DirectorySeparatorChar + "level.txt", JsonUtility.ToJson(level, true));
    }

    float t = 0f;
    int steps = 0;
    // Update is called once per frame
    void Update()
    {
        if(level != null)
        {
            t += Time.deltaTime;
            if (t > level.periods[steps])
            {
                for (int i = 0; i < level.patterns[steps].enemyNB[0]; i++)
                {
                    GameObject gb = GameObject.Instantiate(Resources.Load<GameObject>("Enemies/" + level.patterns[steps].enemyType[0]));
                    EnemyController enemyController = gb.GetComponent<EnemyController>();
                    enemyController.flippedX = level.patterns[steps].enemyFlipped[i];
                    enemyController.offset = level.patterns[steps].enemyOffset[i];
                    enemyController.life = level.patterns[steps].enemyLife[i];
                    enemyController.pattern = Resources.Load<EnemyPattern>("Patterns/" + level.patterns[steps].enemyPattern[0]);
                    enemyController.weapon = Resources.Load<NEAT.Person>("Weapon/" + level.patterns[steps].enemyWeapon[0]);
                }
                steps++;
                if (steps == level.patterns.Count)
                {
                    steps--;
                }
                t = 0f;
            }
        }
    }
}
