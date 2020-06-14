using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideTimerGhost : MonoBehaviour
{
    public int secondsToWait = 15;
    public PreroundScript MainPreroundScript;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Ghost");
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {

        this.transform.Find("HideSecondsGhost").GetComponent<TextMeshProUGUI>().SetText("You have " + secondsToWait.ToString() + " Seconds to hide!");
        yield return new WaitForSecondsRealtime(1);
        secondsToWait -= 1;
        if (secondsToWait <= 0)
        {
            Player.GetComponent<PlayerHideScript>().HidingTimeOver();
            this.transform.parent.Find("RoundTimer").gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        { 
            yield return StartCoroutine(CountDown());
        }
    }

}
