using Fusion;
using UnityEngine;

public class LoadGun : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            Debug.Log("Food in gun!");
            // TODO: play a sound perhaps?
            NetworkObject food = other.GetComponentInParent<NetworkObject>();
            Destroy(other.gameObject);
            gameObject.GetComponentInParent<GunShooter>().AddFood(food);
        }
    }
}
