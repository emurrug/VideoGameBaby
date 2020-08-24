using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyWander : MonoBehaviour
{
   public int xPos;
   public int zPos;
  
    public GameObject theDestination;

    public GameObject toy1;
    public GameObject toy2;
    public GameObject toy3;


    private void Update()
    {
        if (theDestination.transform.position == toy1.transform.position |
            theDestination.transform.position == toy2.transform.position |
            theDestination.transform.position == toy3.transform.position)
        {
            StartCoroutine("SetRandomLocation");
        }
    }

    void OnTriggerEnter (Collider other)
   {
       if(other.tag == "baby")
       {
            StartCoroutine("SetRandomLocation");
	   }
   }

    IEnumerator SetRandomLocation()
    {
        xPos = Random.Range(249, 262);
        zPos = Random.Range(178, 195);
        theDestination.transform.position = new Vector3(xPos, 1, zPos);
        yield return null;
    }
}