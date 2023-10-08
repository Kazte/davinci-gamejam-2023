using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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
    public Transform Body;

    [HideInInspector] public Health Health;

    public Transform Target; // TODO: Agregar el target desde un Manager para "source of truth"

    [Header("AI")]
    public int MaxHealth = 1;

    public float RotationSpeed = 0.5f;
    public float ReachNodeDistance = 0.6f;
    public float WanderSpeed = 4f;
    public float ObstacleDetectionRadius = 1.5f;
    public float RunSpeed = 6f;
    public float DetectionRadius = 4f;
    public float UndetectionRadius = 10f;

    [Header("VFX")]
    public ParticleSystem SleepParticleSystem;

    public ParticleSystem ConfusedParticleSystem;
    public GameObject ConfusedHat;


    [Header("Garbage Drop")]
    public float DropCooldown = 5f;

    public float GarbageRadius = 15f;


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

        // State machine malo >:(
        stateMachine.Add(new WanderState(this, "wander"));
        stateMachine.Add(new RunState(this, "run"));

        stateMachine.ChangeToState("wander");

        // State machine bueno :)
        stateMachineZombie.Add(new WanderCommonState(this, "wander"));
    }

    private void Start()
    {
        currentStateMachine = stateMachine;

        timerDropCooldown = DropCooldown;

        // CanDrop = true;
        GameManager.Instance.AddEnemy();

        SleepParticleSystem.Pause();
        SleepParticleSystem.Clear();

        ConfusedParticleSystem.Pause();
        ConfusedParticleSystem.Clear();
    }

    [Header("Temp")]
    public Color goodColor = Color.green;

    public Color badColor = Color.red;

    private Vector3 lastDirection;

    private void Update()
    {
        currentStateMachine.Update();


        // Body.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, Velocity.z));
        Vector3 direction = -lastDirection;

        // Ensure the object doesn't roll by setting its up direction to the world up
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        Body.rotation = Quaternion.Slerp(Body.rotation, rotation, 25f * Time.deltaTime);

        var distance = Vector3.Distance(Target.position, transform.position);
        if (CanDrop && distance < GarbageRadius && !GameManager.Instance.GetBluePowerUp())
        {
            if (timerDropCooldown <= 0)
            {
                if (Random.value > DropChance)
                {
                    Instantiate(GarbagePrefab, transform.position, Quaternion.identity);
                    GameManager.Instance.AddGarbage();
                }

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
        if (GameManager.Instance.GetBluePowerUp() && enemyState == EnemyState.Normal)
        {
            Rb.velocity = Vector3.zero;
            Velocity = Vector3.zero;
            Animator.speed = 0;
            SleepParticleSystem.Play();

            return;
        }

        if (GameManager.Instance.GetBlackPowerUp() && enemyState == EnemyState.Normal)
        {
            ConfusedParticleSystem.Play();
            ConfusedHat.SetActive(true);
        }
        else if (!GameManager.Instance.GetBluePowerUp())
        {
            ConfusedParticleSystem.Pause();
            ConfusedParticleSystem.Clear();
            ConfusedHat.SetActive(false);
        }

        SleepParticleSystem.Pause();
        SleepParticleSystem.Clear();


        Animator.speed = 1;
        currentStateMachine.FixedUpdate();
        lastDirection = Velocity;
    }


    [ContextMenu("Convert to Zombie (Good)")]
    public void ConvertToZombie()
    {
        AudioManager.Instance.Play("Transform_Npc");
        SpriteRenderer.color = goodColor;
        currentStateMachine = stateMachineZombie;
        GameManager.Instance.RemoveEnemy();
        enemyState = EnemyState.Converted;
        CanDrop = false;
        stateMachineZombie.ChangeToState("wander");
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, GarbageRadius);
        Gizmos.color = Color.white;
    }

    public void ChangeState(string newState)
    {
        stateMachine.ChangeToState(newState);
    }
}