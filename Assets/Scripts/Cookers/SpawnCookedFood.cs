using Fusion;
using UnityEngine;

public class SpawnCookedFood : NetworkBehaviour
{
    public void SpawnFood(NetworkObject prefab, Vector3 spawnPos)
    {
        var obj = Runner.Spawn(prefab, spawnPos, Quaternion.identity, inputAuthority: null);
    }
}
