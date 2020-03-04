using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class POC_GameManager : MonoBehaviour
{
    public PlayerController player;

    GA.GA<NEAT.Person> ga;
    public NEAT.Person a;
    public NEAT.Person b;
    public NEAT.Person c;
    // Start is called before the first frame update
    void Start()
    {

        List<NEAT.Person> population = new List<NEAT.Person>();
        population.Add(a);
        population.Add(b);
        population.Add(c);
        ga = new GA.GA<NEAT.Person>(population, new GA.Selection.NEAT_Selection(), new GA.Crossover.NEAT_Crossover(), new GA.Mutation.NEAT_Mutation(), 10, 0.05f, 0.2f);
    }

    float time = 0f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 3.0f)
        {
            breed();
            time = 0f;
        }
    }

    public void breed()
    {
        ga.breed();

        System.Random random = new System.Random();
        player.weapon = ga.results[random.Next(ga.results.Count)];
        Debug.Log(player.weapon);
        player.weapon.buildModel();
        ga.updatePopulation(ga.results.Skip(2).Take(4).ToList());
    }
}
