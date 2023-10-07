using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum EnemyState
{
    Normal,
    Converted
}

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;
    public Rigidbody Rb;

    [HideInInspector] public Health Health;

    public Transform Target; // TODO: Agregar el target desde un Manager para "source of truth"

    [Header("AI")]
    public int MaxHealth = 1;

    public float RotationSpeed = 0.5f;
    public float ObstacleDetectionRadius = 1.5f;
    public float WanderSpeed = 4f;
    public float RunSpeed = 6f;
    public float DetectionRadius = 4f;
    public float UndetectionRadius = 10f;

    [Header("Garbage Drop")]
    public float DropCooldown = 5f;

    [Range(0f, 1f)] public float DropChance = 1f;
    public GameObject GarbagePrefab;
    public bool CanDrop;

    [HideInInspector] public Vector3 Velocity;

    private float timerDropCooldown;

    private EnemyState enemyState = EnemyState.Normal;


    private StateMachine currentStateMachine;
    private StateMachine stateMachine = new StateMachine();
    private StateMachine stateMachineZombie = new StateMachine();

    private void Awake()
    {
        Health = new Health(1);

        Health.Modified += HealthOnModified;

        // State machine comun
        stateMachine.Add(new WanderState(this, "wander"));
        stateMachine.Add(new RunState(this, "run"));

        stateMachine.ChangeToState("wander");

        // State machine comun
        stateMachineZombie.Add(new WanderCommonState(this, "wander"));

        stateMachineZombie.ChangeToState("wander");

        currentStateMachine = stateMachine;

        timerDropCooldown = DropCooldown;

        CanDrop = true;
        GameManager.Instance.AddEnemy();
    }

    [Header("Temp")]
    public Color goodColor = Color.green;

    public Color badColor = Color.red;


    private void Update()
    {
        if (Vector3.Distance(Target.position, transform.position) > UndetectionRadius)
        {
            Rb.velocity = Vector3.zero;
            Velocity = Vector3.zero;
            return;
        }


        currentStateMachine.Update();

        if (CanDrop)
        {
            if (timerDropCooldown <= 0)
            {
                Instantiate(GarbagePrefab, transform.position, Quaternion.identity);
                GameManager.Instance.AddGarbage();
                timerDropCooldown = DropCooldown;
            }
            else
            {
                timerDropCooldown -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(Target.position, transform.position) > UndetectionRadius)
        {
            Rb.velocity = Vector3.zero;
            Velocity = Vector3.zero;
            return;
        }

        currentStateMachine.FixedUpdate();
    }


    [ContextMenu("Convert to Zombie (Good)")]
    public void ConvertToZombie()
    {
        SpriteRenderer.color = goodColor;
        currentStateMachine = stateMachineZombie;
        GameManager.Instance.RemoveEnemy();
        enemyState = EnemyState.Converted;
        CanDrop = false;
    }

    [ContextMenu("Convert to Normal (Bad)")]
    public void ConvertToNormal()
    {
        SpriteRenderer.color = badColor;
        currentStateMachine = stateMachine;
    }

    private void HealthOnModified(int currentHealth)
    {
        if (currentHealth <= 0 && enemyState == EnemyState.Normal)
        {
            ConvertToZombie();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);

            if (enemyState == EnemyState.Normal)
                Health.Modify(-1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, UndetectionRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, ObstacleDetectionRadius);
    }

    public void ChangeState(string newState)
    {
        stateMachine.ChangeToState(newState);
    }
}