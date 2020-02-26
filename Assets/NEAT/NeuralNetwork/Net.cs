using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NN
{

    public class Node
    {
        public List<Node> inNodes = null;
        public List<Node> outNodes = null;
        public List<float> w = new List<float>();
        public int nb = 0;

        public virtual float activation(float x)
        {
            return x;
        }

        public float evaluateBack(List<float> inputs)
        {
            float sum = 0f;

            if (inNodes == null)
                return activation(inputs[nb]);

            for(int i = 0; i < inNodes.Count; ++i)
            {
                sum += w[i] * inNodes[i].evaluateBack(inputs);
            }
            return activation(sum);
        }
    }

    public class Net
    {
        public List<Node> inNodes = new List<Node>();
        public List<Node> outNodes = new List<Node>();
        public List<Node> hidNodes = new List<Node>();

        /*public Net(List<List<float>> layersW)
        {
            if (layersW.Count < 2)
                Debug.LogError("Not enough layers");

            for(int i = 0; i < layersW[layersW.Count- 1].Count; ++i)
            {
                Node outNode = new Node();
                outNode.w.Add(layersW[layersW.Count - 2][i]);
                outNode.nb = layersW.Count ;
                outNodes.Add(outNode);
            }
            for (int i = layersW.Count - 2; i > 1; ++i)
            {
                for (int j = 0; j > 1; ++i)
                {
                    Node hidNode = new Node();
                hidNode.w.Add(layersW[layersW.Count - 2][i]);
            }
        }*/

        public Net(List<NEAT.GENES.Node> nodes, List<NEAT.GENES.Connection> connections)
        {
            Dictionary<int,Node> nodes_NN = new Dictionary<int,Node>();
            int nIndex = 0;
            foreach(NEAT.GENES.Node node in nodes)
            {
                Node a = new Node();
                a.nb = nIndex++;
                nodes_NN[node.nb] = a;
            }

            foreach(NEAT.GENES.Connection connection in connections)
            {
                if (nodes_NN[connection.outNode.nb].inNodes == null)
                    nodes_NN[connection.outNode.nb].inNodes = new List<Node>();
                nodes_NN[connection.outNode.nb].inNodes.Add(nodes_NN[connection.inNode.nb]);

                if (nodes_NN[connection.inNode.nb].outNodes == null)
                    nodes_NN[connection.inNode.nb].outNodes = new List<Node>();
                nodes_NN[connection.inNode.nb].outNodes.Add(nodes_NN[connection.outNode.nb]);
                nodes_NN[connection.outNode.nb].w.Add(connection.w);
            }

            foreach(NEAT.GENES.Node node in nodes)
            {
                if(node.property == NEAT.GENES.Node.NODE.IN)
                {
                    inNodes.Add(nodes_NN[node.nb]);
                }
                if (node.property == NEAT.GENES.Node.NODE.OUT)
                {
                    outNodes.Add(nodes_NN[node.nb]);
                }
                if (node.property == NEAT.GENES.Node.NODE.HIDDEN)
                {
                    hidNodes.Add(nodes_NN[node.nb]);
                }

            }
        }

        public List<float> evaluate(List<float> inputs)
        {
            List<float> res = new List<float>();

            for(int i = 0; i < outNodes.Count; ++i)
            {
                res.Add(outNodes[i].evaluateBack(inputs));
            }

            return res;
        }
    }
}
