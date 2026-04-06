using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "leave", story: "[Self] [returns] [and] dies", category: "Action", id: "deab8b9305bb79983c15dd67ddd027de")]
public partial class LeaveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<NavMeshAgent> Returns;
    [SerializeReference] public BlackboardVariable<Transform> And;
    EnemySpawner spawner;
    private Transform wayTarg = null;
    private float walkLess = Mathf.Infinity;
    private Transform close = null;
    private Transform free = null; 
    protected override Status OnStart()
    {
        GameObject sewnr = GameObject.FindWithTag("Spawners");
        And.Value = sewnr.transform;
        foreach (Transform freepoint in MoveToCounterAction.WaypointTaken)
        {
            if (Vector3.Distance(Self.Value.transform.position, freepoint.position) < 2f)
            {
                free = freepoint;
                break;
            }
        }
        if (free != null)
        {
            MoveToCounterAction.WaypointTaken.Remove(free);
        }

        if (wayTarg != null)
        {
            Returns.Value.SetDestination(wayTarg.position);
            return Status.Running;
        }


        foreach (Transform point in And.Value)
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
            Returns.Value.SetDestination(wayTarg.position);
            return Status.Running;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!Returns.Value.pathPending && Returns.Value.remainingDistance <= Returns.Value.stoppingDistance)
        {
            spawner = close.GetComponent<EnemySpawner>();
            spawner.activeEnemies--;
            if (spawner.activeBossEnemies > 0)
            {
              spawner.activeBossEnemies--;
            }
            GameObject.Destroy(Self.Value);
        return Status.Success;
        }
        return Status.Running;
    }

}

