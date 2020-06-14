using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoseGhost(string reasonForLosing)
    {
        int ghostInGameSwitchidx = 16;
        GameObject CanvasPlayerUIObject = GameObject.Find("CanvasPlayerUI");
        ConnectionScript MainConnectionScript = GameObject.Find("EventSystem").GetComponent<ConnectionScript>();
        // Get the player
        PlayerMovementScript ghostMoveScript = GameObject.Find("Ghost").GetComponent<PlayerMovementScript>();
        // Enabling right UI elements
        CanvasPlayerUIObject.transform.Find("GhostLost").gameObject.SetActive(true);
        CanvasPlayerUIObject.transform.Find("GhostLost").transform.Find(reasonForLosing).gameObject.SetActive(true);
        ghostMoveScript.CanMove = false;
        // change ghost 1 is in game switch to off
        MainConnectionScript.ChangeDomoticzSwitchStatus(ghostInGameSwitchidx, false);
    }
    
    public void WinGhost(string reasonForWinning)
    {
        GameObject CanvasPlayerUIObject = GameObject.Find("CanvasPlayerUI");
        // Get the player
        PlayerMovementScript ghostMoveScript = GameObject.Find("Ghost").GetComponent<PlayerMovementScript>();
        // Enabling right UI elements
        CanvasPlayerUIObject.transform.Find("GhostWon").gameObject.SetActive(true);
        CanvasPlayerUIObject.transform.Find("GhostWon").transform.Find(reasonForWinning).gameObject.SetActive(true);
        ghostMoveScript.CanMove = false;
    }

    public void LoseGhunter(string reasonForLosing)
    {
        GameObject CanvasPlayerUIObject = GameObject.Find("CanvasPlayerUI");
        // Get the player
        PlayerMovementScript ghunterMoveScript = GameObject.Find("Ghunter").GetComponent<PlayerMovementScript>();
        // Enabling right UI elements
        CanvasPlayerUIObject.transform.Find("GhunterLost").gameObject.SetActive(true);
        CanvasPlayerUIObject.transform.Find("GhunterLost").transform.Find(reasonForLosing).gameObject.SetActive(true);
        ghunterMoveScript.CanMove = false;
    }

    public void WinGhunter(string reasonForWinning)
    {
        GameObject CanvasPlayerUIObject = GameObject.Find("CanvasPlayerUI");
        // Get the player
        PlayerMovementScript ghunterMoveScript = GameObject.Find("Ghunter").GetComponent<PlayerMovementScript>();
        // Enabling right UI elements
        CanvasPlayerUIObject.transform.Find("GhunterWon").gameObject.SetActive(true);
        CanvasPlayerUIObject.transform.Find("GhunterWon").transform.Find(reasonForWinning).gameObject.SetActive(true);
        ghunterMoveScript.CanMove = false;
    }
}
