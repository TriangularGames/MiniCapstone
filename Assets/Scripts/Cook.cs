using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    [SerializeField] private GameObject timer;
    public float time;
    public Image fill;
    public float max;

    private bool StartTimer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTimer)
        {
            time -= Time.deltaTime;
            fill.fillAmount = time / max;
            if (time < 0) { time = 0; }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected: " + other.gameObject.tag);
        if (other.gameObject.tag == "Food")
        {
            StartTimer = true;
            // Get time from object
            time = 5;
            max = time;
            timer.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            StartTimer = false;
            timer.SetActive(false);
        }
    }
}
