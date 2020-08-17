﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Transform _tr;
    private Animator _anim;   
    private bool facingRight;
    public bool noChao;
    public Transform groundCheck;
    public LayerMask solid;
    public float noChaoRaio;
    private float Axis;
    public float Vel;
   private bool estaAndando;
   public float jumpForce;
    
    // Start is called before the first frame update
    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
       _tr = GetComponent<Transform>();
       _anim = GetComponent<Animator>();
       facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        noChao = Physics2D.OverlapCircle(groundCheck.position, noChaoRaio, solid);
        Axis = Input.GetAxisRaw("Horizontal");
        estaAndando = Mathf.Abs(Axis) > 0;

         PermiteFlip();
    }

    void FixedUpdate()
    {
        Walk();
        Flip();
        Jump();
    }    

     private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, noChaoRaio) ;
    }

    public void Walk() 
    { 
     if(estaAndando && facingRight)
        {
            _rb.velocity = new Vector2(Vel, _rb.velocity.y);
        }
        else if (estaAndando && !facingRight)
        {
            _rb.velocity = new Vector2(-Vel, _rb.velocity.y);
        }
    }

    public void Flip() 
    { 
       facingRight = !facingRight;
        _tr.localScale = new Vector2(-_tr.localScale.x, _tr.localScale.y);
    }

    void PermiteFlip()
    {
        if(Axis > 0 && !facingRight)
        {
            Flip();
        }
        else if (Axis< 0 && facingRight)
        {
            Flip();
        }
    }

    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && noChao) 
        { 
            _rb.AddForce(_tr.up * jumpForce, ForceMode2D.Impulse);
        }

    }
}
