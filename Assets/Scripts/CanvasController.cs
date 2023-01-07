using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    GameObject TimeManager;

    SpriteRenderer Mappa;
    SpriteRenderer BGRPG1;
    SpriteRenderer BGRPG2;

    GameObject DiagContainer;

    [SerializeField] SpriteRenderer EscIcon;
    [SerializeField] SpriteRenderer PG1;
    [SerializeField] SpriteRenderer PG2;
    [SerializeField] SpriteRenderer MapIcon1;
    [SerializeField] SpriteRenderer MapIcon2;


    string Phase;

    // Start is called before the first frame update
    void Start()
    {
        TimeManager = GameObject.Find("Time Manager");
        DiagContainer = GameObject.Find("DiagContainer");
        DiagContainer.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        

        if (TimeManager.GetComponent<TimeScript>().mattina == true)
        {
            Phase = "MATTINA";
        }
        else if (TimeManager.GetComponent<TimeScript>().pome == true)
        {
            Phase = "POME";
        }
        else if (TimeManager.GetComponent<TimeScript>().sera == true)
		{
            Phase = "SERA";
		}
        else if (TimeManager.GetComponent<TimeScript>().notte == true)
        {
            Phase = "NOTTE";
        }


        Mappa = GameObject.Find("MAPPA"+Phase).GetComponent<SpriteRenderer>();
        BGRPG1 = GameObject.Find("BGRPG1").GetComponent<SpriteRenderer>();
        BGRPG2 = GameObject.Find("BGRPG2").GetComponent<SpriteRenderer>();

    }


    public void HideAll()
	{
        Mappa.enabled = false;
        BGRPG1.enabled = false;
        BGRPG2.enabled = false;
        PG1.enabled = false;
        PG2.enabled = false;
        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        EscIcon.enabled = false;
    }

    public void ShowMap()
    {
        Mappa.enabled = true;
        BGRPG1.enabled = false;
        BGRPG2.enabled = false;
        PG1.enabled = false;
        PG2.enabled = false;
        MapIcon1.enabled = true;
        MapIcon2.enabled = true;
        EscIcon.enabled = false;
    }

    public void ShowPG1()
    {
        Mappa.enabled = false;
        BGRPG1.enabled = true;
        BGRPG2.enabled = false;
        PG1.enabled = true;
        PG2.enabled = false;
        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        EscIcon.enabled = true;
    }

    public void ShowPG2()
    {
        Mappa.enabled = false;
        BGRPG1.enabled = false;
        BGRPG2.enabled = true;
        PG1.enabled = false;
        PG2.enabled = true;
        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        EscIcon.enabled = true;
    }

    public void StartDialogue()
	{
        DiagContainer.SetActive(true);
        TimeManager.GetComponent<TimeScript>().AdvanceTime(1);

	}

    public void EndDialogue()
	{
        DiagContainer.SetActive(false);
    }

}
