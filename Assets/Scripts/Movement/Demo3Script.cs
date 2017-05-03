using UnityEngine;
using UnityEngine.UI;

public class Demo3Script : MonoBehaviour
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
        mapMaker.GetComponent<MapMaker>().makeMap("53.0817322,14.3644296", 16, "depare");
        var dialog = GameObject.Find("DialogLocation");
        dialog.SetActive(false);
    }
}
