using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Die : StateMachineBehaviour
{
    [SerializeReference] public NavMeshAgent Returns;
    [SerializeReference] public Transform And;
    EnemySpawner spawner;
    private Transform wayTarg = null;
    private float walkLess = Mathf.Infinity;
    private Transform close = null;
    private Transform free = null;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject sewnr = GameObject.FindWithTag("Spawners");
        GameObject Self = animator.gameObject;
        Returns = Self.GetComponent<NavMeshAgent>();
        And = sewnr.transform;
        foreach (Transform freepoint in MoveToCounter.WaypointTaken)
        {
            if (Vector3.Distance(Self.transform.position, freepoint.position) < 2f)
            {
                free = freepoint;
                break;
            }
        }
        if (free != null)
        {
            MoveToCounter.WaypointTaken.Remove(free);
        }

        if (wayTarg != null)
        {
            Returns.SetDestination(wayTarg.position);
            
        }


        foreach (Transform point in And)
        {

            float distance = Vector3.Distance(Self.transform.position, point.position);
            if (distance < walkLess)
            {
                walkLess = distance;
                close = point;
            }
        }
        if (close != null)
        {
            wayTarg = close;
            Returns.SetDestination(wayTarg.position);
            
        }
      
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject Self = GameObject.FindWithTag("Boss");
        if (!Returns.pathPending && Returns.remainingDistance <= Returns.stoppingDistance)
        {
            spawner = close.GetComponent<EnemySpawner>();
            spawner.activeBoss = 0;
            GameObject.Destroy(Self);
        }
    }

}
