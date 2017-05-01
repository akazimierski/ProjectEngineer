using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaterVisibilityScript : MonoBehaviour
{
    public Button myButton;
    public GameObject waterObj;
    
    // Use this for initialization
    void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(ChangeState);
    }

    public void ChangeState()
    {
        var waterScript = waterObj.GetComponent<UnityStandardAssets.Water.Water>();
        
        if (waterObj.activeSelf)
        {
            var currentMode = waterScript.waterMode;

            if (currentMode == UnityStandardAssets.Water.Water.WaterMode.Reflective)
            {
                waterScript.waterMode = UnityStandardAssets.Water.Water.WaterMode.Refractive;
            }

            if (currentMode == UnityStandardAssets.Water.Water.WaterMode.Refractive)
            {
                waterObj.SetActive(false);
            }
        }
        else
        {
            waterObj.SetActive(true);
            waterScript.waterMode = UnityStandardAssets.Water.Water.WaterMode.Reflective;
        }
        

    }
}
