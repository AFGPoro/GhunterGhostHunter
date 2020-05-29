using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HidingScript : MonoBehaviour
{
    //UUID for Domoticz
    //TODO: Automatically assign idx to all hidable objects (seperate class)
    public int idx = 1;
    public bool GhostOccupying = false;


    private ConnectionScript mainConnectionScript;

    private void Start()
    {
        mainConnectionScript = GameObject.Find("EventSystem").GetComponent<ConnectionScript>();

    }

    private void FixedUpdate()
    {
        //CheckOccupancy();
    }


    // Start is called before the first frame update
    public bool GhostHide()
    {

        //////TODO: Checks needed if multiple ghosts allowed.
        //CheckOccupancy();
        //Debug.Log(GhostOccupying);
        //// If statement is lazy, so if checkoccupancy = false it won't trigger the changedomoticzswitch
        //if (GhostOccupying == false)
        //{
        //    // Check and excecute changing of 
        //    if ((mainConnectionScript.ChangeDomoticzSwitchStatus(idx, true) == true))
        //    {
        //        GhostOccupying = true;
        //        return true;
        //    }
        //    else { return false; }
        //}
        //else { return false; }

        // This is only for single player!!
        // TODO: Error handling
        if (GhostOccupying == false)
        {
            StartCoroutine(mainConnectionScript.ChangeDomoticzSwitchStatus(idx, true));
            GhostOccupying = true;
            return true;
        }
        else { return false;  }

    }

    public bool GhostUnhide()
    {
        // TODO: Error handling
        StartCoroutine(mainConnectionScript.ChangeDomoticzSwitchStatus(idx, false));
        GhostOccupying = false;
        return true;
 
    }

    // Unused, part of multiple-ghost check. Not needed in PoC
   private bool CheckOccupancy()
    {
        if (mainConnectionScript.CheckDomoticzSwitchStatus(idx) == true)
        {
            GhostOccupying = true;
            return true;
        }
        else
        {
            GhostOccupying = false;
            return false;
        }
    }
}
