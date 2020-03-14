using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct NamedObjectItems
{
    public float sizePerc;
    public string name;
    public GameObject objectItem;
}

public class POC_GameManager : MonoBehaviour
{
    public PlayerController player;

    public Transform objectTransform;

    public List<NamedObjectItems> objs = new List<NamedObjectItems>();

    GA.GA<NEAT.Person> ga;

    private float crossoverP = 0.1f;

    System.Random random = new System.Random();

    private static POC_GameManager instance;

    public static POC_GameManager Instance { get { return instance; } }


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
        //breed();
    }

    float time = 0f;
    float timeB = 10f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeB -= Time.deltaTime;
        if (ga != null)
        {
            if (time > 5f)
            {
                time = 0f;
                GameObject gb = GameObject.Instantiate(objs.Find(n=>n.name == "weapon").objectItem);
                gb.transform.position = objectTransform.position;
                WeaponObject wp = gb.GetComponent<WeaponObject>();
                NEAT.Person p = ga.results[random.Next(ga.results.Count - 1)];
                wp.contained = p;
            }
        }
            
        if(timeB < 0f)
        {
            timeB = 10f;
            if(player.usedWepons.Count > 1)
                breed();
        }

    }

    public void breed()
    {
        if(ga == null)
        {
            ga = new GA.GA<NEAT.Person>(player.weapon, new GA.Selection.NEAT_Selection(), new GA.Crossover.NEAT_Crossover(), new GA.Mutation.NEAT_Mutation(), new GA.Fitness.NEAT_Fitness(), 10, 0.05f, 0.1f);
        }
        else
        {
            ga.updatePopulation(player.usedWepons);
            ga.updateParameters(ga.population_size, ga.elitesP, crossoverP);
        }
        ga.breed();
        crossoverP += 0.01f;
        if (crossoverP > 0.6f)
            crossoverP = 0.6f;

        player.usedWepons.Clear();

        /*System.Random random = new System.Random();
        player.weapon = new List<NEAT.Person>() { ga.results[random.Next(ga.results.Count - 1)], ga.results[random.Next(ga.results.Count - 1)], ga.results[random.Next(ga.results.Count - 1)] };
        player.chosenW = player.weapon[0];
        player.chosenW.buildModel();*/
    }

    public void enemyDead(Transform transform)
    {
        double item = random.NextDouble();
        if (item > 0.5f)
        {
            float r = (float)random.NextDouble();
            float percSum = objs.Select(n => n.sizePerc).Sum();

            double sum = 0;
            List<double> cumulatedSum = objs.Select(f => sum += f.sizePerc / percSum).ToList();

            int index = cumulatedSum.FindIndex(f => f > r);
            GameObject gb = GameObject.Instantiate(objs[index].objectItem);
            gb.transform.position = transform.position;
            if (gb.GetComponent<WeaponObject>() != null)
            {
                WeaponObject wp = gb.GetComponent<WeaponObject>();
                NEAT.Person p = ga.results[random.Next(ga.results.Count - 1)];
                wp.contained = p;
            }
        }
    }
}
