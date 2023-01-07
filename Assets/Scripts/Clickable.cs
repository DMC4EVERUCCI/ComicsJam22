using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{

    GameObject Function1;
    GameObject Function2;
    GameObject Function3;

    public UnityEvent OtherFunctions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        print("Calling!");
        CallOtherFunctions();
    }

    void CallOtherFunctions()
    {
        OtherFunctions.Invoke();
    }

}
