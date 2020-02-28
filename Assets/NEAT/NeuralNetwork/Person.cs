using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NEAT
{
    [CreateAssetMenu(fileName = "Person", menuName = "NEAT/Person", order = 1)]
    public class Person : ScriptableObject
    {
        public List<GENES.Node> node_gene = new List<GENES.Node>();
        public List<GENES.Connection> node_connect = new List<GENES.Connection>();

        public NN.Net network = null;

        public List<float> evaluate(List<float> inputs)
        {
            int inNodesN = node_gene.Where<GENES.Node>(node => node.property == GENES.Node.NODE.IN).Count();

            if (inputs.Count != inNodesN)
                return null;

            return network.evaluate(inputs);
        }

        public void buildModel()
        {
            network = new NN.Net(node_gene, node_connect);
        }

        public void instantiate()
        {
            foreach (GENES.Connection connection in node_connect)
            {
                connection.instantiate();
            }
	    }

        public float distance(Person person)
        {
            float N = Mathf.Max(node_gene.Count, person.node_gene.Count);
            if (N < 20)
                N = 1f;

            var dissimilarNodes1 = node_connect.Where(n => !person.node_connect.Select(n1 => n1.innov).Contains(n.innov));
            var dissimilarNodes2 = person.node_connect.Where(n => !node_connect.Select(n1 => n1.innov).Contains(n.innov));
            var unionDissimilar = dissimilarNodes1.Union(dissimilarNodes2);
            List<GENES.Connection> similarNodes1 = node_connect.Where(n => person.node_connect.Select(n1 => n1.innov).Contains(n.innov)).ToList();
            List<GENES.Connection> similarNodes2 = person.node_connect.Where(n => node_connect.Select(n1 => n1.innov).Contains(n.innov)).ToList();


            float disjoints = unionDissimilar.Count();
            float weaverage = 0f;
            for(int i = 0; i < similarNodes1.Count; ++i)
            {
                weaverage += Mathf.Abs(similarNodes1[i].w - similarNodes2[i].w);
            }
            if(similarNodes1.Count != 0)
                weaverage /= similarNodes1.Count;

            float c1 = 0.5f;
            float c2 = 0.5f;

            return disjoints*c1/N + weaverage * c2;
        }

        public Person Clone()
        {
            Person res = new Person();
            res.node_connect = new List<GENES.Connection>(node_connect);
            res.node_gene = new List<GENES.Node>(node_gene);
            return res;
        }
    }
}
