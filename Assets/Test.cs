using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test : MonoBehaviour
{
    public NEAT.Person a;
    public NEAT.Person b;
    public NEAT.Person c;

    public NEAT.Person chosen;
    public GameObject cube;

    GA.GA<NEAT.Person> ga;
    // Start is called before the first frame update
    void Start()
    {
        /*var watch = System.Diagnostics.Stopwatch.StartNew();


        a = (NEAT.Person)ScriptableObject.CreateInstance("NEAT.Person");

        NEAT.GENES.Node a1 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.IN, 0);
        NEAT.GENES.Node a2 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.IN, 1);
        NEAT.GENES.Node b1 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.HIDDEN, 2);
        NEAT.GENES.Node c1 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.HIDDEN, 3);
        NEAT.GENES.Node d1 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.OUT, 4);
        NEAT.GENES.Node d2 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.OUT, 5);

        NEAT.GENES.Connection a1b1 = new NEAT.GENES.Connection(a1.nb, b1.nb, 0.5f);
        NEAT.GENES.Connection a2b1 = new NEAT.GENES.Connection(a2.nb, b1.nb, 0.5f);
        NEAT.GENES.Connection b1c1 = new NEAT.GENES.Connection(b1.nb, c1.nb, 0.5f);
        NEAT.GENES.Connection c1d1 = new NEAT.GENES.Connection(c1.nb, d1.nb, 0.5f);
        NEAT.GENES.Connection a1d1 = new NEAT.GENES.Connection(a1.nb, d1.nb, 0.5f);
        NEAT.GENES.Connection c1d2 = new NEAT.GENES.Connection(c1.nb, d2.nb, 0.5f);
        NEAT.GENES.Connection b1d2 = new NEAT.GENES.Connection(b1.nb, d2.nb, 0.2f);
        a.node_gene.Add(a1);
        a.node_gene.Add(a2);
        a.node_gene.Add(b1);
        a.node_gene.Add(c1);
        a.node_gene.Add(d1);
        a.node_gene.Add(d2);

        a.node_connect.Add(a1b1);
        a.node_connect.Add(a2b1);
        a.node_connect.Add(b1c1);
        a.node_connect.Add(c1d1);
        a.node_connect.Add(a1d1);
        a.node_connect.Add(c1d2);
        a.node_connect.Add(b1d2);

        a.buildModel();
        watch.Stop();
        Debug.Log(watch.ElapsedMilliseconds);

        List<float> inputs = new List<float>();
        inputs.Add(0.5f);
        inputs.Add(0.5f);
        Debug.Log(a.network.evaluate(inputs)[0]);
        Debug.Log(a.network.evaluate(inputs)[1]);*/

        //a.instantiate();
        a.buildModel();

        //b.instantiate();
        b.buildModel();

        c.buildModel();

        foreach (NEAT.GENES.Connection connection in a.node_connect)
        {
            NEAT.GENES.Connection.addConnection(connection);
        }

        foreach (NEAT.GENES.Connection connection in b.node_connect)
        {
            NEAT.GENES.Connection.addConnection(connection);
        }

        foreach (NEAT.GENES.Connection connection in c.node_connect)
        {
            NEAT.GENES.Connection.addConnection(connection);
        }

        List<NEAT.Person> population = new List<NEAT.Person>();
        population.Add(a);
        population.Add(b);
        population.Add(c);

        Debug.Log(a);
        Debug.Log(b);
        Debug.Log(c);
        NEAT.GENES.Connection.global_innov = 7;

        ga = new GA.GA<NEAT.Person>(population, new GA.Selection.NEAT_Selection(), new GA.Crossover.NEAT_Crossover(), new GA.Mutation.NEAT_Mutation(), 10, 0.05f, 0.2f);
        //ga.breed();

        //for (int i = 0; i < ga.results.Count; i++)
        //    Debug.Log(ga.results[i]);
        //a.network.randomizeWeight();
        chosen = a;
    }

    float time = 0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 3.0f)
        {
            breed();
            time = 0f;
            cube.transform.position = new Vector3(0f, 0f, 0f);
            cube.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
        List<float> inputs = new List<float>();
        inputs.Add(cube.transform.position.x/10f);
        inputs.Add(cube.transform.position.z/10f);
        inputs.Add(Vector3.Distance(Vector3.zero,cube.transform.position));
        inputs.Add(1f);
        List<float> res = chosen.network.evaluate(inputs);
        //cube.transform.position = new Vector3(res[0] + cube.transform.position.x, 0f,res[1] + cube.transform.position.z);
        cube.GetComponent<Rigidbody>().velocity = (new Vector3(100f*res[0], 0f, 100f*res[1]));

    }

    public void breed()
    {
        ga.breed();

        System.Random random = new System.Random();
        chosen = ga.results[random.Next(ga.results.Count)];
        Debug.Log(chosen);
        chosen.buildModel();
        ga.updatePopulation(ga.results.Skip(2).Take(4).ToList());
    }
}
