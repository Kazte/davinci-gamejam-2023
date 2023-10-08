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

        Enemy.Rb.velocity = Vector3.zero;

        Enemy.Velocity += SteeringBehaviour.Flee(Enemy.transform.position,
            Enemy.Target.position, Enemy.RunSpeed, Enemy.RotationSpeed, Enemy.Velocity);

        var colliders =
            Physics.OverlapSphere(Enemy.transform.position, Enemy.ObstacleDetectionRadius,
                LayerMask.GetMask("Obstacle"));


        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                if (!collider)
                    continue;

                var go = collider.gameObject;

                var end = go.transform.position;


                Debug.DrawLine(Enemy.transform.position, end,
                    Color.magenta);

                Enemy.Velocity += SteeringBehaviour.Flee(Enemy.transform.position,
                    end, Enemy.WanderSpeed * 3f, Enemy.RotationSpeed * 2f, Enemy.Velocity);
            }
        }

        Enemy.Velocity.y = 0f;
        Enemy.Rb.velocity += Enemy.Velocity;
    }
}