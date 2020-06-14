using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceSensorScript : MonoBehaviour
{
    public int distanceSensoridx;
    public int distanceSensorTriggeredidx;
    private bool canTrigger = true;
    private RaycastHit CubeSensorBlocker;
    public GameObject LazerVisualiser;
    
    private ConnectionScript mainConnectionScript;

    float distanceBetweenLazerPoints;
    
    // This is supposed to be a simulation of a distance sensor using a lazer.
    // In real game development, you probably would've used a collision detector. However,
    // the point of this excersize is to simulate a distance sensor.


    private void Start()
    {
        mainConnectionScript = GameObject.Find("EventSystem").GetComponent<ConnectionScript>();
        Physics.Raycast(transform.position, transform.forward, hitInfo: out CubeSensorBlocker, 50, (1 << 11 | 1 << 12));
        distanceBetweenLazerPoints = CubeSensorBlocker.distance;
        if (SceneManager.GetActiveScene().name == "GhunterPlayerScene")
        {
            this.transform.parent.Find("Cylinder").gameObject.SetActive(false);
            canTrigger = false;
        }
    }


    //TODO: Complete. 
    // Function scales lazer. Current method: Hand-scale the lazer. Is only for looks.
    public void DetermineLazerLength()
    {
        Physics.Raycast(transform.position, transform.forward, hitInfo: out CubeSensorBlocker, 50, (1 << 11 | 1 << 12));
        LazerVisualiser.transform.localScale = new Vector3(LazerVisualiser.transform.localScale.x, CubeSensorBlocker.distance, LazerVisualiser.transform.localScale.z);
        LazerVisualiser.transform.position = new Vector3(LazerVisualiser.transform.position.x, LazerVisualiser.transform.position.y, CubeSensorBlocker.distance / 2);
        Debug.Log(CubeSensorBlocker.distance);
    }

    private void FixedUpdate()
    {
        // Doing it this way is not efficient game design. See top comments for explaination.
   
        // Update Laser Sensor in Domoticz when player walks through sensor (so distance is less than normal)
        // Trigger DistanceSensorTriggered switch if ghost walked through sensor
        Physics.Raycast(transform.position, transform.forward, hitInfo: out CubeSensorBlocker, 50, (1 << 11 | 1 << 12));
        if ((CubeSensorBlocker.distance < distanceBetweenLazerPoints) && (canTrigger == true));
        {
            StartCoroutine(mainConnectionScript.ChangeDomoticzSwitchStatus(distanceSensorTriggeredidx, true));
            // Calc scales up unity Units to realistic cm size
            StartCoroutine(mainConnectionScript.ChangeDomoticzDistanceSensorDistance(distanceSensoridx, (Mathf.RoundToInt(CubeSensorBlocker.distance) / 2 * 100)));
            
        }
    }
}
