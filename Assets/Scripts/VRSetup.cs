using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class VRSetup : MonoBehaviour
{
    private BasicSpawner bs;
    private void Awake()
    {
        bs = GetComponent<BasicSpawner>();
        StartCoroutine("StartXRCoroutine");
    }

    public IEnumerator StartXRCoroutine()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.Log("No XR Detected");
            Destroy(GameObject.Find("VRPlayer"));
            Destroy(GameObject.Find("XR Canvas"));
        }
        else
        {
            Debug.Log("Starting XR...");
            bs.isVR = true;
            Destroy(GameObject.Find("Main Camera"));
            Destroy(GameObject.Find("Canvas"));
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
