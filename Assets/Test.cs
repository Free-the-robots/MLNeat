using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();


        NEAT.Person a = new NEAT.Person();

        NEAT.GENES.Node a1 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.IN, 0);
        NEAT.GENES.Node a2 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.IN, 1);
        NEAT.GENES.Node b1 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.HIDDEN, 2);
        NEAT.GENES.Node c1 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.HIDDEN, 3);
        NEAT.GENES.Node d1 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.OUT, 4);
        NEAT.GENES.Node d2 = new NEAT.GENES.Node(NEAT.GENES.Node.NODE.OUT, 5);

        NEAT.GENES.Connection a1b1 = new NEAT.GENES.Connection(a1, b1, 0.5f);
        NEAT.GENES.Connection a2b1 = new NEAT.GENES.Connection(a2, b1, 0.5f);
        NEAT.GENES.Connection b1c1 = new NEAT.GENES.Connection(b1, c1, 0.5f);
        NEAT.GENES.Connection c1d1 = new NEAT.GENES.Connection(c1, d1, 0.5f);
        NEAT.GENES.Connection a1d1 = new NEAT.GENES.Connection(a1, d1, 0.5f);
        NEAT.GENES.Connection c1d2 = new NEAT.GENES.Connection(c1, d2, 0.5f);
        NEAT.GENES.Connection b1d2 = new NEAT.GENES.Connection(b1, d2, 0.2f);
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
        Debug.Log(a.network.evaluate(inputs)[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
