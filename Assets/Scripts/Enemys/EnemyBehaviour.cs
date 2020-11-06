using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    Vector3 lookPos;
    [HideInInspector] public bool isWalk;
    [HideInInspector] public bool walkRight;
    [HideInInspector] public bool walkLeft;
    /*  [HideInInspector] */
    [HideInInspector] public bool impact;
    public float stoppingDistance;


    [Header("References")]
    public Transform pointWalkRight;
    public Transform pointWalkLeft;
    public LayerMask playerLayer;

    public Transform pointAttack;

    [Header("Settings")]
    public bool readyAttack;
    [SerializeField] EnemysType enemysType;
    public float currentLife;
    public float radiusAttack;
    public float radiusWalk;
    public float enemySpeed;




    protected void StartStatus()
    {

        switch (enemysType)
        {
            case EnemysType.SWORD:
                currentLife = 80f;
                break;
            case EnemysType.PISTOL:
                currentLife = 120f;
                break;
        }

    }


    protected void Move()
    {
        walkRight = Physics2D.OverlapCircle(pointWalkRight.position, radiusWalk, playerLayer);
        walkLeft = Physics2D.OverlapCircle(pointWalkLeft.position, radiusWalk, playerLayer);
        readyAttack = Physics2D.OverlapCircle(pointAttack.position, radiusAttack, playerLayer);

        isWalk = walkLeft || walkRight;


        if (walkRight)
        {
            if (Vector2.Distance(playerTransform.position, transform.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
                transform.Rotate(0.0f, -180f, 0.0f, Space.World);

            }

        }
        if (walkLeft)
        {
            if (Vector2.Distance(playerTransform.position, transform.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
                transform.Rotate(0.0f, 0, 0.0f, Space.World);

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

        Destroy(this.gameObject);

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



