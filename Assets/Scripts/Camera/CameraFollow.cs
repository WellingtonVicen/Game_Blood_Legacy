using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMax;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float yMin;

    private Transform target;
    /* public Player25D player; */


    private void Awake()
    {

        target = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    private void LateUpdate()
    {



        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax)
        , Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);


    }





}

