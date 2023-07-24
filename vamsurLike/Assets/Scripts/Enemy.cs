using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    private NavMeshAgent agent;
    private GameObject targetObject;

    private Renderer enemyRenderer;

    public float damage = 8f;

    private float lastChaseTime;
    public float timeBetChase = 2.0f;

    private float lastAttackTime;
    public float timeBetAttack = 0.5f;

    public float timeBetInactive = 3.0f;

    private Animator enemyAnimator;

    public ParticleSystem bloodSprayEffect;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        enemyRenderer = GetComponentInChildren<Renderer>();

        if(GameManager.instance != null && !GameManager.instance.isGameover)
        {
            targetObject = GameManager.instance.player;
        }

        lastChaseTime = 0f;

        enemyAnimator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable(); 
        
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }

        agent.isStopped = false;
        agent.enabled = true;
    }

    public void Setup(Color newColor, float newSpeed = 3.5f, float newHP = 100f, float newDamage = 8f)
    {
        startingHP = newHP;
        HP = newHP;

        damage = newDamage;

        agent.speed = newSpeed;

        enemyRenderer.material.color = newColor;
    }

    private void Update()
    {
        if (dead)
        {
            print("dead해서 업데이트 안함");
            return;
        }

        if(lastChaseTime + timeBetChase <= Time.time)
        {
            lastChaseTime = Time.time;
            agent.SetDestination(targetObject.transform.position);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            bloodSprayEffect.transform.position = hitPoint;
            bloodSprayEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            bloodSprayEffect.Play();
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Dead()
    {
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }

        agent.isStopped = true;
        agent.enabled = false;

        enemyAnimator.SetTrigger("Die");

        float timeAfterDead = Time.time;
        //while (timeAfterDead + timeBetInactive >= Time.time) { }

        base.Dead();
    }

    private void OnTriggerStay(Collider other)
    {
        if(!dead && lastAttackTime + timeBetAttack <= Time.time)
        {
            if(other.gameObject == targetObject)
            {
                lastAttackTime = Time.time;

                Vector3 hitPoint = other.ClosestPoint(targetObject.transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                targetObject.GetComponent<LivingEntity>().OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }
}
