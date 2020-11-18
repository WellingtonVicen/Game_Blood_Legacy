using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverBehaviour : PickupBehaviour
{
    // Start is called before the first frame update

    [Header("Settings")]
    public float xAngle;
    public float yAngle;
    public float zAngle;
    void Start()
    {
        StartPickup();

    }

    // Update is called once per frame
    void Update()
    {
        RotateRecovery();
    }

    public void RotateRecovery()
    {
        transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("ColiderPlayer"))
        {
            Destroy(this.gameObject);

        }
    }


}
