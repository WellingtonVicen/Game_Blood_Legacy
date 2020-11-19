using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource[] Steps;
    public AudioSource Slash;
    public AudioSource Shot;
    private int lastStep;

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



}
