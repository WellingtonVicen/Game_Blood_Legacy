using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : EnemyBehaviour
{
    Animator animator;

    void Start()
    {
        StartStatus();

        playerTransform = GameObject.Find("Player3D").transform;
        animator = GetComponent<Animator>();


    }

    void Update()
    {
        Verifications();
        Move();
        Animations();
    }

    void Animations()
    {
        animator.SetBool("Walk", isWalk);
        animator.SetBool("Attack", readyAttack);
        animator.SetBool("Impact", impact);
        animator.SetBool("Dead", isDead);
    }


}
