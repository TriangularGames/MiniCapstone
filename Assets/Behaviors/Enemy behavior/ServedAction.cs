using Fusion;
using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Served", story: "[Self] is waiting to [be] served", category: "Action", id: "ee1edcc0c39de1a815d183327109b318")]
public partial class ServedAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Be;
    private List<GameObject> MOREFOODS = new List<GameObject>();

    private float thrown = 30f;
    private Vector3 spawnPosition;
    private float timer = 0f;  
    private float timer2 = 0f;
    private float brickThrow = 7f;
    private GameObject FoodsIsSpawned;
    private GameObject theChosenFood;

    protected override Status OnStart()
    {
        GameObject[] allTheFoods = Resources.LoadAll<GameObject>("Food");
        foreach (GameObject food in allTheFoods)
        {
            if (food.CompareTag("Food"))
            {
                MOREFOODS.Add(food);
            }
        }
      
        int seed = System.DateTime.Now.Millisecond + Self.Value.GetInstanceID();
        UnityEngine.Random.InitState(seed);
        int randomFoodsStuff = UnityEngine.Random.Range(0, MOREFOODS.Count);
        theChosenFood = MOREFOODS[randomFoodsStuff];  
        Vector3 SpawnDaFoodsStuff = Self.Value.transform.position + Vector3.up * 3.5f;
        FoodsIsSpawned = GameObject.Instantiate(theChosenFood, SpawnDaFoodsStuff, Quaternion.identity);
        FoodsIsSpawned.tag = "Untagged";
        FoodsIsSpawned.transform.localScale = new Vector3(5f, 5f, 5f);
        if(theChosenFood.name == "GrabbablePizza")
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
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        CapsuleCollider FoodCollider = Self.Value.GetComponentInChildren<CapsuleCollider>();
        Collider[] fits = Physics.OverlapSphere(Self.Value.transform.position + Vector3.up, FoodCollider.radius + 0.5f);
            foreach (var FoodC in fits)
            {
                if (FoodC.CompareTag("Food"))
                {
                    GameObject.Destroy(FoodC.gameObject);
                    return Status.Success;
                }
            }
        
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer > brickThrow)
        {
            spawnPosition = Self.Value.transform.position + (Self.Value.transform.forward * 1.2f) + (Vector3.up * 1.5f);

            GameObject brick = GameObject.Instantiate(Be.Value, spawnPosition, Self.Value.transform.rotation);

            Rigidbody rb = brick.GetComponent<Rigidbody>();

            rb.AddForce(Self.Value.transform.forward * thrown, ForceMode.Impulse);
            timer = 0f;
            return Status.Running;
        }
        if (timer2 > 40f)
        {
            timer2 = 0f;
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        GameObject.Destroy(FoodsIsSpawned);
    }
}

