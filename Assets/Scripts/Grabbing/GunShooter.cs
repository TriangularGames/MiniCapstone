using Fusion;
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
    [SerializeField] private float speed = 10.0f;

    [SerializeField] private GameObject Detector;

    public InputActionProperty interactAction;

    private void Awake()
    {
        if (interactAction != null && interactAction.action != null)
        {
            if (interactAction.reference == null && interactAction.action.bindings.Count == 0)
            {
                interactAction.action.AddBinding("<Mouse>/rightButton");
                interactAction.action.AddBinding("<LeftHandXRController>/PrimaryAction");
            }

            interactAction.action.Enable();
        }
        grabbable = GetComponent<NetworkHandColliderGrabbable>();
        firePoint = transform.GetChild(1);
        Detector = transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        if (grabbable.status == NetworkHandColliderGrabbable.Status.Grabbed)
        {
            if (Detector.activeSelf)
            {
                Detector.SetActive(false);
            }
            if (interactAction.action.IsPressed())
            {
                Debug.Log("Interact Pressed!");
                if (foodToFire.Count > 0)
                {
                    // Get first food in gun
                    NetworkObject food = foodToFire[0];
                    foodToFire.RemoveAt(0);
                    NetworkObject obj = Runner.Spawn(food, firePoint.position, Quaternion.identity, inputAuthority: null);
                    obj.GetComponent<Rigidbody>().AddForce(firePoint.forward * speed, ForceMode.Impulse);
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
