using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHideScript : MonoBehaviour
{
    bool nearHideableObject;
    public LayerMask hidableObjectsMask;
    public Transform hidableObjectCheckerTransform;
    public Transform ghostTransform;
    public Camera ghostCamera;
    public bool ghostIsHidden = false;
    public bool canUnhideManually = true;

    //for logging position prior to hiding
    private Vector3 priorPosition;

    //Canvas
    private GameObject CanvasPlayerUIObject;

    // Needed for raycasting
    private int nonHidableObjectLayers = 1 << 10;

    private Collider[] hidableObjectsNearby;
    private Collider[] oldHidableObjectsNearby;

    // from PlayerMoveScript
    public PlayerMovementScript ghostMoveScript;

    // item ghost is hiding in
    private RaycastHit ObjectToHideIn;


    float nearRadius = 5f;

    private void Start()
    {
        // Finds UI
        CanvasPlayerUIObject = GameObject.Find("CanvasPlayerUI");

        // Revers from layer 10 to (not layer 10)
        nonHidableObjectLayers = ~nonHidableObjectLayers;

        // Needs to exist to properly define & use in Update()
        hidableObjectsNearby = Physics.OverlapSphere(hidableObjectCheckerTransform.position, nearRadius, hidableObjectsMask);
        oldHidableObjectsNearby = Physics.OverlapSphere(hidableObjectCheckerTransform.position, nearRadius, hidableObjectsMask);

        ghostTransform = gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        CheckHidableObjectsNearby();
        if (Input.GetKeyDown("e"))
        {
            if (ghostIsHidden == false)
            {
                

                // If looking at something in Unity layer 10 (a.k.a is a object ghost can hide in)
                if (Physics.Raycast(ghostCamera.transform.position, ghostCamera.transform.forward, out ObjectToHideIn, nearRadius, ((1 << 10))))
                {
                    Debug.Log(ObjectToHideIn);
                    HideGhost(ObjectToHideIn);
                }
            }
            else if (ghostIsHidden && canUnhideManually)
            {
                UnhideGhost(ObjectToHideIn);
            }

        }
    }

    void HideGhost(RaycastHit ObjectGhostIsIn)
    {
        // check and trigger the GhostHide sequence in object the ghost is hiding in
        if (ObjectGhostIsIn.transform.gameObject.GetComponent<HidingScript>().GhostHide(this) == true)
        {
            ghostIsHidden = true;
            ghostMoveScript.CanMove = false;
            // Log position prior to hiding
            priorPosition = transform.position;

            // Set position to be the object the ghost is hiding in
            transform.position = ObjectGhostIsIn.transform.position;
            transform.rotation = ObjectGhostIsIn.transform.rotation;
        }
        
    }

    void UnhideGhost(RaycastHit ObjectGhostIsIn)
    {
        if (ObjectGhostIsIn.transform.gameObject.GetComponent<HidingScript>().GhostUnhide() == true)
        {
            ghostIsHidden = false;
            ghostMoveScript.CanMove = true;
            transform.position = priorPosition;
        }

        
    }

    public void GhostCaught()
    {
        Debug.Log("Caught by Ghunter!");
        GhostLose("GhostWasCaught");


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
                objectNearby.gameObject.GetComponent<ShowUI>().SetUIActive("Ghost");
            }
        }


        // Checks if hidable object goes out of range, if so, disable hint UI
        if (oldHidableObjectsNearby.Length > 0)
        {
            foreach (Collider oldObjectNearby in oldHidableObjectsNearby)
            {
                if (!hidableObjectsNearby.Contains(oldObjectNearby))
                {
                    oldObjectNearby.gameObject.GetComponent<ShowUI>().SetUIInactive("Ghost");
                }
            }
        }
    }
    public void HidingTimeOver()
    {
        if (!ghostIsHidden)
        {
            GhostLose("NotHiddenInTime");
        }
    }

    public void GhostLose(string loseReasonUI)
    {
        GameObject.Find("EventSystem").GetComponent<WinLoseScript>().LoseGhost(loseReasonUI);
    }
}
