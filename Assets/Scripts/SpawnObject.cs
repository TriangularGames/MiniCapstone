using Fusion;
using Fusion.XR.Shared.Rig;
using UnityEngine;

public class SpawnObject : NetworkBehaviour
{
    [SerializeField] private NetworkObject prefab;
    private Vector3 spawnPos;

    private void Awake()
    {
        spawnPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(prefab.gameObject.name + " spawned!");
            var obj = Runner.Spawn(prefab, spawnPos, Quaternion.identity, inputAuthority: null);
        }
    }
}
