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
        public int usage = 0;

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

            float disjoints = unionDissimilar.Count();

            float weaverage = 0f;
            /*
            List<GENES.Connection> similarConns1 = node_connect.Where(n => person.node_connect.Select(n1 => n1.innov).Contains(n.innov)).ToList();
            List<GENES.Connection> similarConns2 = person.node_connect.Where(n => node_connect.Select(n1 => n1.innov).Contains(n.innov)).ToList();


            for(int i = 0; i < similarConns1.Count; ++i)
            {
                weaverage += Mathf.Abs(similarConns1[i].w - similarConns2[i].w);
            }
            if(similarConns1.Count != 0)
                weaverage /= similarConns1.Count;*/

            float c1 = 0.5f;
            float c2 = 0.5f;

            return disjoints*c1/N + weaverage * c2;
        }

        public override string ToString()
        {
            string res = "Nodes Count : " + node_gene.Count + ", Node Connections" + node_connect.Count + "\n";
            res += "In : " + node_gene.Where(p => p.property == GENES.Node.NODE.IN).Count() + ", Out :" + node_gene.Where(p => p.property == GENES.Node.NODE.OUT).Count() + "\n\n";
            foreach (GENES.Node node in node_gene)
            {
                res += node + ", ";
            }
            res += "\n";
            foreach (GENES.Connection conn in node_connect)
            {
                res += conn;
            }
            return res;
        }

        public Person Clone()
        {
            Person res = ScriptableObject.CreateInstance<NEAT.Person>();
            res.node_connect = new List<GENES.Connection>(node_connect);
            res.node_gene = new List<GENES.Node>(node_gene);
            return res;
        }
    }
}
