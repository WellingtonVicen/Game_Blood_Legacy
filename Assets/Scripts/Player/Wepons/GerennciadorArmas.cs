using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerennciadorArmas : MonoBehaviour
{
    [Header("References")]
    public Player25D player;

    [Header("Transforms")]
    public Transform parentPistol;
    public Transform parentSword;

    [Header("Wepons")]
    public GameObject pistolWepon;
    public GameObject swordWepon;
    [Header("Setings")]
    public bool estaEmPunhoPistol;
    public bool estaEmPunhoSword;
    [Header("UI")]
    public GameObject hudPistol;
    public GameObject hudEspada;
    

    // Start is called before the first frame update

    public Animator HUDAnimator;

    public NaoDestrua naoDestrua;

    public static GerennciadorArmas instance;
    public static GerennciadorArmas Instance { get { return Instance; } }
    
    void Awake()
    {
        instance = this;
    }
    void FixedUpdate()
    {
        HUDAnimator.SetBool("Is Gun", estaEmPunhoPistol);
    }

    public void StartWepon()
    {
        swordWepon.SetActive(false);
        pistolWepon.SetActive(true);
        pistolWepon.transform.SetParent(parentPistol);
        pistolWepon.transform.position = parentPistol.position;
        estaEmPunhoPistol = true;
        HUDAnimator.SetBool("Is Gun", true);

    }

    public void WeaponSwitch()
    {
        if (estaEmPunhoPistol)
        {
            pistolWepon.SetActive(false);
            swordWepon.SetActive(true);
            estaEmPunhoPistol = !estaEmPunhoPistol;
            estaEmPunhoSword = true;
            swordWepon.transform.position = parentSword.position;
            swordWepon.transform.SetParent(parentSword);


        }
        else if (estaEmPunhoSword)
        {
            swordWepon.SetActive(false);
            pistolWepon.SetActive(true);
            estaEmPunhoSword = !estaEmPunhoSword;
            estaEmPunhoPistol = true;
            pistolWepon.transform.SetParent(parentPistol);
            pistolWepon.transform.position = parentPistol.position;

        }

    }

    public void RequestFire()
    {
        if (estaEmPunhoPistol)
        {

            Pistol.Instace.Fire();
        }
    }

    public void RequestReload()
    {
        if (estaEmPunhoPistol)
        {
            Pistol.Instace.Reload();
        }
    }

    /* public void RequestReloadPent()
    {
        Pistol.Instace.ReloadPent(3);
    } */

    public void RequestSlash()
    {
        if (estaEmPunhoSword)
        {
            Sword.Instace.Slash();
        }
    }


}
