using Fusion;
using Fusion.XR.Shared;
using Fusion.XR.Shared.Grabbing.NetworkHandColliderBased;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunShooter : NetworkBehaviour
{
    // To check if gun is being held
    [SerializeField] private NetworkHandColliderGrabbable grabbable;
    // The point the food is fired from
    [SerializeField] private Transform firePoint;
    // The food currently inside the gun
    [SerializeField] private List<NetworkObject> foodToFire;
    [SerializeField] private float speed = 2.0f;

    private bool pressed = false;

    [SerializeField] private GameObject Detector;

    public InputActionProperty interactAction;
    public InputActionProperty interactVRLeftAction;
    public InputActionProperty interactVRRightAction;

    private void Awake()
    {
        if (interactAction != null && interactAction.action != null)
        {
            if (interactAction.reference == null && interactAction.action.bindings.Count == 0)
            {
                interactAction.action.AddBinding("<Mouse>/rightButton");
                interactVRLeftAction.EnableWithDefaultXRBindings(side: Fusion.XR.Shared.Rig.RigPart.LeftController, new List<string> { "trigger" });
                interactVRRightAction.EnableWithDefaultXRBindings(side: Fusion.XR.Shared.Rig.RigPart.RightController, new List<string> { "trigger" });
            }

            interactAction.action.Enable();
        }
        grabbable = GetComponent<NetworkHandColliderGrabbable>();
        firePoint = transform.GetChild(1);
        Detector = transform.GetChild(2).gameObject;
    }

    private void FixedUpdate()
    {
        if (grabbable.status == NetworkHandColliderGrabbable.Status.Grabbed)
        {
            if (Detector.activeSelf)
            {
                Detector.SetActive(false);
            }
            if (interactAction.action.IsInProgress() || interactVRLeftAction.action.IsInProgress() || interactVRRightAction.action.IsInProgress())
            {
                if (!pressed)
                {
                    pressed = true;
                    Debug.Log("Interact Pressed!");
                    if (foodToFire.Count > 0)
                    {
                        // Get first food in gun
                        NetworkObject food = foodToFire[0];
                        foodToFire.RemoveAt(0);
                        NetworkObject obj = Runner.Spawn(food, firePoint.position, Quaternion.identity, inputAuthority: null);
                        obj.GetComponent<Rigidbody>().mass = 0.05f;
                        obj.GetComponent<Rigidbody>().AddForce(firePoint.forward * speed, ForceMode.Impulse);
                    }
                    pressed = false;
                }
            }
        }
        else if (grabbable.status == NetworkHandColliderGrabbable.Status.NotGrabbed)
        {
            if (!Detector.activeSelf)
            {
                Detector.SetActive(true);
            }
        }
    }

    public void AddFood(NetworkObject food)
    {
        foodToFire.Add(food);
    }
}
