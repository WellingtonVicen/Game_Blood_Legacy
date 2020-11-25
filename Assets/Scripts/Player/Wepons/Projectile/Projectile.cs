using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Settings")]
    public float speed = 10f;

    public int damage = 10;

    private int firedByLayer;
    Rigidbody2D rb;
    private Vector3 speedProjectile = Vector3.up;
    private Vector3 posDest;
    public static Projectile instace;
    public static Projectile Instace { get { return Instace; } }



    void Awake()
    {
        instace = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Destroy(gameObject, 2f);

    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 2f);
    }

    void Movement()
    {
        posDest = GerennciadorArmas.instance.player.target.localPosition;
        transform.position = Vector3.SmoothDamp(transform.position, posDest, ref speedProjectile, 1 / speed);
    }

   /*  private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        print("Pegou");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
          Destroy(gameObject);
        print("Pegou");
    } */

    private void OnTriggerExit2D(Collider2D other)
    {
         Destroy(gameObject);
        print("Pegou");
    }








}
