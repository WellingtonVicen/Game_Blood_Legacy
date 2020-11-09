using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Settings")]
    public float speed = 20f;
    Rigidbody2D rb;
    private Vector3 speedProjectile = Vector3.up;
    private Vector3 posDest;

    public int damage = 10;
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
        posDest = GerennciadorArmas.instance.player.target.localPosition;


    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Destroy(this.gameObject, 1f);

    }

    void Movement()
    {
        transform.position = Vector3.SmoothDamp(transform.position, posDest, ref speedProjectile, 1 / speed);
    }



}
