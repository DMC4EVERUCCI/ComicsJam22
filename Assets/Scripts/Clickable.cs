using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{

    LevelManager LevelManager;

    public UnityEvent OtherFunctions;

    [SerializeField] SpriteRenderer lettera;
    [SerializeField] SpriteRenderer seme;
    [SerializeField] SpriteRenderer dente;
    [SerializeField] SpriteRenderer sacco;

    bool leaderDone = false;
    bool ItemsClosed = true;

    SpriteRenderer casella;
    GameObject casellaObject;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        //casella = GameObject.Find("CASELLA ITEMS").GetComponent<SpriteRenderer>();
        casellaObject = GameObject.Find("CASELLA ITEMS");
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

            if (gameObject.name == "dente")
            {
                dente.enabled = true;
                Destroy(gameObject);
            }
            if (gameObject.name == "sacco")
			{
                sacco.enabled = true;
                Destroy(gameObject);
			}
            if (gameObject.name == "lettera")
			{
                lettera.enabled = true;
                Destroy(gameObject);
            }
            if (gameObject.name == "seme")
            {
                seme.enabled = true;
                Destroy(gameObject);
            }
        }

        if (gameObject.name == "ZAINO")
        {
            ItemsClosed = false;
            casellaObject.SetActive(true);
            casellaObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (gameObject.name == "ZAINOCLOSE" && ItemsClosed == false)
        {
            print("OH MAMA");
            casellaObject.GetComponent<SpriteRenderer>().enabled = true;
            casellaObject.SetActive(false);
            ItemsClosed = true;
        }



    }

    void CallOtherFunctions()
    {
        OtherFunctions.Invoke();
    }

}
