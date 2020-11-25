using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanKey : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("References")]
    public Transform pointSpwanKey;
    public GameObject key;
    void Start()
    {
        if (!GerennciadorArmas.instance.player.possuiChave)
        {
            Instantiate(key, pointSpwanKey.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
