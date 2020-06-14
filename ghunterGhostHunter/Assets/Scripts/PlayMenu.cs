using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

public class PlayMenu : MonoBehaviour
{
    private string nickname;
    private string hostUsername;
    private string hostPassword;
    private string ipAddress;
    private string connectionString;
    public TMP_InputField nicknameField;
    public TMP_InputField ipAddressField;
    public TMP_InputField hostUsernameField;
    public TMP_InputField hostPasswordField;

    public TMP_Text[] errorMessages;

    public LobbyConnectionScript MainLobbyConnectionScript;

    public ConnectionScript MainConnectionScript;

    public GameObject LobbyMenu;

    public string sceneNameToLoad;

    // Code debt: Hardcoded IDX. No time to make fluid.
    // To make fluid: Get all switches in Domoticz server and append to list
    public int[] ListOfSwitchidxToReset = { 17, 11, 13, 8, 16, 15, 14, 9, 12 };
    //private bool validConnection = false;
    public void Start()
    {
        
        if (PlayerPrefs.GetString("nickname") != null)
        {
            nickname = PlayerPrefs.GetString("nickname");
            nicknameField.text = nickname;

        }

        if (PlayerPrefs.GetString("ipAddress") != null)
        {
            ipAddress = PlayerPrefs.GetString("ipAddress");
            ipAddressField.text = ipAddress;

        }

        if (PlayerPrefs.GetString("hostUsername") != null)
        {
            hostUsername = PlayerPrefs.GetString("hostUsername");
            hostUsernameField.text = hostUsername;

        }

        if (PlayerPrefs.GetString("hostPassword") != null)
        {
            hostPassword = PlayerPrefs.GetString("hostPassword");
            hostPasswordField.text = hostPassword;

        }
    }
    public void PlayAsGhost()
    {
        SaveInput();
        sceneNameToLoad = "GhostPlayerScene";
        StartCoroutine(InitialiseGame());

    }

    public void PlayAsGhunter()
    {
        SaveInput();
        sceneNameToLoad = "GhunterPlayerScene";
        StartCoroutine(InitialiseGame());
        
    }

    private IEnumerator InitialiseGame()
    {
        foreach (int switchidxToDisable in ListOfSwitchidxToReset)
        {
            yield return MainConnectionScript.ChangeDomoticzSwitchStatus(switchidxToDisable, false);
        }
        StartCoroutine(TryConnectIP(sceneNameToLoad));
    }

    public void SaveNickname()
    {
        nickname = nicknameField.text;
        PlayerPrefs.SetString("nickname", nickname);
    }

    public void SaveConnectionDetails()
    {
        // TODO: Prevent URL tampering: Escape characters
        hostUsername = hostUsernameField.text;
        hostPassword = hostPasswordField.text;
        ipAddress = ipAddressField.text;
        connectionString = "http://" + hostUsername + ":" + hostPassword + "@" + ipAddress + "/json.htm?";

        PlayerPrefs.SetString("hostUsername", hostUsername);
        PlayerPrefs.SetString("hostPassword", hostPassword);
        PlayerPrefs.SetString("ipAddress", ipAddress);
    }

    void SaveInput()
    {
        SaveNickname();
        SaveConnectionDetails();
        
    }

    public IEnumerator TryConnectIP(string gameRole)
    {
        using (UnityWebRequest connectionTest = UnityWebRequest.Get(connectionString + "type=command&param=getversion"))
        {
            yield return connectionTest.SendWebRequest();

            if (connectionTest.error != null)
            {
                switch (connectionTest.error)
                {
                    case "Cannot connect to destination host":
                        Debug.Log("Cannot connect!");
                        // 3 = error text for connection
                        ipAddressField.transform.GetChild(3).gameObject.SetActive(true);
                        break;
                    default:
                        Debug.Log(connectionTest.error);
                        break;
                }
                //validConnection = false;
            }
            else
            {
                Debug.Log(connectionTest.downloadHandler.text);
                PlayerPrefs.SetString("connectionString", connectionString);
                //SwitchToLobby(gameRole);
                SceneManager.LoadSceneAsync(gameRole);
            }
        }
    }



    public void SaveConnectIP()
    {
        ipAddress = ipAddressField.text;

    }

    public void SwitchToLobby(string gameRoleToPlay)
    {
        LobbyMenu.SetActive(true);
        StartCoroutine(MainLobbyConnectionScript.CheckGameBegan("Ghunter"));
        this.gameObject.SetActive(false);
    }
}
