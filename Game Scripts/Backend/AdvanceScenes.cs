//this script is to help advance scenes
//loadscene ordering can be accessed in "File" >> "Build Settings" within the Unity UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceScenes : MonoBehaviour
{
    public string block; //can be referenced to get current block


    void Update()
    {
        //restricts subs from continuing without the researcher
        //researcher instructs them to hit "C" to move to informed consent
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(1);
        }

    }

    //advances to subject registration (id/condition input)
    //these are triggered by buttons on that page, rather than events
    public void GoToRegistration()
    {
        SceneManager.LoadScene(2);
    }

    //advances to gameplay (narrative)
    public void GoToTestButton()
    {
        SceneManager.LoadScene(3);
    }

    //advances to baby trials instructions
    public void GoToBabyInstructions()
    {
        Dialogue.Advances = 0; //this is just for extra redundancy
        SceneManager.LoadScene(5);
    }

    //advances to end of game instructions
    public void GoToEndGame()
    {
        SceneManager.LoadScene(7);
    }




    //calls on currently active scene
    public void CurrentBlock()
    {
        Scene blockstage = SceneManager.GetActiveScene();
        block =  blockstage.name;
    }
}

