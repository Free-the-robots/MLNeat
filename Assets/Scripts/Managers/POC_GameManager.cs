using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class POC_GameManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject weaponObject;

    public Transform objectTransform;

    GA.GA<NEAT.Person> ga;

    private float crossoverP = 0.1f;

    System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        //breed();
    }

    float time = 0f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 10f)
        {
            time = 0f;
            GameObject gb = GameObject.Instantiate(weaponObject);
            gb.transform.position = objectTransform.position;
            WeaponObject wp = gb.GetComponent<WeaponObject>();
            NEAT.Person p = ga.results[random.Next(ga.results.Count - 1)];
            wp.contained = p;
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
        crossoverP += 0.1f;
        if (crossoverP > 0.6f)
            crossoverP = 0.6f;

        player.usedWepons.Clear();

        /*System.Random random = new System.Random();
        player.weapon = new List<NEAT.Person>() { ga.results[random.Next(ga.results.Count - 1)], ga.results[random.Next(ga.results.Count - 1)], ga.results[random.Next(ga.results.Count - 1)] };
        player.chosenW = player.weapon[0];
        player.chosenW.buildModel();*/
    }
}
