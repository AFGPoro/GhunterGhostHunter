using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ConnectionScript : MonoBehaviour
{
    // full URL. "http://" + hostUsername + ":" + hostPassword + "@" + ipAddress + "/json.htm?";
    public string connectionString;
    public Canvas ErrorCanvas;
    public bool connectionError;
    public bool checkForConnection = true;
    public bool switchIsOn;
    public bool switchStatusChanged;

    private void Start()
    {
        if (PlayerPrefs.GetString("connectionString") != null)
        {
            connectionString = PlayerPrefs.GetString("connectionString");
        }

        StartCoroutine(TryConnectDomoticz());
    }

    public IEnumerator ChangeDomoticzSwitchStatus(int idx, bool switchcmd)
    {
        string onOffCmd;

        // convert bool to string
        if (switchcmd == true)
        {
            onOffCmd = "On";
        }
        else { onOffCmd = "Off";  }

        string switchConnectionString = connectionString + string.Format("type=command&param=switchlight&idx={0}&switchcmd={1}", idx, onOffCmd);
        using (UnityWebRequest editSwitchRequest = UnityWebRequest.Get(switchConnectionString))
        {
            yield return editSwitchRequest.SendWebRequest();
            // Return false if change not successful. Return true if successful.
            if (editSwitchRequest.error != null)
            {
                switchStatusChanged = false;
            }
            else
            {
                switchStatusChanged = true;
            }
        }
    }

    public IEnumerator ChangeDomoticzDistanceSensorDistance(int idx, int centimeters)
    {

        string distanceSensorConnectionString = connectionString + string.Format("type=command&param=udevice&idx={0}&nvalue=0&svalue={1}", idx, centimeters);
        using (UnityWebRequest editDistanceSensorDistanceRequest = UnityWebRequest.Get(distanceSensorConnectionString))
        {
            yield return editDistanceSensorDistanceRequest.SendWebRequest();
            // TODO: What if error?
        }
    }
    public bool CheckDomoticzSwitchStatus(int idx)
    {
        // TODO: What if error?
        string switchCommandString = connectionString + string.Format("type=devices&rid={0}", idx);
        StartCoroutine(CheckDomoticzSwitch(switchCommandString));

        return switchIsOn;

    }

    private IEnumerator CheckDomoticzSwitch(string switchCommandString)
    {
        using (UnityWebRequest checkSwitchRequest = UnityWebRequest.Get(switchCommandString))
        {
            yield return checkSwitchRequest.SendWebRequest();

            if (checkSwitchRequest.error != null)
            {
                Debug.Log(checkSwitchRequest.error);
                // No result given. Need error check in caller
            }
            else
            {
                Switch HidableObjectSwitch = JsonUtility.FromJson<Switch>(checkSwitchRequest.downloadHandler.text);
                Debug.Log(HidableObjectSwitch.result[0].Data);
                // If switch is on, a ghost should already be in the object
                if (HidableObjectSwitch.result[0].Data == "On")
                {
                    switchIsOn = true;
                }
                else
                {
                    switchIsOn = false;
                }
            }
        }
    }


    public IEnumerator TryConnectDomoticz()
    {
        while (checkForConnection)
        {
            using (UnityWebRequest connectionTest = UnityWebRequest.Get(connectionString + "type=command&param=getversion"))
            {
                yield return connectionTest.SendWebRequest();

                if (connectionTest.error != null)
                {
                    Debug.Log(connectionTest.error);
                    connectionError = true;
                    //TODO: Save location and status of player and freeze player where player was before error happend

                    //TODO: Check ping


                    switch (connectionTest.error)
                    {
                        default:
                            // 0 = ConnectionErrorObject
                            ErrorCanvas.transform.GetChild(0).gameObject.SetActive(true);
                            // second getchild 0 = countdown
                            StartCoroutine(DisconnectCountdown(30f, ErrorCanvas.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TMP_Text>()));
                            break;
                    }
                }
                else
                {
                    connectionError = false;
                }
            }
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator DisconnectCountdown(float secondsUntilDisconnect, TMP_Text CountDownText)
    {
        if (connectionError == true)
        {
            CountDownText.text = secondsUntilDisconnect.ToString() + "s";
            secondsUntilDisconnect -= 0.1f;
            // Return to main menu if countdown is done and no connection is established
            if (secondsUntilDisconnect <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
            yield return new WaitForSeconds(.1f);
            
        }
        else
        {
            StopCoroutine("DisconnectCountdown");
        }

    }
}
