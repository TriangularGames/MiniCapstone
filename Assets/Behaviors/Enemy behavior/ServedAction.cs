using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Served", story: "[Self] is waiting to [be] served", category: "Action", id: "ee1edcc0c39de1a815d183327109b318")]
public partial class ServedAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Be;

    private float thrown = 30f;
    private Vector3 spawnPosition;
    private float timer = 0f;  
    private float timer2 = 0f;
    private float brickThrow = 7f;

    protected override Status OnUpdate()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer > brickThrow)
        {
            spawnPosition = Self.Value.transform.position + (Self.Value.transform.forward * 1.2f) + (Vector3.up * 1.5f);

            GameObject brick = GameObject.Instantiate(Be.Value, spawnPosition, Self.Value.transform.rotation);

            Rigidbody rb = brick.GetComponent<Rigidbody>();

            rb.AddForce(Self.Value.transform.forward * thrown, ForceMode.Impulse);
            timer = 0f;
            return Status.Running;
        }
        if (timer2 > 40f)
        {
            timer2 = 0f;
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

