using Unity.Behavior;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    [SerializeField] public float BrickBreakTimer = 5f;
    void Start()
    {
        Destroy(gameObject, BrickBreakTimer);
    }

   
}
