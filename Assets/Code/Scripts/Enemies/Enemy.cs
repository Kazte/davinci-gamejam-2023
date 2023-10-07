using System;
using UnityEngine;
using UnityEngine.Serialization;

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

    public float WanderSpeed = 4f;
    public float RunSpeed = 6f;
    public float DetectionRadius = 4f;
    public float UndetectionRadius = 10f;

    [Header("Garbage Drop")]
    public float DropCooldown = 5f;

    [Range(0f, 1f)] public float DropChance = 1f;
    public GameObject GarbagePrefab;
    [HideInInspector] public bool CanDrop;

    private float timerDropCooldown;


    private StateMachine stateMachine = new StateMachine();

    private void Awake()
    {
        Health = new Health(1);

        Health.Modified += HealthOnModified;

        stateMachine.Add(new WanderState(this, "wander"));
        stateMachine.Add(new RunState(this, "run"));

        stateMachine.ChangeToState("wander");

        timerDropCooldown = DropCooldown;

        GameManager.Instance.AddEnemy();
    }

    [Header("Temp")]
    public Color goodColor = Color.green;

    public Color badColor = Color.red;


    private void Update()
    {
        stateMachine.Update();

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
        stateMachine.FixedUpdate();
    }


    [ContextMenu("Convert to Zombie (Good)")]
    public void ConvertToZombie()
    {
        Debug.Log("Convert to zombie (GOOD)");
        SpriteRenderer.color = goodColor;
    }

    [ContextMenu("Convert to Normal (Bad)")]
    public void ConvertToNormal()
    {
        Debug.Log("Convert to Normal (BAD)");
        SpriteRenderer.color = badColor;
    }

    private void HealthOnModified(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            ConvertToZombie();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Health.Modify(-1);
            Destroy(other.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, UndetectionRadius);
        Gizmos.color = Color.white;
    }

    public void ChangeState(string newState)
    {
        stateMachine.ChangeToState(newState);
    }
}