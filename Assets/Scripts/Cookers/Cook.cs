using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    [SerializeField] private GameObject timer;
    public float time;
    public Image fill;
    public float max;
    public float delay = 0.5f;

    [SerializeField] private string Type;

    private Collider obj = null;

    private bool StartTimer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTimer && delay <= 0.0f)
        {
            if (!timer.activeSelf)
            {
                timer.SetActive(true);
            }

            time -= Time.deltaTime;
            fill.fillAmount = time / max;
            if (time < 0)
            {
                time = 0;

                // If Obj can Change
                if (obj != null)
                {                    
                    obj.gameObject.transform.parent.GetComponent<Change>().ChangeObj();
                    timer.SetActive(false);
                }
                else
                {
                    timer.SetActive(false);
                    StartTimer = false;
                }
            }
        }
        else
        {
            delay -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected: " + other.gameObject.tag);
        if (other.gameObject.tag == Type)
        {
            obj = other;
            StartTimer = true;
            // Get time from object
            time = 5;
            max = time;
            timer.SetActive(true);
        }
        if (other.gameObject.tag == "Food")
        {
            obj = null;
            //timer.GetComponent<Image>().color = fill.color;
            fill.color = Color.red;
            StartTimer = true;
            time = 5;
            delay = 2.0f;
            max = time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Type || other.gameObject.tag == "Food")
        {
            obj = null;
            StartTimer = false;
            timer.SetActive(false);
        }
    }
}
