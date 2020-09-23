using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player25D : MonoBehaviour
{
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


    private Animator animator;
    private Rigidbody rb;

    private bool gravidade1;
    private bool gravidade2;
    private float gravitScale;

    public Transform target;
    public Vector3 offset;

    public Transform chest;
    Transform BD;
    Transform hips;
    public static Vector3 mousePosition;
    public float valorPoint;
    Vector3 lookPos;
    private float contador;
    public int qtosPulos;

    public bool possuiHabilPulo;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _tr = GetComponent<Transform>();
        facingRight = true;
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        slide = false;
       
    }


    void Update()
    {

        noChao = Physics.CheckSphere(groundCheckTransform.position, noChaoRaio, solid, QueryTriggerInteraction.Ignore);

        Axis = Input.GetAxis("Horizontal");
        estaAndando = Axis != 0;

        praTrasE = Axis < 0 && facingRight;
        praTrasD = Axis > 0 && !facingRight;

        Jump();
        ControleDeFisica();
        HandleRotation();
        controleSlide();
        Slide();

    }

    void FixedUpdate()
    {
        // rb.velocity = new Vector3(inputMovement * walkSpeed, rb.velocity.y,0 ); 2.5D
        Walk();
        Animations();
        HandlePos();
        Tras();

    }

    private void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        target.localPosition = ray.GetPoint(valorPoint);

        if (target.localPosition.y <= 4f && target.localPosition.y >= -2.5f)
        {
            chest.LookAt(target.localPosition);
        }

        print(target.localPosition);

        chest.rotation = chest.rotation * Quaternion.Euler(offset);
    }

    void HandlePos()
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

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);

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
        if (Input.GetButtonDown("Jump") && noChao)
        {
            rb.AddForce(_tr.up * jumpForce, ForceMode.Impulse);
            qtosPulos++;
        }
        else if (Input.GetButtonDown("Jump") && !noChao && qtosPulos <= 1 && possuiHabilPulo)
        {
            qtosPulos++;
            rb.AddForce(_tr.up * jumpForce, ForceMode.Impulse);

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
            Physics.gravity = new Vector3(0, -15, 0);
        }
        else if (gravidade2)
        {
            Physics.gravity = new Vector3(0, -30, 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
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
    }

    void Slide()
    {
        if (Input.GetKeyDown(KeyCode.C) && facingRight && !slide)
        {
            rb.AddForce(new Vector2(slideForce, 0), ForceMode.Impulse);
            slide = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && !facingRight && !slide)
        {
            rb.AddForce(new Vector2(-slideForce, 0), ForceMode.Impulse);
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











}
