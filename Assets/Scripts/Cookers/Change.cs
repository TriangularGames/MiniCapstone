using Fusion;
using UnityEngine;

public class Change : MonoBehaviour
{
    [SerializeField] private GameObject cookedVersion;
    [SerializeField] private SpawnCookedFood foodSpawner;

    private void Awake()
    {
        foodSpawner = GameObject.Find("_FoodSpawner").GetComponent<SpawnCookedFood>();
    }

    public void ChangeObj()
    {
        if (cookedVersion != null)
        {
            foodSpawner.SpawnFood(cookedVersion.GetComponent<NetworkObject>(), transform.position);
            Destroy(gameObject);
            AudioManager.Instance.PlayClip("ding 1");
        }
        else
        {
            Destroy(gameObject);
            AudioManager.Instance.PlayClip("Fire1");
        }
    }
}
