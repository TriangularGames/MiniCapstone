using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class VRSetup : MonoBehaviour
{
    [SerializeField] GameObject VRPlayer;
    [SerializeField] GameObject VRCanvas;

    [SerializeField] GameObject PCCanvas;

    private void Awake()
    {
        StartCoroutine("StartXRCoroutine");
    }

    public IEnumerator StartXRCoroutine()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.Log("No XR Detected");
            Instantiate(PCCanvas);
            Destroy(GameObject.Find("XR Interaction Manager"));
        }
        else
        {
            Debug.Log("Starting XR...");
            //Instantiate(VRPlayer);
            //Instantiate(VRCanvas);
            Destroy(GameObject.Find("Main Camera"));
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
    }

    void StopXR()
    {
        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
    }
}
