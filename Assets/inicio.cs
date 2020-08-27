using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inicio : MonoBehaviour
{


    public GameObject player;
    // Start is called before the first frame update
   void Awake() 
   {
       //player = Instantiate(play,transformPlayer.position, transformPlayer.rotation);
       DontDestroyOnLoad(player);
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
