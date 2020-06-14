using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyConnectionScript : MonoBehaviour
{
    private ConnectionScript MainConnectionScript;
    private int gameStartedSwitchidx = 15;
    // Start is called before the first frame update
    void Start()
    {
        MainConnectionScript = gameObject.GetComponent<ConnectionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CheckGameBegan(string gameRole)
    {
        yield return StartCoroutine(MainConnectionScript.CheckDomoticzSwitchStatus(gameStartedSwitchidx));
        if (MainConnectionScript.switchIsOn)
        {
            SceneManager.LoadSceneAsync(gameRole);
        }
        yield return StartCoroutine(CheckGameBegan(gameRole)); 
        
    }

    public void StartGame()
    {
        MainConnectionScript.ChangeDomoticzSwitchStatus(gameStartedSwitchidx, true);
    }


}
