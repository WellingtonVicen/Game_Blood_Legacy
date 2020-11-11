using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Singleton")]
    public static GameManager instace;
    public static GameManager Instace { get { return instace; } }
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
