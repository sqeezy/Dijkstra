using System;
using System.Collections.Generic;

namespace Dijkstras
{
    internal class Graph
    {
        private Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();

        public void add_vertex(char name, Dictionary<char, int> edges)
        {
            vertices[name] = edges;
        }
		//comments to add
        public List<char> shortest_path(char start, char finish)
        {
            Dictionary<char, char> previous = new Dictionary<char, char>();
            Dictionary<char, int> distances = new Dictionary<char, int>();
            List<char> nodes = new List<char>();

            List<char> path = null;

            foreach (KeyValuePair<char, Dictionary<char, int>> vertex in vertices)
            {
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodes.Add(vertex.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == finish)
                {
                    path = new List<char>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in vertices[smallest])
                {
                    var alternativeDistance = distances[smallest] + neighbor.Value;
                    if (alternativeDistance < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alternativeDistance;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }

            return path;
        }
    }
}