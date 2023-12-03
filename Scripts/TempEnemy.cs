using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State { Idle, Patrol, Attack, Stunned };
public class TempEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GunScript gun;
    [SerializeField] private TempLootSplash lootSplash;
    [SerializeField] private Transform player;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float range = 8;
    [SerializeField] private float idleDelay = 3.5f;
    [SerializeField] private float idleTimer;
    [SerializeField] private float stunDelay = 1f;
    [SerializeField] private float stunTimer;
    [SerializeField] private float shootDelay = 5f;
    [SerializeField] private float shootTimer;
    public float health = 100;
    [SerializeField] private State currentState;
    private float deathTimer;
    private Animator a;

    bool facingForwards;

    private void Awake()
    {
        a = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        lootSplash = GetComponent<TempLootSplash>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Patrol;
        gun.gameObject.tag = "EnemyWeapon";
        idleTimer = 0f;
        stunTimer = 0f;
        shootTimer = 0f;
        deathTimer = 0f;
        currentTarget = pointA;
        agent.destination = currentTarget.position;
        a.SetBool("isDead", false);
        a.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot Cooldown
        if (shootTimer < shootDelay)
        {
            shootTimer += Time.deltaTime;
        }
        else
        {
            shootTimer = 0f;
            gun.onFireUp();
        }


        //Update state
        if (Vector3.Distance(player.position, transform.position) <= range && !(currentState == State.Stunned))
        {
            currentState = State.Attack;
        }
        else if (currentState == State.Attack && !(currentState == State.Stunned))
        {
            currentState = State.Patrol;
        }

        //Different States
        switch(currentState)
        {
            case State.Idle:
                idle();
                break;
            case State.Patrol:
                patrol();
                break;
            case State.Attack:
                attack();
                break;
            case State.Stunned:
                stunned();
                break;
        }

        if (health <= 0)
        {
            die();
        }
    }

    //called by grenade
    public void grenadeHit(Vector3 explosionCenter, float explosionForce, float explosionRadius)
    {
        SoundManager.Instance.playSound("Enemy_Damage");
        health -= 34;
        agent.enabled = false;
        rb.isKinematic = false;
        //rb.inertiaTensor = new Vector3(0, 0, 0);
        rb.AddExplosionForce(explosionForce, explosionCenter, explosionRadius);
        currentState = State.Stunned;
        stunTimer = 0f;
    }

    //enemy attack behavior
    void attack()
    {
        agent.stoppingDistance = range/2;
        agent.speed = speed/2;
        agent.destination = player.position;
        gun.onClick(agent.velocity.x, "Bullet");
        gun.ammo = 30;
    }

    //enemy patrol behavior
    void patrol()
    {
        a.SetBool("isIdle", false);
        agent.stoppingDistance = 0f;
        agent.speed = speed;
        if (agent.remainingDistance == 0 && currentTarget == pointB)
        {
            currentTarget = pointA;
            currentState = State.Idle;
        }
        else if (agent.remainingDistance == 0)
        {
            currentTarget = pointB;
            currentState = State.Idle;
        }
        agent.destination = currentTarget.position;
    }
    
    //enemy idle behavior
    void idle()
    {
        a.SetBool("isIdle", true);
        agent.speed = 0;
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDelay)
        {
            idleTimer = 0;
            currentState = State.Patrol;
        }
    }

    //enemy stun behavior
    void stunned()
    {
        a.SetBool("isIdle", true);
        stunTimer += Time.deltaTime;
        if (stunTimer > stunDelay)
        {
            agent.enabled = true;
            rb.isKinematic = true;
            agent.destination = currentTarget.position;
            currentState = State.Patrol;
        }
    }

    //called when the enemy reaches 0 health
    void die()
    {
        if (deathTimer < 1.5f)
        {
            a.SetBool("isDead", true);
            deathTimer += Time.deltaTime;
        }
        else
        {
            SoundManager.Instance.playSound("Enemy_Death");

            //DROP GUN
            gun.transform.parent = null;
            gun.gameObject.tag = "Weapon";
            gun.onEnemyDeath();
            gun.transform.position = this.transform.position + new Vector3(0, 1, 0);
            gun.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            //SPAWN LOOT AND DELETE
            lootSplash.spawnLoot();
            Destroy(this.gameObject);
        }
    }

    //used to detect bullet hits
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            SoundManager.Instance.playSound("Enemy_Damage");
            health -= collision.gameObject.GetComponent<BulletScript>().damage;
            Destroy(collision.gameObject);
        }
    }
}
