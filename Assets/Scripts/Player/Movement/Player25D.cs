﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player25D : MonoBehaviour
{
    private const float V = 0.5882102f;
    float Axis;
    [Header("Setings")]
    public float currentLife;
    public PlayerType playerType;
    [Range(0, 10)] public float sensitivity;
    [Range(0, 10)] public float noChaoRaio;
    [Range(0, 15)] public float jumpForce;
    [HideInInspector] public bool estaAndando;
    private bool praTrasE;
    private bool praTrasD;
    private bool fallTrigger;
    private bool jumpTrigger;
    [HideInInspector] public bool facingRight;
    [HideInInspector] public bool noChao;
    [HideInInspector] public bool slide;
    [Range(0, 10)] public float slideForce;
    [Range(0, 15)] public float valorPoint;
    public bool possuiHabilPulo;
    private Transform _tr;
    [Header("Reference")]
    public Transform groundCheckTransform;
    public LayerMask solid;
    public LayerMask plataforma;
    public Transform targetTransform;
    //private CapsuleCollider capsule;
    private Animator animator;
    private Rigidbody2D rb;
    private bool gravidade1;
    private bool gravidade2;
    private float gravitScale;
    private LoadScene newPortal;
    private bool loadingScene;

    public Transform target;
    public Vector3 offset;

    public Transform chest;
    public Transform hand;
    public Transform lowerArm;
    Transform BD;
    Transform hips;
    public static Vector3 mousePosition;
    Vector3 lookPos;
    private float contador;
    [HideInInspector] public int qtosPulos;
    [HideInInspector] public bool isDead;
    public float crouched;
    public static bool crouch;
    private string sceneName;
    [HideInInspector] public Vector3 startPosition;
    public static bool naPlatarmorma;
    /* [HideInInspector] */
    public bool possuiChave;
    public Transform targetBullets;
    public GerennciadorArmas gerennciadorArmas;
    public GameObject bladeVFX, pistolVFX, jumpVFX, fallVFX;
    public SoundManager sm;
    public bool win;

    [Header("UI")]
    public Image healthBarPistol;
    public Image healthBarSword;
    public Image Fade;
    public GameObject objective;
    public GameObject objective1;
    public GameObject avisoPorta;
    public Animator avisoPortaChave;
    public GameObject canvas;

    private float fadeTransparency;

    public static Player25D instace;
    public static Player25D Instace { get { return Instace; } }

    void Start()
    {

        fadeTransparency = 1f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
        //capsule = GetComponent<CapsuleCollider>();
        facingRight = true;
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        hand = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
        slide = false;
        gerennciadorArmas.StartWepon();
        isDead = false;

    }


    void Update()
    {

        noChao = Physics2D.OverlapCircle(groundCheckTransform.position, noChaoRaio, solid);

        if (noChao)
        {

            if (fallTrigger && rb.velocity.y < -3f)
            {

                sm.PlayFalling();
                Instantiate(fallVFX, targetBullets.transform.position, new Quaternion(0, 0, 0, 0));
                fallTrigger = false;

            }

        }
        else
        {

            fallTrigger = true;

        }

        //Sistema de Fade
        if (loadingScene)
        {

            fadeTransparency += 2 * Time.deltaTime;

            if (fadeTransparency > 1)
            {
                if (jumpTrigger)
                {

                    rb.velocity = Vector2.zero;
                    rb.AddForce(_tr.up * jumpForce, ForceMode2D.Impulse);
                    jumpTrigger = false;

                }

                SceneManager.LoadScene(newPortal.sceneName);
                if (win)
                {

                    Destroy(this.gameObject);
                    Destroy(targetTransform.gameObject);
                    Destroy(canvas.gameObject);

                }
                else
                {

                    gameObject.transform.position = startPosition;

                }

                loadingScene = false;

            }

        }
        else if (!loadingScene && fadeTransparency > 0f)
        {

            fadeTransparency -= 2 * Time.deltaTime;

        }

        // naPlatarmorma = Physics2D.OverlapCircle(groundCheckTransform.position,noChaoRaio);
        Axis = Input.GetAxis("Horizontal");
        estaAndando = Axis != 0;
        praTrasE = Axis < 0 && facingRight;
        praTrasD = Axis > 0 && !facingRight;

        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, fadeTransparency);

        Walk();
        Jump();
        Animations();
        ControleDeFisica();
        HandleRotation();
        controleSlide();
        Slide();
        RequestSwitch();
        RequestAttacks();
        RequestReload();


    }

    void FixedUpdate()
    {
        // rb.velocity = new Vector3(inputMovement * walkSpeed, rb.velocity.y,0 ); 2.5D
        HandleAimPos();
        Tras();
        Crouch();

    }

    private void LateUpdate()
    {
        if (!isDead)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            target.localPosition = ray.GetPoint(valorPoint);

            chest.LookAt(new Vector2(target.position.x, target.position.y));
            //hand.LookAt(target.localPosition);
            //print(target.localPosition);

            // Pistol.instace.projectileExit.LookAt(new Vector2(target.position.x, target.position.y));

            chest.rotation = chest.rotation * Quaternion.Euler(offset);
            hand.rotation = hand.rotation * Quaternion.Euler(offset);
        }
    }

    void RequestSwitch()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            gerennciadorArmas.WeaponSwitch();
        }

    }

    void RequestReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gerennciadorArmas.RequestReload();
        }
    }
    void HandleAimPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookPos = lookP;
        }

    }

    void HandleRotation()
    {
        Vector3 directionToLook = lookPos - transform.position;
        directionToLook.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);

        //Debug.Log(lookPos.x + " " + transform.position.x);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);
        transform.rotation = targetRotation;

        //Debug.Log(targetRotation);
        if (targetRotation.y < 0.5f)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }
    void Walk()
    {
        if (estaAndando && facingRight && Axis > 0 && !isDead)
        {
            rb.velocity = new Vector2(sensitivity, rb.velocity.y);
        }
        else if (estaAndando && !facingRight && Axis < 0 && !isDead)
        {
            rb.velocity = new Vector2(-sensitivity, rb.velocity.y);
        }

    }

    void Tras()
    {
        if (praTrasD)
        {
            rb.velocity = new Vector2(sensitivity, rb.velocity.y);
        }
        else if (praTrasE)
        {
            rb.velocity = new Vector2(-sensitivity, rb.velocity.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckTransform.position, noChaoRaio);

    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && noChao && !crouch && !isDead)
        {
            rb.AddForce(_tr.up * jumpForce, ForceMode2D.Impulse);
            qtosPulos++;
            Instantiate(jumpVFX, targetBullets.transform.position, new Quaternion(0, 0, 0, 0));
            sm.PlayJump();

        }
        else if (Input.GetButtonDown("Jump") && !noChao && qtosPulos <= 1 && possuiHabilPulo && !crouch && !isDead)
        {
            qtosPulos++;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(_tr.up * jumpForce, ForceMode2D.Impulse);

        }
        else if (noChao && qtosPulos >= 2 || noChao && qtosPulos >= 1 && !possuiHabilPulo)
        {
            qtosPulos = 0;
        }

    }

    public void ControleDeFisica()
    {
        if (gravidade1)
        {
            //Physics2D.gravity = new Vector3(0, -15, 0);
            rb.gravityScale = 1.53f;
        }
        else if (gravidade2)
        {
            //Physics2D.gravity = new Vector3(0, -30, 0);
            rb.gravityScale = 3.06f;
        }
        else
        {
            //Physics2D.gravity = new Vector3(0, -9.81f, 0);
            rb.gravityScale = 1;
        }

        gravidade1 = rb.velocity.y < 0f;
        gravidade2 = rb.velocity.y >= 0f && !Input.GetButton("Jump");
    }

    public void Animations()
    {
        animator.SetBool("Walk", estaAndando);
        animator.SetBool("Jump", !noChao);
        animator.SetBool("WalkBack", praTrasE || praTrasD);
        animator.SetBool("JumpBack", praTrasE && !noChao || praTrasD && !noChao);
        animator.SetBool("Slide", slide);
        animator.SetBool("Crouch", crouch);
        animator.SetBool("Espada", gerennciadorArmas.estaEmPunhoSword);
        animator.SetBool("Slash", Sword.instace.isSword);
    }

    void Slide()
    {
        if (Input.GetKeyDown(KeyCode.C) && facingRight && !slide && !crouch && !isDead)
        {
            rb.AddForce(new Vector2(slideForce, 0), ForceMode2D.Impulse);
            slide = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && !facingRight && !slide && !crouch && !isDead)
        {
            rb.AddForce(new Vector2(-slideForce, 0), ForceMode2D.Impulse);
            slide = true;
        }

    }

    void controleSlide()
    {
        if (slide)
        {
            contador += Time.deltaTime;
        }

        if (contador >= 0.8f)
        {
            slide = !slide;
            contador = 0;
        }

    }


    void Crouch()
    {
        crouched = Input.GetAxis("Vertical");
        crouch = crouched < 0;

    }

    public void RequestAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gerennciadorArmas.estaEmPunhoPistol)
            {

                gerennciadorArmas.RequestFire();
            }
            else if (gerennciadorArmas.estaEmPunhoSword)
            {
                gerennciadorArmas.RequestSlash();
            }

        }

    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        healthBarPistol.fillAmount -= damage / 100;
        healthBarSword.fillAmount -= damage / 100;
        if (currentLife <= 0)
        {
            Dead();

        }
        else
        {
            isDead = false;
        }
    }

    public void RecoverLife(float recovery)
    {
        if (currentLife < 100)
        {
            currentLife += recovery;
            healthBarPistol.fillAmount += recovery / 100;
            healthBarSword.fillAmount += recovery / 100;

        }
    }

    void Dead()
    {
        isDead = true;
        SceneManager.LoadScene(2);
        Destroy(this.gameObject);
        Destroy(targetTransform.gameObject);
        Destroy(canvas.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Portal"))
        {
            newPortal = collider.GetComponent<LoadScene>();
            startPosition = newPortal.newPostionPortal;
            loadingScene = true;

        }
        else if (collider.CompareTag("PortalS") && rb.velocity.y > 0f)
        {
            newPortal = collider.GetComponent<LoadScene>();
            startPosition = newPortal.newPostionPortal;
            loadingScene = true;
            jumpTrigger = true;
            qtosPulos = 0;

        }
        else if (collider.CompareTag("PortalC") && possuiChave)
        {

            newPortal = collider.GetComponent<LoadScene>();
            startPosition = newPortal.newPostionPortal;
            win = true;
            loadingScene = true;

        }
        else if (collider.CompareTag("PortalC") && !possuiChave)
        {
            avisoPortaChave.SetBool("On", true);
        }
        else if (collider.CompareTag("PortalT"))
        {
            avisoPorta.SetActive(true);

        }
        else if (collider.CompareTag("Bullet"))
        {

            TakeDamage(Bullet.instace.damage);
            Destroy(collider.gameObject);

            var VFXRotation = new Quaternion();

            if (collider.transform.position.x < transform.position.x)
            {

                VFXRotation = new Quaternion(0, 180, 0, 0);

            }
            else
            {

                VFXRotation = new Quaternion(0, 0, 0, 0);

            }

            Instantiate(pistolVFX, targetBullets.transform.position, VFXRotation);


        }
        else if (collider.CompareTag("Stick"))
        {
            TakeDamage(SwordWeapon.instace.damage);

            var VFXRotation = new Quaternion();

            if (collider.transform.position.x < transform.position.x)
            {

                VFXRotation = new Quaternion(0, 180, 0, 0);

            }
            else
            {

                VFXRotation = new Quaternion(0, 0, 0, 0);

            }

            sm.PlaySlash();
            Instantiate(bladeVFX, targetBullets.transform.position, VFXRotation);
        }
        else if (collider.CompareTag("PickUp"))
        {
            RecoverLife(25);
        }

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Platarforma"))
        {
            naPlatarmorma = true;
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Platarforma"))
        {
            naPlatarmorma = !naPlatarmorma;
        }
        else if (collider.CompareTag("PortalT"))
        {
            avisoPorta.SetActive(false);
        }
        else if (collider.CompareTag("PortalC") && !possuiChave)
        {
            avisoPortaChave.SetBool("On", false);
        }

    }




}