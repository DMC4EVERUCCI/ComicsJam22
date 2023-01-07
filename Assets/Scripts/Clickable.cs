using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{

    LevelManager LevelManager;

    public UnityEvent OtherFunctions;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        print("Calling!");
        if (LevelManager.actionInProgress == false)
		{
            CallOtherFunctions();
        }
    }

    void CallOtherFunctions()
    {
        OtherFunctions.Invoke();
    }

}
