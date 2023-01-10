using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    StudioEventEmitter OST;

    // Start is called before the first frame update
    void Start()
    {
        OST = GameObject.Find("START").GetComponent<StudioEventEmitter>();
        OST.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressStart()
	{
        OST.Stop();
        SceneManager.LoadScene(1);
	}

}
