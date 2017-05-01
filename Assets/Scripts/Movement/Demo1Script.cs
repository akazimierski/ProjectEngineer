using UnityEngine;
using UnityEngine.UI;

public class Demo1Script : MonoBehaviour
{
    public Button myButton;
    public GameObject mapMaker;

    // Use this for initialization
    void Start() {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(SelectLocation);
    }
	
	// Update is called once per frame
	void SelectLocation() {
        mapMaker.GetComponent<MapMaker>().makeMap("42.385367,-71.035441", 17, "depareusa1");
	}
}
