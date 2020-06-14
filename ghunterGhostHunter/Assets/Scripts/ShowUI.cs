using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUI : MonoBehaviour
{
    public GameObject GhostCanvasUI;
    public GameObject GhunterCanvasUI;
    // Update is called once per frame

    public void SetUIActive(string ghunterOrGhost)
    {
        if (ghunterOrGhost == "Ghost")
        {
            GhostCanvasUI.SetActive(true);
        }
        else if (ghunterOrGhost == "Ghunter")
        {
            GhunterCanvasUI.SetActive(true);
        }
        
    }

    public void SetUIInactive(string ghunterOrGhost)
    {
        if (ghunterOrGhost == "Ghost")
        {
            GhostCanvasUI.SetActive(false);
        }
        else if (ghunterOrGhost == "Ghunter")
        {
            GhunterCanvasUI.SetActive(false);
        }
        
    }

}
