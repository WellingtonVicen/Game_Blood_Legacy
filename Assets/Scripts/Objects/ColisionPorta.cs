using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionPorta : MonoBehaviour
{

   public BoxCollider2D colider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Player25D.possuiChave)
        {
            colider.isTrigger = true;
        }
        else { 
            colider.isTrigger = false;
        }

    }
}
