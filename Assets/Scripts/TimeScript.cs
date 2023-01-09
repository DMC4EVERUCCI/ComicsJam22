using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public bool mattina;
    public bool pome;
    public bool sera;
    public bool notte;

    [SerializeField] SpriteRenderer mattinaIcon;
    [SerializeField] SpriteRenderer pomeIcon;
    [SerializeField] SpriteRenderer seraIcon;
    [SerializeField] SpriteRenderer notteIcon;

    public int phaseNum;

    public int day;

    LevelManager LevelManager;

    // Start is called before the first frame update
    void Start()
    {
        NotteTime();

        LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvanceTime(int number)
	{
        number += phaseNum;
        switch (number)
		{
            case 1:
                MattinaTime();
                print("WAKE UP DARLING");
                break;
            case 2:
                PomeTime();
                print("YO JOSUKE");
                break;
            case 3:
                SeraTime();
                print("MOMENTO GAMING");
                break;
            case 4:
                NotteTime();
                print("SLEEPYYY");
                break;
        }

        phaseNum++;
        if (phaseNum == 4)
		{
            phaseNum = 0;
            day++;
		}
    }

    public void WheelUpdate()
	{
        if (mattina == true)
        {

            mattinaIcon.enabled = true;
            pomeIcon.enabled = false;
            seraIcon.enabled = false;
            notteIcon.enabled = false;
        }
        else if (pome == true)
        {

            mattinaIcon.enabled = false;
            pomeIcon.enabled = true;
            seraIcon.enabled = false;
            notteIcon.enabled = false;
        }
        else if (sera == true)
        {

            mattinaIcon.enabled = false;
            pomeIcon.enabled = false;
            seraIcon.enabled = true;
            notteIcon.enabled = false;
        }
        else if (notte == true)
        {

            mattinaIcon.enabled = false;
            pomeIcon.enabled = false;
            seraIcon.enabled = false;
            notteIcon.enabled = true;

            
        }
    }

    public void MattinaTime()
    {
        mattina = true;
        pome = false;
        sera = false;
        notte = false;
    }

    public void PomeTime()
    {

        mattina = false;
        pome = true;
        sera = false;
        notte = false;

    }

    public void SeraTime()
	{

        mattina = false;
        pome = false;
        sera = true;
        notte = false;

    }

    public void NotteTime()
    {

        mattina = false;
        pome = false;
        sera = false;
        notte = true;

    }



}
