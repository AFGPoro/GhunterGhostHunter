using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoseTimerGhost : MonoBehaviour
{
    public int secondsToWait = 15;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {

        this.transform.Find("BackToLobbyGhost").GetComponent<TextMeshProUGUI>().SetText("Sending you back to the main menu in " + secondsToWait.ToString() + " seconds..");
        yield return new WaitForSecondsRealtime(1);
        secondsToWait -= 1;
        if (secondsToWait <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadSceneAsync(0);
        }
        else
        {
            yield return StartCoroutine(CountDown());
        }
    }
}
