using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class ScoreKeeper : MonoBehaviour
{

    public int scoreValue = 0;
    public GameObject score;


    void Update()
    {
        
        score.GetComponent<Text>().text = "Cars Flagged:" + scoreValue;

        scoreValue = CarCycle.scoreValue;

    }
}

