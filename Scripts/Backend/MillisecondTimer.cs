//this script is just constantly keeping track of time in seconds (with 3 decimal places)
//since the beginning of the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillisecondTimer : MonoBehaviour
{
    public string TIMENOW;
    public double TimeSinceStart;

    void Update()
    {   //updates the static variables in CombinedData script
        CombinedData.TIMENOW = TIMENOW;

        TimeSinceStart = Time.realtimeSinceStartup;
        TIMENOW = TimeSinceStart.ToString("f3");
    }
}
