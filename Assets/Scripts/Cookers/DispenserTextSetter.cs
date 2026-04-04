using TMPro;
using UnityEngine;

public class DispenserTextSetter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string replace = "FoodDispenser";
        string str = gameObject.name;
        string result = str.Replace(replace, "");
        text.text = result;
    }
}
