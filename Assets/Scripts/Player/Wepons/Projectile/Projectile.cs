using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Settings")]
    public float speed = 10f;
    Rigidbody rb;
    private Vector3 speedProjectile =  Vector3.zero;
    private  Vector3 posDest;
    public static Projectile instace;
    public static Projectile Instace { get { return Instace; } }



    void Awake()
    {
        instace = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posDest = GerennciadorArmas.instance.player.target.localPosition;


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, posDest , ref speedProjectile, 1 / speed);
        Destroy(this.gameObject, 0.3f);

    }
}
