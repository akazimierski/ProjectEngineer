using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonOptionsScript : MonoBehaviour
{
    public Button myButton;
    public GameObject dialogObj;

    // Use this for initialization
    void Start () {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(Action);
    }
	
	// Update is called once per frame
	void Action () {
        if (dialogObj.activeSelf)
        {
            dialogObj.SetActive(false);
        }
        else
        {
            dialogObj.SetActive(true);
        }
	}
}
