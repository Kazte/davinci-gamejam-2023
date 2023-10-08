using UnityEngine;

public class WanderState : State
{
    private NodeAI currentNode;

    private Collider[] colliders = new Collider[25];

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

        if (Vector3.Distance(Enemy.transform.position, currentNode.transform.position) <= 0.5f)
        {
            currentNode = NodeManager.Instance.GetRandomChildOfNode(currentNode);
        }

        if (!GameManager.Instance.GetBlackPowerUp() &&
            Vector3.Distance(Enemy.transform.position, Enemy.Target.position) <= Enemy.DetectionRadius)
        {
            Enemy.ChangeState("run");
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();


        Enemy.Rb.velocity = Vector3.zero;

        Enemy.Velocity += SteeringBehaviour.Seek(Enemy.transform.position, currentNode.transform.position,
            Enemy.WanderSpeed, Enemy.RotationSpeed, Enemy.Velocity);

        var collidersLength =
            Physics.OverlapSphereNonAlloc(Enemy.transform.position, Enemy.ObstacleDetectionRadius, colliders,
                LayerMask.GetMask("Obstacle"));


        if (collidersLength > 0)
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
                    end, Enemy.WanderSpeed, Enemy.RotationSpeed, Enemy.Velocity);
            }
        }

        Enemy.Velocity.y = 0f;
        Enemy.Rb.velocity += Enemy.Velocity;
    }
}