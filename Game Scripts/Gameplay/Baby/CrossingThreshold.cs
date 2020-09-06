//this script is to be added to the baby object to keep track of when they are
//crossing salient boundaries like blanket, grass, or road

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingThreshold : MonoBehaviour
{
    public bool BlanketCross = false;
    public bool RoadCross = false;

    public bool theRoad;
    public bool theGrass;
    public bool theBlanket;
 
    void OnTriggerEnter(Collider other)
    {   //logs if the baby crosses a threshold (either the edge of the blanket or the edge of the road)
        //the bool is reset to "false" in the "Narrative2" or the "BabyScene" data logging scripts
        if (other.gameObject.name == "BlanketThreshold")
        {
            BlanketCross = true;
        }
        if (other.gameObject.name == "RoadThreshold")
        {
            RoadCross = true;
        }


        if (other.gameObject.name == "RoadLocation")
        {
            theRoad = true;
            theBlanket = false;
            theGrass = false;
        }
        if (other.gameObject.name == "GrassLocation")
        {
            theRoad = false;
            theBlanket = false;
            theGrass = true;
        }
        if (other.gameObject.name == "BlanketLocation")
        {
            theRoad = false;
            theBlanket = true;
            theGrass = false;
        }
    }




}
