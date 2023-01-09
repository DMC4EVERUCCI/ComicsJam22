using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpigaManager : MonoBehaviour
{

    LevelManager LevelManager;
    CanvasController CanvasController;

    int SpigaPG1 = 3;
    int SpigaPG2 = 2;
    int SpigaPG3 = 2;
    int SpigaPG4 = 4;
    int SpigaPG5 = 5;

    int currentSpiga;
    int currentPG;

    [SerializeField] SpriteRenderer Step1;
    [SerializeField] SpriteRenderer Step2;
    [SerializeField] SpriteRenderer Step3;
    [SerializeField] SpriteRenderer Step4;
    [SerializeField] SpriteRenderer Step5;
    [SerializeField] SpriteRenderer Step6;
    [SerializeField] SpriteRenderer Step7;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        CanvasController = GameObject.Find("Canvas Controller").GetComponent<CanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPG = CanvasController.currentPG;

        if (currentSpiga < 0)
		{
            currentSpiga = 0;
		}

        switch (currentSpiga)
		{
            case 0:
                Step1.enabled = false;
                Step2.enabled = false;
                Step3.enabled = false;
                Step4.enabled = false;
                Step5.enabled = false;
                Step6.enabled = false;
                Step7.enabled = false;
                break;
            case 1:
                Step1.enabled = true;
                Step2.enabled = false;
                Step3.enabled = false;
                Step4.enabled = false;
                Step5.enabled = false;
                Step6.enabled = false;
                Step7.enabled = false;
                break;
            case 2:
                Step1.enabled = true;
                Step2.enabled = true;
                Step3.enabled = false;
                Step4.enabled = false;
                Step5.enabled = false;
                Step6.enabled = false;
                Step7.enabled = false;
                break;
            case 3:
                Step1.enabled = true;
                Step2.enabled = true;
                Step3.enabled = true;
                Step4.enabled = false;
                Step5.enabled = false;
                Step6.enabled = false;
                Step7.enabled = false;
                break;
            case 4:
                Step1.enabled = true;
                Step2.enabled = true;
                Step3.enabled = true;
                Step4.enabled = true;
                Step5.enabled = false;
                Step6.enabled = false;
                Step7.enabled = false;
                break;
            case 5:
                Step1.enabled = true;
                Step2.enabled = true;
                Step3.enabled = true;
                Step4.enabled = true;
                Step5.enabled = true;
                Step6.enabled = false;
                Step7.enabled = false;
                break;
            case 6:
                Step1.enabled = true;
                Step2.enabled = true;
                Step3.enabled = true;
                Step4.enabled = true;
                Step5.enabled = true;
                Step6.enabled = true;
                Step7.enabled = false;
                break;
            case 7:
                Step1.enabled = true;
                Step2.enabled = true;
                Step3.enabled = true;
                Step4.enabled = true;
                Step5.enabled = true;
                Step6.enabled = true;
                Step7.enabled = true;
                LevelManager.SpigaWin(currentPG);
                break;
        }
    }

    public void SpigaUpdate()
    {
        switch (currentPG)
        {
            case 1:
                SpigaPG1 = currentSpiga;
                break;
            case 2:
                SpigaPG2 = currentSpiga;
                break;
            case 3:
                SpigaPG3 = currentSpiga;
                break;
            case 4:
                SpigaPG4 = currentSpiga;
                break;
            case 5:
                SpigaPG5 = currentSpiga;
                break;
        }
    }

    public void SpigaFind(int i)
	{
        print("FINDINGGGG!");
        switch (i)
		{
            case 1:
                currentSpiga = SpigaPG1;
                break;
            case 2:
                currentSpiga = SpigaPG2;
                break;
            case 3:
                currentSpiga = SpigaPG3;
                break;
            case 4:
                currentSpiga = SpigaPG4;
                break;
            case 5:
                currentSpiga = SpigaPG5;
                break;

        }
	}

    public void SpigaChange(int i)
	{
        currentSpiga += i;
    }

}
