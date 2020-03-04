using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class POC_GameManager : MonoBehaviour
{
    public PlayerController player;

    GA.GA<NEAT.Person> ga;
    // Start is called before the first frame update
    void Start()
    {
    }

    float time = 0f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 10.0f)
        {
            breed();
            time = 0f;
        }
    }

    public void breed()
    {
        if(ga == null)
        {
            ga = new GA.GA<NEAT.Person>(player.weapon, new GA.Selection.NEAT_Selection(), new GA.Crossover.NEAT_Crossover(), new GA.Mutation.NEAT_Mutation(), new GA.Fitness.NEAT_Fitness(), 10, 0.05f, 0.2f);
        }
        else
        {
            ga.updatePopulation(player.weapon);
        }
        ga.breed();

        System.Random random = new System.Random();
        player.weapon = new List<NEAT.Person>() { ga.results[random.Next(ga.results.Count - 1)], ga.results[random.Next(ga.results.Count - 1)], ga.results[random.Next(ga.results.Count - 1)] };
        player.chosenW = player.weapon[0];
        player.chosenW.buildModel();
        Debug.Log(player.weapon[0]);
    }
}
