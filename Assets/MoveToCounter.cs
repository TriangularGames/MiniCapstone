using System.Buffers;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class MoveToCounter : StateMachineBehaviour
{
    [SerializeReference] public Transform Waypoint;

    public NavMeshAgent Moves; 
    public static List<Transform> WaypointTaken = new List<Transform>();
    private Transform wayTarg = null;
    private float walkLess = Mathf.Infinity;
    private Transform close = null;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject Enemy = animator.gameObject;
        Moves = Enemy.GetComponent<NavMeshAgent>(); 
        GameObject waypnt = GameObject.FindWithTag("Waypoints");
        Waypoint = waypnt.transform;
        if (wayTarg != null)
        {
            Moves.SetDestination(wayTarg.position);
        }

        foreach (Transform point in Waypoint)
        {
            if (WaypointTaken.Contains(point)) continue;

            float distance = Vector3.Distance(Enemy.transform.position, point.position);
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
            Moves.SetDestination(wayTarg.position);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!Moves.pathPending && Moves.remainingDistance <= Moves.stoppingDistance)
        {
            animator.SetTrigger("Wait");
        }
    }

}
