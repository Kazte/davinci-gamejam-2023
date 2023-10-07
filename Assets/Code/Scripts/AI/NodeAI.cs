using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NodeAI : MonoBehaviour
{
    public List<NodeAI> Childs = new List<NodeAI>();


    private void OnDrawGizmos()
    {
        foreach (var child in Childs)
        {
            var dir = child.transform.position - transform.position;
            dir.y = 0f;
            GizmoDrawArrow.ForGizmo(transform.position, dir);
        }
    }

    public NodeAI GetRandomChild()
    {
        return Childs[Random.Range(0, Childs.Count)];
    }
}