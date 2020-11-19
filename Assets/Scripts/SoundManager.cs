using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource[] Steps, Shouts;
    public AudioSource Slash;
    public AudioSource Shot, Jumping, Falling, Reloading;
    private int lastStep, lastShout;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStep()
    {

        var random = 0;

        for (bool canPass = false; canPass == true;)
        {

            random = Random.Range(0, Steps.Length - 1);

            if (random != lastStep)
            {

                canPass = true;

            }

        }
        
        Steps[random].Play();
        lastStep = random;

    }

    public void PlaySlash()
    {

        Slash.Play();

    }

    public void PlayShot()
    {

        Shot.Play();

    }

    public void PlayJump()
    {
        var random = 0;

        for (bool canPass = false; canPass == true;)
        {

            random = Random.Range(0, Shouts.Length - 1);

            if (random != lastShout)
            {

                canPass = true;

            }

        }

        Shouts[random].Play();
        Jumping.Play();

    }

    public void PlayFalling()
    {

        Falling.Play();

    }

    public void PlayReloading()
    {

        Reloading.Play();

    }


}
