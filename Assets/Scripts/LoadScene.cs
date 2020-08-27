using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
  
     private int taEmQual;

    // Start is called before the first frame update
    void Awake() {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.tag == "Player" && taEmQual == 0) 
        {
            SceneManager.LoadScene(1);
            taEmQual++;
        }
        else if(collider.tag == "Player" && taEmQual == 1)
        { 
               SceneManager.LoadScene(0);
                taEmQual--;
        }
      
    }
}
