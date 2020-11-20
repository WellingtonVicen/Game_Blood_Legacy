using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaoDestrua : MonoBehaviour
{

    public string Tag;
    // Start is called before the first frame update
    void Awake()
    {
        NotDestrua(Tag);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void NotDestrua(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }
}
