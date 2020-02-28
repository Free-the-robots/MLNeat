using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace GA.Mutation
{
    public class NEAT_Mutation : GA_Mutation<NEAT.Person>
    {
        public override NEAT.Person mutation(NEAT.Person person)
        {
            NEAT.Person res = person.Clone();
            float r = UnityEngine.Random.value;
            if(r > 0.5f)
            {
                List<Tuple<int, int>> innout = new List<Tuple<int, int>>();
                for(int i = 0; i < person.node_gene.Count; ++i)
                {
                    for(int j = i; j < person.node_gene.Count; ++j)
                    {
                        innout.Add(new Tuple<int, int>(person.node_gene[i].nb, person.node_gene[j].nb));
                    }
                }
                List<Tuple<int, int>> connectionsPossible = innout.Where(
                    p=>!person.node_connect.Select(n=>n.inNode).Contains(p.Item1)
                || !person.node_connect.Select(n => n.outNode).Contains(p.Item2)).ToList();

                if (connectionsPossible.Count > 0)
                {
                    System.Random random = new System.Random();
                    int rr = random.Next(connectionsPossible.Count);
                    int nb1 = connectionsPossible[rr].Item1;
                    int nb2 = connectionsPossible[rr].Item2;
                    NEAT.GENES.Connection connection = new NEAT.GENES.Connection(nb1, nb2, UnityEngine.Random.Range(-1f, 1f), true);

                    person.node_connect.Add(connection);
                }
            }
            else
            {
                System.Random random = new System.Random();
                int ran = random.Next(person.node_connect.Count);
                person.node_connect[ran].enabled = false;

                NEAT.GENES.Node newn = new NEAT.GENES.Node();
                newn.activation = (NEAT.GENES.Node.ACTIVATION)random.Next(5);
                newn.nb = person.node_gene.Select(n => n.nb).Max()+1;
                newn.property = NEAT.GENES.Node.NODE.HIDDEN;

                NEAT.GENES.Connection con1 = new NEAT.GENES.Connection(person.node_connect[ran].inNode, newn.nb, 1f, true);
                NEAT.GENES.Connection con2 = new NEAT.GENES.Connection(newn.nb, person.node_connect[ran].outNode, person.node_connect[ran].w, true);

                person.node_gene.Add(newn);

                person.node_connect.Add(con1);
                person.node_connect.Add(con2);

            }
            return res;
        }
    }
}
