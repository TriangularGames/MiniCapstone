using System;
using Unity.Behavior;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MinionDies", story: "[Self] [Navs] to [Spawner] and dies", category: "Action", id: "96f0c31e151a8168d51e98496faf8b20")]
public partial class MinionDiesAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<NavMeshAgent> Navs;
    [SerializeReference] public BlackboardVariable<Transform> Spawner;
    EnemySpawner spawner;
    private Transform wayTarg = null;
    private float walkLess = Mathf.Infinity;
    private Transform close = null;
    private Transform free = null;
    protected override Status OnStart()
    {
        GameObject sewnr = GameObject.FindWithTag("Spawners");
        Spawner.Value = sewnr.transform;
        foreach (Transform freepoint in MinionToCounterAction.WaypointTaken)
        {
            if (Vector3.Distance(Self.Value.transform.position, freepoint.position) < 5f)
            {
                free = freepoint;
                break;
            }
        }
        if (free != null)
        {
            MinionToCounterAction.WaypointTaken.Remove(free);
        }

        if (wayTarg != null)
        {
            Navs.Value.SetDestination(wayTarg.position);
            return Status.Running;
        }


        foreach (Transform point in Spawner.Value)
        {

            float distance = Vector3.Distance(Self.Value.transform.position, point.position);
            if (distance < walkLess)
            {
                walkLess = distance;
                close = point;
            }
        }
        if (close != null)
        {
            wayTarg = close;
            Navs.Value.SetDestination(wayTarg.position);
            return Status.Running;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!Navs.Value.pathPending && Navs.Value.remainingDistance <= Navs.Value.stoppingDistance)
        {
            spawner = close.GetComponent<EnemySpawner>();
            spawner.activeBossEnemies--;
            GameObject.Destroy(Self.Value);
            return Status.Success;
        }
        return Status.Running;
    }
}

