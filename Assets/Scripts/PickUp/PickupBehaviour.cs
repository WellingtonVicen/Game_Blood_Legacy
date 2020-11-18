using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] PickupsType PickupsType;

    [HideInInspector] public float recovery = 0;
    [HideInInspector] public int bullets = 0;

  
    // Start is called before the first frame update


    void Start()
    {

    }
    protected void StartPickup()
    {
        switch (PickupsType)
        {
            case PickupsType.RECOVERY:
                recovery = 25f;
                break;
            case PickupsType.BULLET:
                bullets = 3;
                break;
            default:
                recovery = 0;
                bullets = 0;
                break;

        }

    }


    // Update is called once per frame
    void Update()
    {
    }
}
