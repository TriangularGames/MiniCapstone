using UnityEngine;

public class Change : MonoBehaviour
{
    [SerializeField] private GameObject cookedVersion;

    public void ChangeObj()
    {
        if (cookedVersion != null)
        {
            Instantiate(cookedVersion, transform.position, Quaternion.identity);
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
