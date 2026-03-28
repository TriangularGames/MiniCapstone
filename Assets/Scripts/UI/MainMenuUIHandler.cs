using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion.Addons.ConnectionManagerAddon;

public class MainMenuUIHandler : MonoBehaviour
{
    [Header("Panels")]
    public GameObject playerDetailsPanel;
    public GameObject sessionBrowserPanel;
    public GameObject createSessionPanel;
    public GameObject statusPanel;

    [Header("Player settings")]
    public TMP_InputField playerNameInputField;

    [Header("New game session")]
    public TMP_InputField sessionNameInputField;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerNickname"))
            playerNameInputField.text = PlayerPrefs.GetString("PlayerNickname");
    }

    void HideAllPanels()
    {
        playerDetailsPanel.SetActive(false);
        sessionBrowserPanel.SetActive(false);
    }

    public async void OnFindGameClicked()
    {
        PlayerPrefs.SetString("PlayerNickname", playerNameInputField.text);
        PlayerPrefs.Save();

        ConnectionManager connectionManager = FindFirstObjectByType<ConnectionManager>();

        if (true) await connectionManager.Connect();

        HideAllPanels();

        sessionBrowserPanel.gameObject.SetActive(true);
        FindFirstObjectByType<SessionListUIHandler>().OnLookingForGameSessions();
    }

    public void OnCreateNewGameClicked()
    {
        HideAllPanels();

        createSessionPanel.SetActive(true);
    }

    public async void OnStartNewSessionClicked()
    {
        ConnectionManager connectionManager = FindFirstObjectByType<ConnectionManager>();

        if (true) await connectionManager.Connect();

        HideAllPanels();

        statusPanel.gameObject.SetActive(true);
    }

    public void OnJoiningServer()
    {
        HideAllPanels();

        statusPanel.gameObject.SetActive(true);
    }
}
