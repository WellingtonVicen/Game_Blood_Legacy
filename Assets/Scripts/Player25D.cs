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
  public bool facingRight;
  public bool noChao;

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


   void Start() 
    {
        animator = GetComponent<Animator>();
         rb = GetComponent<Rigidbody>();
         _tr = GetComponent<Transform>();
         facingRight = true;
         chest = animator.GetBoneTransform(HumanBodyBones.Chest);
    }


    void Update()
    {

         noChao = Physics.CheckSphere(groundCheckTransform.position, noChaoRaio, solid,  QueryTriggerInteraction.Ignore);

       Axis = Input.GetAxis("Horizontal");
       estaAndando = Axis != 0;

       //PermiteFlip();
       Jump();
       ControleDeFisica();
       HandleRotation();
       


    }

    void FixedUpdate() 
    {
      // rb.velocity = new Vector3(inputMovement * walkSpeed, rb.velocity.y,0 ); 2.5D
       Walk();
       Animations();
       HandlePos();
      
    }

    private void LateUpdate() 
    { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        target.localPosition = ray.GetPoint(valorPoint);
          chest.LookAt(target.localPosition);

         chest.rotation = chest.rotation * Quaternion.Euler(offset);
    }

    void HandlePos()
    {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         
         
         RaycastHit hit;
         if(Physics.Raycast(ray, out hit, Mathf.Infinity))
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
          
         Debug.Log(targetRotation);

         if(targetRotation.y < 0.5f )
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
        if(estaAndando && facingRight && Axis > 0)
        {
          rb.velocity = new Vector2(Vel,rb.velocity.y);
        }
        else if (estaAndando && !facingRight && Axis < 0)
        {
            rb.velocity = new Vector2(-Vel,rb.velocity.y);
        }
         
    }

    void Flip()
    { 
        facingRight = !facingRight;
         _tr.localScale = new Vector3(-_tr.localScale.x, _tr.localScale.y, -_tr.localScale.z);
    } 

  


    
    void PermiteFlip() 
    { 
        if(Axis > 0 && !facingRight)
        {
            Flip();
        }
        else if(Axis < 0 && facingRight) 
        { 
            Flip();
        }

    }

      private void OnDrawGizmosSelected()
      {
          Gizmos.color = Color.cyan;
          Gizmos.DrawWireSphere(groundCheckTransform.position, noChaoRaio);
        
      }

      public void Jump()
      {
          if(Input.GetButtonDown("Jump") && noChao) 
          { 
              rb.AddForce(_tr.up *  jumpForce, ForceMode.Impulse);
          }
      }

      public void ControleDeFisica()
      { 
          if(gravidade1) 
          { 
              Physics.gravity = new Vector3(0, -15, 0);
          }
          else if ( gravidade2) 
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
      }

    




  
   
}
