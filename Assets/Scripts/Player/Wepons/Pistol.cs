using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{

    [Header("Settings")]
    public int numeroBalas = 6;
    public int balasReload = 6;

    [Header("References")]
    public Transform projectileExit;
    public GameObject projectile;

    public static Pistol instace;
    public static Pistol Instace { get { return instace; } }

    [Header("UI")]
    public Text bulletsText;
    public Text bullestsToReloadText;

    public Image bulletsImage;
    public float FillAmountValue;


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

    private void FixedUpdate()
    {
        FillAmount();
    }

    public void Fire()
    {
        if (numeroBalas > 0)
        {
            Instantiate(projectile, projectileExit.position, Quaternion.identity);
            numeroBalas--;
            bulletsImage.fillAmount -= 0.166f;
            /* bulletsText.text = numeroBalas.ToString(); */
            //print(numeroBalas);
        }

    }

    public void FillAmount()
    {
        switch (balasReload)
        {
            case 6:
                FillAmountValue = 1f;
                print(FillAmountValue);
                break;
            case 5:
                FillAmountValue = 0.83f;
                print(FillAmountValue);
                break;
            case 4:
                FillAmountValue = 0.664f;
                print(FillAmountValue);
                break;
            case 3:
                FillAmountValue = 0.498f;
                print(FillAmountValue);
                break;
            case 2:
                FillAmountValue = 0.322f;
                print(FillAmountValue);
                break;
            case 1:
                FillAmountValue = 0.166f;
                print(FillAmountValue);
                break;
            case 0:
                FillAmountValue = 0f;
                print(FillAmountValue);
                break;
            default:
                FillAmountValue = 1f;
                print(FillAmountValue);
                break;

        }

    }


    public void Reload()
    {
        switch (numeroBalas)
        {

            case 6:

                break;
            case 5:
                if (balasReload >= 1)
                {
                    balasReload -= 1;
                    numeroBalas += 1;
                    bulletsImage.fillAmount += 0.166f;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    /* bulletsText.text = numeroBalas.ToString(); */
                    bulletsImage.fillAmount += FillAmountValue;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 4:
                if (balasReload >= 2)
                {
                    balasReload -= 2;
                    numeroBalas += 2;
                    bulletsImage.fillAmount += 0.322f;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    /* bulletsText.text = numeroBalas.ToString(); */
                    bulletsImage.fillAmount += FillAmountValue;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 3:
                if (balasReload >= 3)
                {
                    balasReload -= 3;
                    numeroBalas += 3;
                    /*  bulletsText.text = numeroBalas.ToString(); */
                    bulletsImage.fillAmount += 0.498f;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    /*  bulletsText.text = numeroBalas.ToString(); */
                    bulletsImage.fillAmount += FillAmountValue;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 2:
                if (balasReload >= 4)
                {
                    numeroBalas += 4;
                    balasReload -= balasReload;
                    /* bulletsText.text = numeroBalas.ToString(); */
                    bulletsImage.fillAmount += 0.664f;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    /* bulletsText.text = numeroBalas.ToString(); */
                    bulletsImage.fillAmount += FillAmountValue;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 1:
                if (balasReload >= 5)
                {
                    balasReload -= 5;
                    numeroBalas += 5;
                    /* bulletsText.text = numeroBalas.ToString(); */
                    bulletsImage.fillAmount += 0.83f;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    numeroBalas += balasReload;
                    balasReload -= balasReload;
                    /*    bulletsText.text = numeroBalas.ToString(); */
                    bulletsImage.fillAmount += FillAmountValue;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                break;
            case 0:
                if (balasReload >= 6)
                {
                    numeroBalas += 6;
                    balasReload -= 6;
                    /*  bulletsText.text = numeroBalas.ToString(); */
                    bulletsImage.fillAmount += 1f;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }
                else if (balasReload > 0)
                {
                    balasReload -= balasReload;
                    numeroBalas += balasReload;
                    /* bulletsText.text = n umeroBalas.ToString(); */
                    bulletsImage.fillAmount += FillAmountValue;
                    bullestsToReloadText.text = balasReload.ToString();
                    break;
                }

                break;
        }


    }
}






