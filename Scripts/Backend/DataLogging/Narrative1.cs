//This script logs any relevant data during the scene called "Narrative1" (i.e., the first tutorial)

using UnityEngine;

public class Narrative1 : MonoBehaviour
{
    public string EVENTNAME;
    public string RESPONSENAME;
    public string BLOCKNAME;
    public string CARSPEED;
    public string TRIAL;

    public DBConnect dbconnect;
    public TutorialCarCycle tutorialcarcycle;
    public TutorialScoreKeeper tutorialscorekeeper;
    public AdvanceScenes advancescenes;
    public Dialogue dialogue;


    private void Start()
    {
        //registers current scene for data logging
        advancescenes.CurrentBlock();
        BLOCKNAME = advancescenes.block;
        EVENTNAME = "Start New Block";
        dbconnect.Insert();

        //uses button as tricgger to add estimated speed to the data file
        tutorialcarcycle.enterMPH.onClick.AddListener(GetMPH);

        //uses button as tricgger to add see if subs need to repeat instructions
        dialogue.repeatInstructions.onClick.AddListener(RepeatInstructions);
        //uses button as tricgger to add see if subs need to repeat instructions
        dialogue.imReady.onClick.AddListener(DoNotRepeatInstructions);

    }

    void Update()
    {   //updates the static variables in CombinedData script
        CombinedData.BLOCKNAME = BLOCKNAME;
        CombinedData.CARSPEED = CARSPEED;
        CombinedData.TRIAL = "Tutorial Instructions 1";
        CombinedData.EVENTNAME = EVENTNAME;
        CombinedData.RESPONSENAME = RESPONSENAME;


        //updates data if subjects press SPACE to wave down cars
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CARSPEED = tutorialcarcycle.speed.ToString();
            EVENTNAME = "Space Pressed";
            RESPONSENAME = "Space Pressed";
            dbconnect.Insert();
        }
    }

    void GetMPH()
    {   //updates data if subjects submit their estimated mph speed
        CARSPEED = tutorialcarcycle.speed.ToString();
        EVENTNAME = "Estimated Speed";
        RESPONSENAME = tutorialcarcycle.mphInputField.text;
        dbconnect.Insert();
    }

    void RepeatInstructions()
    {   //logs if player needed to repeat the instructions
        EVENTNAME = "Repeat Instructions";
        RESPONSENAME = "Yes";
        dbconnect.Insert();
    }
    void DoNotRepeatInstructions()
    {   //logs if player is ready to continue
        EVENTNAME = "Repeat Instructions";
        RESPONSENAME = "No";
        dbconnect.Insert();
    }

}
