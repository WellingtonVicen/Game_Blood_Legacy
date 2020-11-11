using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("UI")]
    public Text bulletsText;
    public Text bullestsToReloadText;

    // Start is called before the first frame update

    void Awake()
    {
        instace = this;
        bulletsText.text = numeroBalas.ToString();
        bullestsToReloadText.text = balasReload.ToString();
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
            bulletsText.text = numeroBalas.ToString();
            //print(numeroBalas);
        }

    }

    public void Reload()
    {
        switch (numeroBalas)
        {
            case 7:
            //
                break;
            case 6:
                if (balasReload >= 1)
                {
                    balasReload--;
                    numeroBalas++;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 5:
                if (balasReload >= 2)
                {
                    balasReload -= 2;
                    numeroBalas += 2;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 4:
                if (balasReload >= 3)
                {
                    balasReload -= 3;
                    numeroBalas += 3;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 3:
                if (balasReload >= 4)
                {
                    balasReload -= 4;
                    numeroBalas += 4;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 2:
                if (balasReload >= 5)
                {
                    numeroBalas += 5;
                    balasReload -= balasReload;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 1:
                if (balasReload >= 6)
                {
                    balasReload -= 6;
                    numeroBalas += 6;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 0:
                if (balasReload >= 7)
                {
                    balasReload -= 7;
                    numeroBalas += 7;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    balasReload -= balasReload;
                    numeroBalas += balasReload;
                    bulletsText.text = numeroBalas.ToString();
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }

                break;
        }


    }
}




