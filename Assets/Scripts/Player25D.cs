using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player25D : MonoBehaviour
{
    private const float V = 0.5882102f;
    public float Axis;
    public float Vel;
    public float noChaoRaio;
    public float jumpForce;
    public bool estaAndando;
    private bool praTrasE;
    private bool praTrasD;
    public bool facingRight;
    public bool noChao;
    public bool slide;
    public float slideForce;

    private Transform _tr;
    public Transform groundCheckTransform;
    public LayerMask solid;
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
    public float valorPoint;
    Vector3 lookPos;
    private float contador;
    public int qtosPulos;

    public bool possuiHabilPulo;
    public float crouched;
    public bool crouch;
    public GameObject pistol;
    public Transform parentPistol;
    public GameObject espada;
    public Transform parentEspada;

    public bool estaEmPunhoArma;
    public bool estaEmPunhoEspada;




    private void Awake()
    {
        NaoDestrua("Player");
    }

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
        espada.SetActive(false);
        pistol.SetActive(true);
        estaEmPunhoArma = true;
        pistol.transform.position = parentPistol.position;
        pistol.transform.SetParent(parentPistol);


    }


    void Update()
    {

        noChao = Physics2D.OverlapCircle(groundCheckTransform.position, noChaoRaio, solid);

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
        TrocaArma();
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
        // print(target.localPosition);

        chest.rotation = chest.rotation * Quaternion.Euler(offset);
        hand.rotation = hand.rotation * Quaternion.Euler(offset);
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
            rb.velocity = new Vector2(Vel, rb.velocity.y);
        }
        else if (estaAndando && !facingRight && Axis < 0)
        {
            rb.velocity = new Vector2(-Vel, rb.velocity.y);
        }

    }

    void Tras()
    {
        if (praTrasD)
        {
            rb.velocity = new Vector2(Vel, rb.velocity.y);
        }
        else if (praTrasE)
        {
            rb.velocity = new Vector2(-Vel, rb.velocity.y);
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

    void Animations()
    {
        animator.SetBool("Walk", estaAndando);
        animator.SetBool("Jump", !noChao);
        animator.SetBool("WalkBack", praTrasE || praTrasD);
        animator.SetBool("JumpBack", praTrasE && !noChao || praTrasD && !noChao);
        animator.SetBool("Slide", slide);
        animator.SetBool("Crouch", crouch);
        animator.SetBool("Espada", estaEmPunhoEspada);
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

    public void NaoDestrua(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    void Crouch()
    {
        crouched = Input.GetAxis("Vertical");
        crouch = crouched < 0;

        if (crouch)
        {
            //capsule.height = 1.5f;
            //capsule.center = new Vector3(7.204121e-18f, 0.7241328f, 0.03175694f);
        }
        else
        {
            //capsule.height = 1.792002f;
            //capsule.center = new Vector3(7.204121e-18f, 0.82f, 0.03175694f);
        }
    }

    void TrocaArma()
    {
        if (Input.GetKeyDown(KeyCode.F) && estaEmPunhoArma)
        {
            pistol.SetActive(false);
            espada.SetActive(true);
            estaEmPunhoArma = !estaEmPunhoArma;
            estaEmPunhoEspada = true;
            espada.transform.position = parentEspada.position;
            espada.transform.SetParent(parentEspada);
        }
        else if (Input.GetKeyDown(KeyCode.F) && estaEmPunhoEspada)
        {
            espada.SetActive(false);
            pistol.SetActive(true);
            estaEmPunhoEspada = !estaEmPunhoEspada;
            estaEmPunhoArma = true;
            pistol.transform.position = parentPistol.position;
            pistol.transform.SetParent(parentPistol);

        }

    }

}