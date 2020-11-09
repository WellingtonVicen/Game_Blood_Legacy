using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Settings")]
    public float speed = 0.5f;
    public float damage = 10f;
    Vector3 direction;
    private Vector3 speedProjectile = Vector3.up;



    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        direction = GerennciadorArmas.instance.player.transform.localPosition;
        Movement();
        Destroy(this.gameObject, 1f);
    }
    void FixedUpdate()
    {

    }

    void Movement()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.position, direction, ref speedProjectile, 1 / speed);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
