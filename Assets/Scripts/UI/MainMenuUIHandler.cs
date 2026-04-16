using TMPro;
using UnityEngine;
using Fusion.Addons.ConnectionManagerAddon;

public class MainMenuUIHandler : MonoBehaviour
{
    [Header("Panels")]
    public GameObject playerDetailsPanel;
    public GameObject sessionBrowserPanel;
    public GameObject createSessionPanel;
    public GameObject BG;

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
        createSessionPanel.SetActive(false);
        sessionBrowserPanel.SetActive(false);
    }

    public void OnFindGameClicked()
    {
        PlayerPrefs.SetString("PlayerNickname", playerNameInputField.text);
        PlayerPrefs.Save();

        ConnectionManager connectionManager = FindFirstObjectByType<ConnectionManager>();

        connectionManager.OnJoinLobby();

        HideAllPanels();

        sessionBrowserPanel.SetActive(true);

        sessionBrowserPanel.GetComponent<SessionListUIHandler>().OnLookingForGameSessions();
    }

    public void OnCreateNewGameClicked()
    {
        HideAllPanels();
        createSessionPanel.SetActive(true);
    }

    public async void OnStartNewSessionClicked()
    {
        ConnectionManager connectionManager = FindFirstObjectByType<ConnectionManager>();

        if (true) await connectionManager.Connect(sessionNameInputField.text);

        HideAllPanels();
        BG.SetActive(false);
    }

    public void OnJoiningServer()
    {
        HideAllPanels();
        BG.SetActive(false);
    }
}
