using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "leave", story: "[Self] [returns] [and] dies", category: "Action", id: "deab8b9305bb79983c15dd67ddd027de")]
public partial class LeaveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<NavMeshAgent> Returns;
    [SerializeReference] public BlackboardVariable<Transform> And;
    EnemySpawner spawner;

    protected override Status OnStart()
    {
        //spawner.activeEnemies--;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
        //GameObject.Destroy(Self.Value);
    }
}

