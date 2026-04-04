using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToCounter", story: "[Enemy] [moves] to [waypoint]", category: "Action", id: "037fad9e3389397b0ce5872cd51c5125")]
public partial class MoveToCounterAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<NavMeshAgent> Moves;
    [SerializeReference] public BlackboardVariable<Transform> Waypoint;

    private static List<Transform> WaypointTaken = new List<Transform>();
    private Transform wayTarg = null;
    private float walkLess = Mathf.Infinity;
    private Transform close = null;
    protected override Status OnStart()
    {
        if (wayTarg != null)
        {
            Moves.Value.SetDestination(wayTarg.position);
            return Status.Running;
        }

        foreach (Transform point in Waypoint.Value)
        {
            if (WaypointTaken.Contains(point)) continue;

            float distance = Vector3.Distance(Enemy.Value.transform.position, point.position);
            if (distance < walkLess)
            {
                walkLess = distance;
                close = point;
            }
        }
        if (close != null)
        {
            wayTarg = close;
            WaypointTaken.Add(wayTarg);
            Moves.Value.SetDestination(wayTarg.position);
            return Status.Running;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!Moves.Value.pathPending && Moves.Value.remainingDistance <= Moves.Value.stoppingDistance)
        {
            return Status.Success;
        }
        return Status.Running;
    }
}

