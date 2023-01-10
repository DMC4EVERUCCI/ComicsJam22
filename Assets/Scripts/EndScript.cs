using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EndScript : MonoBehaviour
{

    StudioEventEmitter OST;

    // Start is called before the first frame update
    void Start()
    {
        OST = GameObject.Find("OST").GetComponent<StudioEventEmitter>();

        OST.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
            Application.Quit();
		}
    }
}
