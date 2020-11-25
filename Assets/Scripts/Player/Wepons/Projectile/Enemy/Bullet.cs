using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Settings")]
    public float speed = 0.5f;
    public float damage = 2f;
    Vector3 direction;
    private Vector3 speedProjectile = Vector3.zero;

    [Header("Singleton")]
    public static Bullet instace;
    public static Bullet Instace { get { return Instace; } }

    void Awake()
    {
        instace = this;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Destroy(this.gameObject, 1f);
    }
    void FixedUpdate()
    {
        direction = GerennciadorArmas.instance.player.targetBullets.position;

    }

    void Movement()
    {

        // transform.Translate(direction * speed * Time.deltaTime);
        transform.localPosition = Vector3.SmoothDamp(transform.position, direction, ref speedProjectile, 1 / speed);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Destroy(gameObject);
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(gameObject);
      
    }

}
