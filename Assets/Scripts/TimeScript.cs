using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public bool mattina;
    public bool pome;
    public bool sera;
    public bool notte;

    public int phaseNum;

    public int day;

    // Start is called before the first frame update
    void Start()
    {
        MattinaTime();
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
