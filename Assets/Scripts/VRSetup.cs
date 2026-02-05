using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class VRSetup : MonoBehaviour
{
    private BasicSpawner bs;
    private void Awake()
    {
        bs = GetComponent<BasicSpawner>();
        StopXR();
        StartCoroutine("StartXRCoroutine");
    }

    public IEnumerator StartXRCoroutine()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.Log("No XR Detected");
            GameObject.Find("VRPlayer").gameObject.SetActive(false);
            GameObject.Find("XR Canvas").gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Starting XR...");
            bs.isVR = true;
            //GameObject.Find("Main Camera").gameObject.SetActive(false);
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
