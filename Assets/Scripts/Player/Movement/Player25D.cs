using System.Collections;
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
    public float crouched;
    public static bool crouch;
    private string sceneName;
    [HideInInspector] public Vector3 startPosition;
    public static bool naPlatarmorma;
    public static bool possuiChave;
    public Transform targetBullets;
    public GerennciadorArmas gerennciadorArmas;
    [Header("UI")]
    public Image healthBarPistol;
    public Image healthBarSword;

    public static Player25D instace;
    public static Player25D Instace { get { return Instace; } }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
        //capsule = GetComponent<CapsuleCollider>();
        facingRight = true;
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        hand = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
        slide = false;
        //possuiChave = true;    para poder acessar a salas Vermelhas
        gerennciadorArmas.StartWepon();
    }


    void Update()
    {

        noChao = Physics2D.OverlapCircle(groundCheckTransform.position, noChaoRaio, solid);
        // naPlatarmorma = Physics2D.OverlapCircle(groundCheckTransform.position,noChaoRaio);
        Axis = Input.GetAxis("Horizontal");
        estaAndando = Axis != 0;
        praTrasE = Axis < 0 && facingRight;
        praTrasD = Axis > 0 && !facingRight;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        target.localPosition = ray.GetPoint(valorPoint);

        chest.LookAt(new Vector2(target.position.x, target.position.y));
        //hand.LookAt(target.localPosition);
        //print(target.localPosition);
        Pistol.instace.projectileExit.LookAt(new Vector2(target.position.x, target.position.y));

        chest.rotation = chest.rotation * Quaternion.Euler(offset);
        hand.rotation = hand.rotation * Quaternion.Euler(offset);
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
        if (estaAndando && facingRight && Axis > 0)
        {
            rb.velocity = new Vector2(sensitivity, rb.velocity.y);
        }
        else if (estaAndando && !facingRight && Axis < 0)
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
        if (Input.GetButtonDown("Jump") && noChao && !crouch)
        {
            rb.AddForce(_tr.up * jumpForce, ForceMode2D.Impulse);
            qtosPulos++;
        }
        else if (Input.GetButtonDown("Jump") && !noChao && qtosPulos <= 1 && possuiHabilPulo && !crouch)
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
        if (Input.GetKeyDown(KeyCode.C) && facingRight && !slide && !crouch)
        {
            rb.AddForce(new Vector2(slideForce, 0), ForceMode2D.Impulse);
            slide = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && !facingRight && !slide && !crouch)
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
            print("Morreu");

        }
    }

    public void RecoverLife(float recovery)
    {
        if (currentLife <= 100)
        {
            currentLife += recovery;
            healthBarPistol.fillAmount += recovery / 100;
            healthBarSword.fillAmount += recovery / 100;

        }
    }

    void Dead()
    {

        Destroy(this.gameObject, 1.5f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Portal"))
        {
            var newPortal = collider.GetComponent<LoadScene>();
            SceneManager.LoadScene(newPortal.sceneName);
            startPosition = newPortal.newPostionPortal;
            gameObject.transform.position = startPosition;

        }
        else if (collider.CompareTag("PortalS") && rb.velocity.y > 0f)
        {
            var newPortal = collider.GetComponent<LoadScene>();
            SceneManager.LoadScene(newPortal.sceneName);
            startPosition = newPortal.newPostionPortal;
            gameObject.transform.position = startPosition;
            qtosPulos = 0;

        }
        else if (collider.CompareTag("PortalC") && possuiChave)
        {
            var newPortal = collider.GetComponent<LoadScene>();
            SceneManager.LoadScene(newPortal.sceneName);
            startPosition = newPortal.newPostionPortal;
            gameObject.transform.position = startPosition;
        }
        else if (collider.CompareTag("Bullet"))
        {
            TakeDamage(Bullet.instace.damage);
        }
        else if (collider.CompareTag("Stick"))
        {
            TakeDamage(SwordWeapon.instace.damage);
        }
        else if (collider.CompareTag("PickUp"))
        {
            RecoverLife(2);
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

    }


}