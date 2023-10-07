﻿using UnityEngine;

public class WanderState : State
{
    private NodeAI currentNode;

    public WanderState(Enemy enemy, string stateName) : base(enemy, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        currentNode = NodeManager.Instance.GetClosestNode(Enemy.transform.position);
        Enemy.CanDrop = true;
    }

    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(Enemy.transform.position, currentNode.transform.position) <= 0.1f)
        {
            currentNode = NodeManager.Instance.GetRandomChildOfNode(currentNode);
        }

        if (Vector3.Distance(Enemy.transform.position, Enemy.Target.position) <= Enemy.DetectionRadius)
        {
            Enemy.ChangeState("run");
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        var dir = currentNode.transform.position - Enemy.transform.position;
        dir.Normalize();


        Enemy.Rb.velocity = dir * Enemy.WanderSpeed;
    }
}