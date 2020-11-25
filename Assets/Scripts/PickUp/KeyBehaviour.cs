using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{

    [Header("Settings")]
    public float xAngle;
    public float yAngle;
    public float zAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateKey();
    }

    public void RotateKey()
    {
        transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("ColiderPlayer"))
        {
            GerennciadorArmas.instance.player.possuiChave = true;
            Destroy(this.gameObject);
            

        }
    }
}
