using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimerScript : MonoBehaviour
{
    public int secondsToWait = 300;
    public PreroundScript MainPreroundScript;
    public WinLoseScript MainWinLoseScript;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
        MainWinLoseScript = GameObject.Find("EventSystem").GetComponent<WinLoseScript>();
    }

    private IEnumerator CountDown()
    {
        Debug.Log("Countdown");
        this.transform.Find("RoundTimeRemaining").GetComponent<TextMeshProUGUI>().SetText("Round time remaining: " + secondsToWait.ToString() + " seconds");
        yield return new WaitForSecondsRealtime(1);
        secondsToWait -= 1;
        if (secondsToWait <= 0)
        {
            if (SceneManager.GetActiveScene().name == "GhunterPlayerScene")
            {
                MainWinLoseScript.LoseGhunter("TimeRanOut");
            }
            else if (SceneManager.GetActiveScene().name == "GhostPlayerScene")
            {
                MainWinLoseScript.WinGhost("TimeRanOut");
            }
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
