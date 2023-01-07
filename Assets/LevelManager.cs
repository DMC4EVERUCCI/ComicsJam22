using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tommy;
using System.IO;
using Boxes = CanvasController.SingleTextBox;
using MultiBoxes = CanvasController.MultiTextBox;

public class LevelManager : MonoBehaviour
{

    // The .toml file which contains the information to populate the level
    // !!! ASSIGN THE CORRESPONDING ONE FOR EACH LEVEL
    public TextAsset levelSource;
    private TomlTable levelTable;

    private CanvasController canvas;

    public bool actionInProgress = false;
    public int pressedButton = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set canvas controller
        canvas = GameObject.Find("Canvas Controller").GetComponent<CanvasController>();

        // File reading works correctly
        string fileContents = levelSource.ToString();
        //Debug.Log(fileContents);

        // TOML table translation
        levelTable = TOML.Parse(new StringReader(fileContents));



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dialogue()
	{
        StartCoroutine(mainCoroutine());
    }

    // The flow of the level is handled here
    public IEnumerator mainCoroutine()
    {
        // Wait a frame to allow for initialization of other classes
        yield return null;

        actionInProgress = true;

        // Change battle title
        string name = levelTable["name"].ToString();
        canvas.printToTextBox(name, Boxes.Name);

        // Generate and print intro text
        List<string> introText = getStringList(levelTable["intro"]["introtext"].AsArray);
        yield return StartCoroutine(canvas.printDialogue(introText, MultiBoxes.DialogueBox));

        canvas.ShowChoices();
        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
        canvas.printToTextBox(levelTable["choice2"]["choicetext"].ToString(), Boxes.Choice2);


        // Generate initial BattleLog flavortext and titles of all boxes and actions
        //canvas.printToTextBox(levelTable["intro"]["flavor_text"].ToString(), Boxes.BattleLog);

        // This stores the successfully performed actions
        //HashSet<int> performedActions = new HashSet<int>();

        // This stores the enemy actions
        //TomlArray enemyMovesToml = levelTable["enemy"]["actions"].AsArray;
        //List<TomlNode> enemyMoves = getNodeList(enemyMovesToml);

        while (true)
        {

            if (pressedButton == 1)
            {
                List<string> choice1diag = getStringList(levelTable["choice1"]["choicediag"].AsArray);
                yield return StartCoroutine(canvas.printDialogue(choice1diag, MultiBoxes.DialogueBox));

                pressedButton = 0;
                canvas.HideChoices();
                canvas.EndDialogue();
            }

            if (pressedButton == 2)
            {
                List<string> choice2diag = getStringList(levelTable["choice2"]["choicediag"].AsArray);
                yield return StartCoroutine(canvas.printDialogue(choice2diag, MultiBoxes.DialogueBox));

                pressedButton = 0;
                canvas.HideChoices();
                canvas.EndDialogue();
            }

            // If a normal action was pressed
            //if (pressedButton > 0 && pressedButton <= 3)
            //{
            //    // Access the corresponding action TOML node
            //    TomlNode action = levelTable["player"][string.Concat("action", pressedButton)];
            //
            //    // Do we need a previous action to perform this?
            //    int requiredAction = 0;
            //    if (action.HasKey("requires")) requiredAction = action["requires"].AsInteger;
            //
            //    //print("Got so far");
            //
            //    // Check if the requirement is satisfied
            //    if (requiredAction == 0 || performedActions.Contains(requiredAction))
            //    {
            //        Debug.Log("Performing action" + pressedButton);
            //        List<string> description = getStringList(action["description_success"].AsArray);
            //        List<string> enemyReaction = getStringList(action["enemy_dialogue_success"].AsArray);
            //        yield return StartCoroutine(canvas.printDialogue(description, MultiBoxes.BattleLog));
            //        yield return StartCoroutine(canvas.printDialogue(enemyReaction, MultiBoxes.EnemyDialogue));
            //        performedActions.Add(pressedButton);
            //        canvas.setActionButton(pressedButton, false);
            //        if (hasAll3(performedActions)) canvas.setActionButton(4, true);
            //    }
            //    else
            //    {
            //        List<string> description = getStringList(action["description_failure"].AsArray);
            //        List<string> enemyReaction = getStringList(action["enemy_dialogue_failure"].AsArray);
            //        yield return StartCoroutine(canvas.printDialogue(description, MultiBoxes.BattleLog));
            //        yield return StartCoroutine(canvas.printDialogue(enemyReaction, MultiBoxes.EnemyDialogue));
            //    }
            //
            //    // End player turn, deactivate player outline & activate enemy outline
            //    andreiOutline.enabled = false;
            //    enemyOutline.enabled = true;
            //
            //    // A normal action was succeeded or failed, now it's the turn of the enemy
            //    // We remove the first action, to be appended back to the end of the list
            //    TomlNode enemyAction = enemyMoves[0];
            //    enemyMoves.RemoveAt(0);
            //
            //    //print(enemyAction);
            //
            //    // We get the text to be displayed...
            //    List<string> enemyDialogue = getStringList(enemyAction["dialogue"].AsArray);
            //    List<string> enemyMoveDescription = getStringList(enemyAction["description"].AsArray);
            //    yield return StartCoroutine(canvas.printDialogue(enemyDialogue, MultiBoxes.EnemyDialogue));
            //    yield return StartCoroutine(canvas.printDialogue(enemyMoveDescription, MultiBoxes.BattleLog));
            //
            //    // ... and finally we close the loop and put the old move at the back of the list, ready to be cycled
            //    enemyMoves.Add(enemyAction);
            //
            //}
            //// If we're in the ultimate zone, baby
            //if (pressedButton == 4)
            //{
            //    List<string> description = getStringList(levelTable["player"]["ultimate"]["description"].AsArray);
            //    List<string> enemyReaction = getStringList(levelTable["player"]["ultimate"]["enemy_dialogue"].AsArray);
            //    yield return StartCoroutine(canvas.printDialogue(description, MultiBoxes.BattleLog));
            //    yield return StartCoroutine(canvas.printDialogue(enemyReaction, MultiBoxes.EnemyDialogue));
            //    break;
            //}

            actionInProgress = false;

            // Reactivate player outline & deactivate enemy outline
            //pressedButton = 0;
            yield return null;
        }


        //Outro
        //if (outroTime == true)
        //{
        //    canvas.hideAllButBattleLog();
        //    endEnemy.enabled = true;
        //
        //    List<string> outroText = getStringList(levelTable["outro"]["text"].AsArray);
        //    yield return StartCoroutine(canvas.printDialogue(outroText, MultiBoxes.BattleLog));
        //    canvas.StopOST();
        //    SceneManager.LoadScene(NextScene);
        //}

        yield return null;
    }

    public void pressButton(int buttonNumber)
    {
        pressedButton = buttonNumber;
    }

    private List<string> getStringList(TomlArray arr)
    {
        List<string> ans = new List<string>();
        foreach (TomlString s in arr) ans.Add(s.ToString());
        return ans;
    }
    private List<TomlNode> getNodeList(TomlArray arr)
    {
        List<TomlNode> ans = new List<TomlNode>();
        foreach (TomlNode n in arr) ans.Add(n);
        return ans;
    }
}
