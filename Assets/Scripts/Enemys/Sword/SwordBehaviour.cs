using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : EnemyBehaviour
{
    Animator animator;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player3D").transform;
    }

    void Start()
    {
        StartStatus();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        Verifications();
        Move();
        Animations();

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

    void Animations()
    {
        animator.SetBool("Walk", isWalk);
        animator.SetBool("Attack", readyAttack);
        animator.SetBool("Impact", impact);
        animator.SetBool("Dead", isDead);
    }


}
