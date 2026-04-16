using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BrickAndWait", story: "[Self] waits then [Bricks]", category: "Action", id: "83298dd19b0e129ec874b45505501f56")]
public partial class BrickAndWaitAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Bricks;

    private float thrown = 30f;
    private Vector3 spawnPosition;
    private float timer = 0f;
    private float timer2 = 0f;
    private float brickThrow = 7f;
    private Animator bossimator;
    protected override Status OnUpdate()
    {
        GameObject Boss = GameObject.FindWithTag("Boss");
        bossimator = Boss.GetComponent<Animator>();
        AnimatorStateInfo bossInfo = bossimator.GetCurrentAnimatorStateInfo(0);
        
        timer += Time.deltaTime;
        if (timer > brickThrow)
        {
            spawnPosition = Self.Value.transform.position + (Self.Value.transform.forward * 1.2f) + (Vector3.up * 1.5f);

            GameObject brick = GameObject.Instantiate(Bricks.Value, spawnPosition, Self.Value.transform.rotation);

            Rigidbody rb = brick.GetComponent<Rigidbody>();

            rb.AddForce(Self.Value.transform.forward * thrown, ForceMode.Impulse);
            timer = 0f;
            return Status.Running;
        }
        if (bossInfo.IsName("Die"))
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

