using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public bool isWalk;
    [HideInInspector] public bool walkRight;
    [HideInInspector] public bool walkLeft;
    [HideInInspector] public bool impact;
    [HideInInspector] public bool readyAttack;
    [HideInInspector] public bool isDead;


    [Header("References")]
    public Transform playerTransform;
    public Transform pointWalkRight;
    public Transform pointWalkLeft;
    public Transform pointAttack;
    public Transform parentWepon;
    public GameObject weapon;
    public LayerMask playerLayer;
    public LayerMask solid;

    [Header("Settings")]
    [SerializeField] EnemysType enemysType;
    protected float stoppingDistance;
    protected float currentLife;
    protected float radiusAttack;
    protected float radiusWalk;
    protected float enemySpeed;



    protected void Verifications()
    {
        walkRight = Physics2D.OverlapCircle(pointWalkRight.position, radiusWalk, playerLayer);
        walkLeft = Physics2D.OverlapCircle(pointWalkLeft.position, radiusWalk, playerLayer);
        readyAttack = Physics2D.OverlapCircle(pointAttack.position, radiusAttack, playerLayer);

        isWalk = walkLeft || walkRight;
    }
    protected void StartStatus()
    {

        switch (enemysType)
        {
            case EnemysType.SWORD:
                stoppingDistance = 1.3f;
                currentLife = 80f;
                radiusAttack = 1.89f;
                radiusWalk = 1.4f;
                enemySpeed = 2;
                break;
            case EnemysType.PISTOL:
                stoppingDistance = 2f;
                currentLife = 40f;
                radiusAttack = 5;
                radiusWalk = 3f;
                enemySpeed = 2;
                break;
        }

    }

    protected void Move()
    {

        if (walkRight)
        {
            if (Vector2.Distance(playerTransform.position, transform.position) > stoppingDistance)
            {
                transform.Rotate(0.0f, -180f, 0.0f, Space.World);
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position, enemySpeed * Time.deltaTime);

            }

        }
        if (walkLeft)
        {
            if (Vector2.Distance(playerTransform.position, transform.position) > stoppingDistance)
            {
                transform.Rotate(0.0f, 0, 0.0f, Space.World);
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position, enemySpeed * Time.deltaTime);

            }

        }

    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            Dead();
        }
    }

    protected void Dead()
    {
        // spwanManger
        isDead = true;
        Destroy(this.gameObject, 1.3f);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointWalkRight.position, radiusWalk);
        Gizmos.DrawWireSphere(pointWalkLeft.position, radiusWalk);
        Gizmos.DrawWireSphere(pointAttack.position, radiusAttack);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(Projectile.instace.damage);
            impact = true;
            print(currentLife);
        }
        else if (other.CompareTag("Blade"))
        {
            TakeDamage(Sword.instace.damage);
            impact = true;
            print(currentLife);
        }

    }
    void OnTriggerStay2D(Collider2D other)
    {
        impact = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {

        impact = false;

    }



}



