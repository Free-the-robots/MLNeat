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
            foreach(GENES.Connection connection in node_connect)
            {
                connection.instantiate();
            }
	}

        public float similarities(Person person)
        {
            return 0f;
        }
    }
}
