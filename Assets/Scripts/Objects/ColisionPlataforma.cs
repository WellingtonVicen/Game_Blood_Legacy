using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionPlataforma : MonoBehaviour
{
    // Start is called before the first frame update


    public BoxCollider2D collision;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Player25D.naPlatarmorma && Player25D.crouch)
        {
            collision.isTrigger = true;
        }
        else
        {
           collision.isTrigger = false;
        }

    }
}
