using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideTimerGhunter : MonoBehaviour
{
    public int secondsToWait = 15;
    public PreroundScript MainPreroundScript;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        Debug.Log("Countdown");
        this.transform.Find("HideSecondsGhunter").GetComponent<TextMeshProUGUI>().SetText("Giving ghosts " + secondsToWait.ToString() + " Seconds to hide");
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("bruh");
        secondsToWait -= 1;
        if (secondsToWait <= 0)
        {
            MainPreroundScript.StartRoundGhunter();
            this.transform.parent.Find("RoundTimer").gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Heer");
            yield return StartCoroutine(CountDown());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
