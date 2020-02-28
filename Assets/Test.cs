using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test : MonoBehaviour
{
    public NEAT.Person a;
    public NEAT.Person b;
    public GameObject cube;

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

        a.instantiate();
        a.buildModel();

        b.instantiate();
        b.buildModel();

        List<NEAT.Person> population = new List<NEAT.Person>();
        population.Add(a);
        population.Add(b);

        GA.GA<NEAT.Person> ga = new GA.GA<NEAT.Person>(population, new GA.Selection.NEAT_Selection(), new GA.Crossover.NEAT_Crossover(), new GA.Mutation.NEAT_Mutation());
        ga.breed();
        Debug.Log(ga.results[0].node_connect.Count);
        Debug.Log(ga.results[0].node_gene.Count);
        Debug.Log(ga.results[1].node_connect.Count);
        Debug.Log(ga.results[1].node_gene.Count);
        //a.network.randomizeWeight();
    }

    // Update is called once per frame
    void Update()
    {

       /* List<float> inputs = new List<float>();
        inputs.Add(cube.transform.position.x/100f);
        inputs.Add(cube.transform.position.z/100f);
        inputs.Add(Vector3.Distance(Vector3.zero,cube.transform.position));
        inputs.Add(1f);
        List<float> res = a.network.evaluate(inputs);
        //cube.transform.position = new Vector3(res[0] + cube.transform.position.x, 0f,res[1] + cube.transform.position.z);
        cube.GetComponent<Rigidbody>().velocity = (new Vector3(10f*res[0], 0f, 10f*res[1]));*/

    }
}
