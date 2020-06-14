using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFindScript : MonoBehaviour
{
    bool nearHideableObject;
    public LayerMask hidableObjectsMask;
    public Transform hidableObjectCheckerTransform;
    public Camera GhunterCamera;

    //Canvas
    private GameObject CanvasPlayerUIObject;

    // Needed for raycasting
    private int nonHidableObjectLayers = 1 << 10;

    private Collider[] hidableObjectsNearby;
    private Collider[] oldHidableObjectsNearby;

    // item ghost is hiding in
    private RaycastHit ObjectToHideIn;


    float nearRadius = 5f;

    private void Start()
    {
        CanvasPlayerUIObject = GameObject.Find("CanvasPlayerUI");
        // Revers from layer 10 to (not layer 10)
        nonHidableObjectLayers = ~nonHidableObjectLayers;

        // Needs to exist to properly define & use in Update()
        hidableObjectsNearby = Physics.OverlapSphere(hidableObjectCheckerTransform.position, nearRadius, hidableObjectsMask);
        oldHidableObjectsNearby = Physics.OverlapSphere(hidableObjectCheckerTransform.position, nearRadius, hidableObjectsMask);

    }

    // Update is called once per frame
    void Update()
    {
        CheckHidableObjectsNearby();
        if (Input.GetKeyDown("e"))
        {
            // If looking at something in Unity layer 10 (a.k.a is a object ghost can hide in)
            if (Physics.Raycast(GhunterCamera.transform.position, GhunterCamera.transform.forward, out ObjectToHideIn, nearRadius, ((1 << 10))))
            {
                Debug.Log(ObjectToHideIn.transform.gameObject.name);
                ObjectToHideIn.transform.gameObject.GetComponent<HidingScript>().GhunterCheckObject(this);
            }
            else { Debug.Log("nope"); }
        }
    }



    void CheckHidableObjectsNearby()
    {
        // sets old objects in range to variable
        oldHidableObjectsNearby = hidableObjectsNearby;

        //Returns array of objects in range
        hidableObjectsNearby = Physics.OverlapSphere(hidableObjectCheckerTransform.position, nearRadius, hidableObjectsMask);

        // Checks if hidable object in range. If yes: Show hint UI
        if (hidableObjectsNearby.Length > 0)
        {
            foreach (Collider objectNearby in hidableObjectsNearby)
            {
                objectNearby.gameObject.GetComponent<ShowUI>().SetUIActive("Ghunter");
            }
        }


        // Checks if hidable object goes out of range, if so, disable hint UI
        if (oldHidableObjectsNearby.Length > 0)
        {
            foreach (Collider oldObjectNearby in oldHidableObjectsNearby)
            {
                if (!hidableObjectsNearby.Contains(oldObjectNearby))
                {
                    oldObjectNearby.gameObject.GetComponent<ShowUI>().SetUIInactive("Ghunter");
                }
            }
        }
    }

    public void FoundGhost()
    {
        Debug.Log("Found a ghost!");
        CanvasPlayerUIObject.transform.Find("GhunterCaughtGhost").gameObject.SetActive(true);
        WinLoseScript MainWinLoseScript = GameObject.Find("EventSystem").GetComponent<WinLoseScript>();
        MainWinLoseScript.WinGhunter("FoundGhosts");
    }
}
