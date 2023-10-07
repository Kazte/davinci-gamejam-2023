using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class NodeManager : Singleton<NodeManager>
{
    private List<NodeAI> nodes = new List<NodeAI>();

    [ContextMenu("Validate")]
    private void OnValidate()
    {
        nodes.Clear();

        var nodesFounded = FindObjectsByType<NodeAI>(FindObjectsSortMode.None).ToList();

        nodes = nodesFounded;
    }

    public NodeAI GetClosestNode(Vector3 position)
    {
        return nodes.OrderBy(x => Vector3.Distance(position, x.transform.position)).First();
    }

    public NodeAI GetRandomChildOfNode(NodeAI currentNode)
    {
        return currentNode.GetRandomChild();
    }
}