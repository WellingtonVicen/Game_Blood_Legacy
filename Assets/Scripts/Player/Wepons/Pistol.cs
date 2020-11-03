using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{

    [Header("Settings")]
    public int numeroBalas = 7;
    public int balasReload = 7;

    [Header("References")]
    public Transform projectileExit;
    public GameObject projectile;

    public static Pistol instace;
    public static Pistol Instace { get { return instace; } }

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

    }

    public void Fire()
    {
        if (numeroBalas > 0)
        {
            Instantiate(projectile, projectileExit.position, Quaternion.identity);
            numeroBalas--;
            print(numeroBalas);
        }

    }

    public void Reload()
    {
        switch (numeroBalas)
        {
            case 7:
                print("Pistola Totalmente Carregada");
                break;
            case 6:
                if (balasReload >= 1)
                {
                    print("Recarege 1");
                    balasReload--;
                    numeroBalas++;
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    break;
                }
                break;
            case 5:
                if (balasReload >= 2)
                {
                    print("Recarege 2");
                    balasReload -= 2;
                    numeroBalas += 2;
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    break;
                }
                break;
            case 4:
                if (balasReload >= 3)
                {
                    print("Recarege 3");
                    balasReload -= 3;
                    numeroBalas += 3;
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    break;
                }
                break;
            case 3:
                if (balasReload >= 4)
                {
                    print("Recarege 4");
                    balasReload -= 4;
                    numeroBalas += 4;
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    break;
                }
                break;
            case 2:
                if (balasReload >= 5)
                {
                    print("Recarege 5");
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    break;
                }
                break;
            case 1:
                if (balasReload >= 6)
                {
                    print("Recarege 6");
                    balasReload -= 6;
                    numeroBalas += 6;
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    break;
                }
                break;
            case 0:
                if (balasReload >= 7)
                {
                    print("Recarege 7");
                    balasReload -= 7;
                    numeroBalas += 7;
                    break;
                }
                else if (balasReload > 0)
                {
                    balasReload -= balasReload;
                    numeroBalas += balasReload;
                    break;
                }

                break;
        }


    }
}




