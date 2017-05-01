using UnityEngine;
using UnityEngine.UI;

public class Demo2Script : MonoBehaviour
{
    public Button myButton;
    public GameObject mapMaker;

    // Use this for initialization
    void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(SelectLocation);
    }

    // Update is called once per frame
    void SelectLocation()
    {
        mapMaker.GetComponent<MapMaker>().makeMap("40.893774,-73.923017", 14, "depareusa");
    }
}
