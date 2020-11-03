using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [Header("Settings")]
    public bool isSword;
    [Header("References")]
    public GameObject slashEffect;
    public Transform slashEffectTrasnform;
    private float contSlash;

    public static Sword instace;
    public static Sword Instace { get { return instace; } }

    // Start is called before the first frame update
    void Awake()
    {
        instace = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isSword)
        {
            contSlash += Time.deltaTime;
        }

        if (contSlash >= 1.2f)
        {
            isSword = !isSword;
            contSlash = 0;
        }

    }

    public void Slash()
    {
        if (!isSword)
        {
            isSword = true;
        }
    }



}
