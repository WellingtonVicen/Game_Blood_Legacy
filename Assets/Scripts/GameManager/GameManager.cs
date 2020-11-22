using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Singleton")]
    public static GameManager instace;
    public static GameManager Instace { get { return instace; } }

    [Header("UI")]
    public GameObject canvas;
    void Awake()
    {
      instace = this;
    }

    // Update is called once per frame
    void Update()
    {
      //StateGame();
    }

    void StateGame()
    {
        if (GerennciadorArmas.instance.player.isDead)
        {

             SceneManager.LoadScene(1);
        }
    }
}
