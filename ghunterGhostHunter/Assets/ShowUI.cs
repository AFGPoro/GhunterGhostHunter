using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUI : MonoBehaviour
{
    public GameObject canvasUI;
    // Update is called once per frame
    public void ToggleUI()
    {
        canvasUI.SetActive(!canvasUI.activeInHierarchy);
    }

    public void SetUIActive()
    {
        canvasUI.SetActive(true);
    }

    public void SetUIInactive()
    {
        canvasUI.SetActive(false);
    }

}
