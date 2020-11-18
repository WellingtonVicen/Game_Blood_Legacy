using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("References")]
    public Transform[] pointSpwanEnemy;
    public GameObject EnemyPistol;
    public GameObject EnemytSword;

    [HideInInspector]public int numberP;
    [HideInInspector]public int numberS;

    [Header("Singleton")]
    public static SpwanManager instace;
    public static SpwanManager Instance { get { return Instance; } }
    void Awake()
    {
        instace = this;

    }
    void Start()
    {
        SpwanEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpwanEnemy()
    {
        for (int i = 0; i < pointSpwanEnemy.Length; i++)
        {
            if (i % 2 == 0)
            {
                numberP = Random.Range(0, 10);
                Instantiate(EnemyPistol, pointSpwanEnemy[i].transform);
            }
            else
            {
                numberS = Random.Range(5, 10);
                Instantiate(EnemytSword, pointSpwanEnemy[i].transform);
            }
        }

    }


}
