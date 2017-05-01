using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoomOutScript : MonoBehaviour
{
    public Button myButton;
    public GameObject userObj;
    Transform charContr;
    // Use this for initialization
    void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(Movement);
        charContr = userObj.GetComponent<Transform>();
    }

    public void Movement()
    {
        charContr.Translate(Vector3.back * 0.2f, Space.Self);
    }
}
