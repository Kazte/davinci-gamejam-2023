using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class NodeManager : Singleton<NodeManager>
{
    private List<NodeAI> nodes = new List<NodeAI>();

    private void Start()
    {
        nodes.Clear();

        var nodesFounded = transform.GetComponentsInChildren<NodeAI>().ToList();

        nodes = nodesFounded;
    }

    public NodeAI GetClosestNode(Vector3 position)
    {
        var closestNode = nodes.OrderBy(x => Vector3.Distance(position, x.transform.position)).FirstOrDefault();

       return closestNode;
    }

    public NodeAI GetRandomChildOfNode(NodeAI currentNode)
    {
        return currentNode.GetRandomChild();
    }
}