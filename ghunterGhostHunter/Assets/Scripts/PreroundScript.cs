using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreroundScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject MainCanvasPlayerUI;
    public GameObject WaitingForStartUI;
    public GameObject HideTimerUI;
    public ConnectionScript MainConnectionScript;
    public int playerInGameSwitchidx = 16;
    public int GameStartedSwitchidx = 15;
    // Start is called before the first frame update
    void Start()
    {
        MainConnectionScript = GameObject.Find("EventSystem").GetComponent<ConnectionScript>();
        Player = GameObject.FindGameObjectWithTag("Player");
        
        Player.GetComponent<PlayerMovementScript>().CanMove = false;

        MainCanvasPlayerUI = GameObject.Find("CanvasPlayerUI");
        if (SceneManager.GetActiveScene().name == "GhunterPlayerScene")
        {
            playerInGameSwitchidx = 17;
            WaitingForStartUI = MainCanvasPlayerUI.transform.Find("WaitingForStartGhunter").gameObject;
            HideTimerUI = MainCanvasPlayerUI.transform.Find("HideTimerGhunter").gameObject;
        }
        else if (SceneManager.GetActiveScene().name == "GhostPlayerScene")
        {
            playerInGameSwitchidx = 16;
            WaitingForStartUI = MainCanvasPlayerUI.transform.Find("WaitingForStartGhost").gameObject;
            HideTimerUI = MainCanvasPlayerUI.transform.Find("HideTimerGhost").gameObject;

        }
        //WaitingForStartUI.SetActive(true);
        StartPreround();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPreround()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        WaitingForStartUI.SetActive(true);
        StartCoroutine(CheckIfGameBegan());
    }

    public IEnumerator CheckIfGameBegan()
    {
        yield return StartCoroutine(MainConnectionScript.CheckDomoticzSwitchStatus(GameStartedSwitchidx));
        if (MainConnectionScript.switchIsOn)
        {
            EndPreRound();
        }
        else
        {
            //yield return new WaitForSeconds(0.4f);
            yield return StartCoroutine(CheckIfGameBegan());
        }
    }

    public void EndPreRound()
    {
        if (SceneManager.GetActiveScene().name == "GhunterPlayerScene")
        {
            HideTimerUI.SetActive(true);
            WaitingForStartUI.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "GhostPlayerScene")
        {
            Player.GetComponent<PlayerMovementScript>().CanMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            StartCoroutine(MainConnectionScript.ChangeDomoticzSwitchStatus(playerInGameSwitchidx, true));
            HideTimerUI.SetActive(true);
            WaitingForStartUI.SetActive(false);
        }


    }

    public void StartRoundGhunter()
    {
        Player.GetComponent<PlayerMovementScript>().CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        StartCoroutine(MainConnectionScript.ChangeDomoticzSwitchStatus(playerInGameSwitchidx, true));
        WaitingForStartUI.SetActive(false);
    }

    public void StartMainRound()
    {
        
        StartCoroutine(MainConnectionScript.ChangeDomoticzSwitchStatus(GameStartedSwitchidx, true));
    }
}
