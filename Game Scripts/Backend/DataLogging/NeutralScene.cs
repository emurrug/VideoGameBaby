//This script is to record data during the block where only a car is looping (without a baby)
//See "CombinedData" script for more details

using UnityEngine;

public class NeutralScene : MonoBehaviour
{
    public string EVENTNAME;
    public string RESPONSENAME;
    public string BLOCKNAME;
    public string CARSPEED;
    public string TRIAL;

    public DBConnect dbconnect;
    public ScoreKeeper scorekeeper;
    public CarCycle carcycle;
    public AdvanceScenes advancescenes;


    private void Start()
    {
        //registers current scene for data logging
        advancescenes.CurrentBlock();
        BLOCKNAME = advancescenes.block;
        EVENTNAME = "Start New Block";
        dbconnect.Insert();

        //uses button as tricgger to add estimated speed to the data file
        carcycle.enterMPH.onClick.AddListener(GetMPH);

    }

    void Update()
    {   //updates the static variables in CombinedData script
        CombinedData.BLOCKNAME = BLOCKNAME;
        CombinedData.CARSPEED = CARSPEED;
        CombinedData.TRIAL = TRIAL;
        CombinedData.EVENTNAME = EVENTNAME;
        CombinedData.RESPONSENAME = RESPONSENAME;


        //updates data if subjects press SPACE to wave down cars
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TRIAL = carcycle.trial.ToString();
            CARSPEED = carcycle.speed.ToString();
            EVENTNAME = "Space Pressed";
            RESPONSENAME = "Score " + (scorekeeper.scoreValue + 1);
            dbconnect.Insert();
        }
    }

    void GetMPH()
    {   //updates data if subjects submit their estimated mph speed
        TRIAL = (carcycle.trial - 1).ToString();
        CARSPEED = carcycle.speed.ToString();
        EVENTNAME = "Estimated Speed";
        RESPONSENAME = carcycle.mphInputField.text;
        dbconnect.Insert();
        
    }

    public void GetCarLocation() //called from "CarCycle" script
    {   //updates data when the car changes location (starts moving and ends moving)
        TRIAL = carcycle.trial.ToString();
        CARSPEED = carcycle.speed.ToString();
        EVENTNAME = carcycle.carlocation;
        RESPONSENAME = "NA";
        dbconnect.Insert();
    }
    

}



