using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MinionToCounter", story: "[Self] [nav] to [Counter] from [Spawner]", category: "Action", id: "6ac58538c358206070c8f3b7e81879b7")]
public partial class MinionToCounterAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<NavMeshAgent> Nav;
    [SerializeReference] public BlackboardVariable<Transform> Counter;
    [SerializeReference] public BlackboardVariable<Transform> Spawner;
   
    public static List<Transform> WaypointTaken = new List<Transform>();
    private Transform wayTarg = null;
    private float walkLess = Mathf.Infinity;
    private Transform close = null;

    protected override Status OnStart()
    {
        GameObject waypnt = GameObject.FindWithTag("Waypoints");
        Counter.Value = waypnt.transform;
        GameObject sewnr = GameObject.FindWithTag("Spawners");
        Spawner.Value = sewnr.transform;

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
            if (close.name == "SpawningArchP1")
            {
                Transform way5= Counter.Value.GetChild(5);
                Transform way4 = Counter.Value.GetChild(4);

                if (WaypointTaken.Contains(way5))
                {
                    wayTarg = way4;
                }
                else
                {
                    wayTarg = way5;
                }

            }
            else if (close.name == "SpawningArchP2")
            {
                Transform way2 = Counter.Value.GetChild(2);
                Transform way1 = Counter.Value.GetChild(1);

                if (WaypointTaken.Contains(way2))
                {
                    wayTarg = way1;
                }
                else
                {
                    wayTarg = way2;
                }
            }

            if (wayTarg != null)
            {
                
                if (!WaypointTaken.Contains(wayTarg))
                {
                    WaypointTaken.Add(wayTarg);
                }

                Nav.Value.SetDestination(wayTarg.position);
                return Status.Running;
            }
        }

            return Status.Running;
    }
   
    protected override Status OnUpdate()
    {
        if (!Nav.Value.pathPending && Nav.Value.remainingDistance <= Nav.Value.stoppingDistance)
        {
            return Status.Success;
        }
        return Status.Running;
    }
}

