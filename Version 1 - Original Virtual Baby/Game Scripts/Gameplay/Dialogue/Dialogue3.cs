//This script is for when the subject completes the game
//It exits the game and moves them to a qualtrics survey for demos/debrief

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Dialogue3 : MonoBehaviour
{
    public static int Advances = 0;     //how many times the subject has clicked (to read instructions)

    public GameObject DialogueBox;      //the canvas gameobject that has all the instructions elements

    public GameObject DialogueText;     //the written instructions
    public GameObject ClicktoContinue;  //small text item that lets subjects know how to advance

    public Button OpenSurvey;           //button to exit game and open qualtrics hyperlink


    void Start()
    {
        //for when subjects need to repeat instructions or move on after the instructions
        Button opensurvey = OpenSurvey.GetComponent<Button>();
        opensurvey.onClick.AddListener(EndTheGame);

        //make sure only the correct items are showing at start
        OpenSurvey.gameObject.SetActive(false);
        ClicktoContinue.gameObject.SetActive(true);

    }


    void Update()
    {


        //when subs click, they advance the instructions dialogue
        if (Input.GetMouseButtonDown(0) && Advances != 3)
        {
            Advances += 1;
        }
        if (Advances == 0)
        { DialogueText.GetComponent<Text>().text = "Congratulations! You successfully waved down 20 cars!"; }
        else if (Advances == 1)
        { DialogueText.GetComponent<Text>().text = "We sincerely appreciate your participation in this task."; }
        else if (Advances == 2)
        { DialogueText.GetComponent<Text>().text = "At this time, you will be prompted to close the game and open a link to a survey where you will answer a few questions about yourself."; }
        else if (Advances == 3) //will not advance after this point
        {
            DialogueText.GetComponent<Text>().text = "Whenever you are ready, click the button below to continue";
            
            ClicktoContinue.gameObject.SetActive(false);
            OpenSurvey.gameObject.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }

    }

    private void EndTheGame()
    {
        Application.OpenURL("https://cornell.qualtrics.com/jfe/form/SV_9pDBskhaTrbEXxH");
        Application.Quit();
    }
}

