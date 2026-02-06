using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class Player : NetworkBehaviour
{
    private NetworkCharacterController _cc;
    public GameObject PC;
    public GameObject VR;
    private GameObject VRCanvas = null;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Instantiate(PC, transform);
        }
        else
        {
            Instantiate(VR, transform);
            VRCanvas = GameObject.Find("XR Canvas").transform.GetChild(0).gameObject;
            VRCanvas.SetActive(true);
        }

        if (VRCanvas != null)
        {
            GameObject btn = VRCanvas.transform.GetChild(2).gameObject;
            Button Send = btn.GetComponent<Button>();
            Send.onClick.AddListener(delegate { SendMsg(); });
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (HasInputAuthority)
        {
            if (GetInput(out NetworkInputData data))
            {
                data.direction.Normalize();
                _cc.Move(5 * data.direction * Runner.DeltaTime);
            }
        }
    }

    private void Update()
    {
        if (HasInputAuthority && Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            RPC_SendMessage("Hey Mate!");
        }
    }

    private TMP_Text _messages;

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_SendMessage(string message, RpcInfo info = default)
    {
        RPC_RelayMessage(message, info.Source);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayMessage(string message, PlayerRef messageSource)
    {
        if (_messages == null) _messages = FindFirstObjectByType<TMP_Text>();

        if (messageSource == Runner.LocalPlayer)
        {
            message = $"You said: {message}\n";
            Debug.Log(message);
        }
        else
        {
            message = $"Some other player said: {message}\n";
            Debug.Log(message);
        }

        _messages.text += message;
    }

    public void SendMsg()
    {
        if (HasInputAuthority) RPC_SendMessage("Tally Ho!");
    }
}
