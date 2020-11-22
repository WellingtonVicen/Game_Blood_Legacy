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
    public bool isSorted;
    [HideInInspector] public int numberP;
    [HideInInspector] public int numberS;
    public GameObject recoveryPickup;
    public GameObject bulletPickup;
    public GameObject bladeVFX, pistolVFX;
    public SoundManager sm;

    [Header("Settings")]
    [SerializeField] EnemysType enemysType;
    protected  float stoppingDistance;
    protected float currentLife;
    public float radiusAttack;
    public float radiusWalk;
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
        numberP = Random.Range(0, 10);
        numberS = Random.Range(5, 10);

        if (numberP == SpwanManager.instace.numberP || numberS == SpwanManager.instace.numberS)
        {
            isSorted = true;
            print("Sorteado");
        }
        else
        {
            isSorted = false;
            print("Nao Sorteado");
        }
        switch (enemysType)
        {
            case EnemysType.SWORD:
                stoppingDistance = 1.3f;
                currentLife = 10f;
                radiusAttack = 0.54f;
                radiusWalk = 0.58f;
                enemySpeed = 2;
                break;
            case EnemysType.PISTOL:
                stoppingDistance = 2f;
                currentLife = 10f;
                radiusAttack = 3.8f;
                radiusWalk = 0.58f;
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
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);

            }

        }
        if (walkLeft)
        {
            if (Vector2.Distance(playerTransform.position, transform.position) > stoppingDistance)
            {
                transform.Rotate(0.0f, 0, 0.0f, Space.World);
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);

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
        if (isSorted)
        {
            Invoke("Drop", 1f);
            isSorted = false;

        }


    }

    protected void Drop()
    {
        if (numberP <= 5)
        {

            Instantiate(recoveryPickup, pointAttack.position, Quaternion.identity);
        }
        else
        {
            Instantiate(bulletPickup, pointAttack.position, Quaternion.identity);
        }
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

            impact = true;

            var VFXRotation = new Quaternion();

            if (other.transform.position.x < transform.position.x)
            {

                VFXRotation = new Quaternion(0, 180, 0, 0);

            }
            else
            {

                VFXRotation = new Quaternion(0, 0, 0, 0);

            }

            TakeDamage(Projectile.instace.damage);
            Instantiate(pistolVFX, transform.position + new Vector3(0, 1.2f, 0), VFXRotation);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Blade"))
        {
            impact = true;

            var VFXRotation = new Quaternion();

            if (other.transform.position.x < transform.position.x)
            {

                VFXRotation = new Quaternion(0, 180, 0, 0);

            }
            else
            {

                VFXRotation = new Quaternion(0, 0, 0, 0);

            }

            sm.PlaySlash();
            TakeDamage(Sword.instace.damage);
            Instantiate(bladeVFX, transform.position + new Vector3 (0,1.2f,0), VFXRotation);
        }

    }
    void OnwTriggerStay2D(Collider2D other)
    {
        impact = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {

        impact = false;

    }



}



