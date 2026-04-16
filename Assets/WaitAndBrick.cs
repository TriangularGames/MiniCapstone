using System.Collections.Generic;
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
    private GameObject FoodsIsSpawned;
    private GameObject theChosenFood;
    private List<GameObject> MOREFOODS = new List<GameObject>();
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject Enemy = GameObject.FindWithTag("Boss");
        GameObject[] allTheFoods = Resources.LoadAll<GameObject>("Food");
        foreach (GameObject food in allTheFoods)
        {
            if (food.CompareTag("Food"))
            {
                MOREFOODS.Add(food);
            }
        }

        int seed = System.DateTime.Now.Millisecond + Enemy.GetInstanceID();
        UnityEngine.Random.InitState(seed);
        int randomFoodsStuff = UnityEngine.Random.Range(0, MOREFOODS.Count);
        theChosenFood = MOREFOODS[randomFoodsStuff];
        Vector3 SpawnDaFoodsStuff = Enemy.transform.position + Vector3.up * 3.5f;
        FoodsIsSpawned = GameObject.Instantiate(theChosenFood, SpawnDaFoodsStuff, Quaternion.identity);
        FoodsIsSpawned.tag = "Untagged";
        FoodsIsSpawned.transform.localScale = new Vector3(5f, 5f, 5f);
        if (theChosenFood.name == "GrabbablePizza")
        {
            FoodsIsSpawned.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        if (theChosenFood.name == "GrabbableSteak")
        {
            FoodsIsSpawned.transform.rotation = Quaternion.Euler(90, 0, 0);
            FoodsIsSpawned.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
        if (theChosenFood.name == "GrabbableCookedEgg")
        {
            FoodsIsSpawned.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        Rigidbody NOMOREFOODGRAVITY = FoodsIsSpawned.GetComponent<Rigidbody>();
        if (NOMOREFOODGRAVITY != null)
        {
            NOMOREFOODGRAVITY.useGravity = false;
            NOMOREFOODGRAVITY.isKinematic = true;
        }
    }   

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject Enemy = GameObject.FindWithTag("Boss");
        GameObject Brick = GameObject.FindWithTag("Brick");
        Be = Enemy.GetComponent<NavMeshAgent>();
        CapsuleCollider FoodCollider = Enemy.GetComponentInChildren<CapsuleCollider>();
        Collider[] fits = Physics.OverlapSphere(Enemy.transform.position + Vector3.up, FoodCollider.radius + 0.5f);
        foreach (var FoodC in fits)
        {
            if (FoodC.CompareTag("Food"))
            {
                GameObject.Destroy(FoodC.gameObject);
                animator.SetTrigger("Die");
            }
        }
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
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Destroy(FoodsIsSpawned);
    }
}

