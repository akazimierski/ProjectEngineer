using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveLeftScript : MonoBehaviour
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
        if (Mathf.Abs(charContr.transform.position.x + Vector3.right.x * 0.5f) < 3.0f
            && Mathf.Abs(charContr.transform.position.z + Vector3.right.z * 0.5f) < 3.0f)
        {
            charContr.Move(Vector3.right * 0.5f);
        }
    }
}
