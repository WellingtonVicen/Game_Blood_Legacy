using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBehavior : EnemyBehaviour
{
    [SerializeField]
    float minDistanceToFireAttack, maxDistanceToFireAttack;
    Animator animator;
    public GameObject bullet;
    public Transform exitBullet;
    public Transform chest;
    float fireDelay;
    public SoundManager soundManager;

    Vector3 lookPos;
    [HideInInspector] public bool isShooting;
    [HideInInspector] public bool isWhip;



    void Awake()
    {
        playerTransform = GameObject.Find("Player3D").transform;
        StartStatus();

    }

    // Start is called before the first frame update
    void Start()
    {
        //StartStatus();
        //StartWeapon(); 
        animator = GetComponent<Animator>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
    }



    // Update is called once per frame
    void Update()
    {
        Verifications();
        Animations();
        WhipPistol();
        RotatePistol();

        if (impact)
        {
            contImp += Time.deltaTime;
        }

        if (contImp > 0.4f)
        {
            impact = false;
            contImp = 0;
        }

    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    void WhipPistol()
    {
        float currentDistance = Vector2.Distance(new Vector2(playerTransform.position.x, playerTransform.position.z),
        new Vector2(this.transform.position.x, this.transform.position.z));

        if (currentDistance < 1.5)
        {
            isWhip = true;
        }
        else
        {
            isWhip = false;
        }


    }

    void FollowPlayer()
    {
        if (readyAttack && !isWhip && !isDead)
        {
            chest.LookAt(new Vector2(playerTransform.position.x, playerTransform.position.y));
            fireDelay += Time.deltaTime;
            InvokeRepeating("Fire", 0.1f, 3f);
        }
        else
        {
            isShooting = false;
        }

    }

    void RotatePistol()
    {
        if (walkLeft)
        {
            transform.Rotate(0.0f, 0, 0.0f, Space.World);
        }
        else if (walkRight)
        {
            transform.Rotate(0.0f, -180f, 0.0f, Space.World);

        }
    }



    public void Fire()
    {
        if (fireDelay > 0.8f)
        {
            fireDelay = 0;
            Instantiate(bullet, exitBullet.position, Quaternion.identity);
            sm.PlayShot();
            isShooting = true;
        }

    }

    public void StartWeapon()
    {
        weapon.transform.SetParent(parentWepon);
        weapon.transform.position = parentWepon.position;
    }

    void Animations()
    {
        animator.SetBool("Impact", impact);
        animator.SetBool("Dead", isDead);
        animator.SetBool("Shooting", isShooting);
        animator.SetBool("Whip", isWhip);

    }

}
