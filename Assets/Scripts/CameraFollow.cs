using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  [SerializeField]
  private float xMax;

[SerializeField]
 private float yMax;

[SerializeField]
 private float xMin;

 [SerializeField]
private float yMin;

private Transform target;

  private Player player;


  private void Awake() 
  {
     NaoDestrua("Camera");
  }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;

       
    }

    // Update is called once per frame
     private void LateUpdate() {
       transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin,xMax)
       , Mathf.Clamp(target.position.y, yMin,yMax), transform.position.z);
    }


    public void NaoDestrua(string tag)
      { 
          GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

          if(objs.Length > 1) 
           { 
            Destroy(this.gameObject);
          }
         DontDestroyOnLoad(this.gameObject);
   }

 
     
  }

