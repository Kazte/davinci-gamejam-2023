using UnityEngine;

public class RunState : State
{
    public RunState(Enemy enemy, string stateName) : base(enemy, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.CanDrop = false;
    }

    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(Enemy.Target.position, Enemy.transform.position) > Enemy.UndetectionRadius)
        {
            Enemy.ChangeState("wander");
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        var dir = Enemy.Target.position - Enemy.transform.position;
        dir.Normalize();

        Enemy.Rb.velocity = dir * -Enemy.RunSpeed;
    }
}