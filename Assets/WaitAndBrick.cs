using UnityEngine;
using UnityEngine.AI;

public class WaitAndBrick : StateMachineBehaviour
{
    public NavMeshAgent Be;
    private float thrown = 30f;
    private Vector3 spawnPosition;
    private float timer = 0f;
    private float timer2 = 0f;
    private float brickThrow = 7f;


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject Enemy = GameObject.FindWithTag("Boss");
        GameObject Brick = GameObject.FindWithTag("Brick");
        Be = Enemy.GetComponent<NavMeshAgent>();
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer > brickThrow)
        {
            spawnPosition = Enemy.transform.position + (Enemy.transform.forward * 1.2f) + (Vector3.up * 1.5f);

            GameObject brick = GameObject.Instantiate(Brick, spawnPosition, Enemy.transform.rotation);

            Rigidbody rb = brick.GetComponent<Rigidbody>();

            rb.AddForce(Enemy.transform.forward * thrown, ForceMode.Impulse);
            timer = 0f;
        }
        if (timer2 > 40f)
        {
            timer2 = 0f;
            animator.SetTrigger("Die");
        }
    }
}

