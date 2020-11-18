using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : MonoBehaviour
{

    [Header("Settings")]
    public float damage = 0.1f;

    [Header("Sigleton")]
    public static SwordWeapon instace;
    public static SwordWeapon Instace { get { return Instace; } }
    // Start is called before the first frame update

    void Awake()
    {
        instace = this;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
