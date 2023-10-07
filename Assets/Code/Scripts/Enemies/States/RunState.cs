using UnityEngine;

public class RunState : State
{
    private Collider[] colliders = new Collider[25];

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

        var raycastHitsLength =
            Physics.OverlapSphereNonAlloc(Enemy.transform.position, 1f, colliders, LayerMask.GetMask("Obstacle"));

        for (int i = 0; i < raycastHitsLength; i++)
        {
            var col = colliders[i];

            var obstacleDirection = Enemy.transform.position - col.transform.position;
            var distance = Vector3.Distance(col.transform.position, Enemy.transform.position);
            obstacleDirection.y = 0f;
            obstacleDirection.Normalize();

            dir += obstacleDirection * (-1 * (3f / distance));
        }

        Enemy.Rb.velocity = dir * -Enemy.RunSpeed;
    }
}