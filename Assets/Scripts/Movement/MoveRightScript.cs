using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveRightScript : MonoBehaviour
{

    public Button myButton;
    public GameObject userObj;
    CharacterController charContr;
    // Use this for initialization
    void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(Movement);
        charContr = userObj.GetComponent<CharacterController>();
    }

    public void Movement()
    {
        charContr.Move(Vector3.left * 0.5f);
    }
}
