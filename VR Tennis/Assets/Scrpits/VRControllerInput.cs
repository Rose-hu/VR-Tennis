using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerInput : MonoBehaviour
{
    public GameObject saveCanvas;
    public GameObject save;
    public GameObject launch;

    private void Update()
    {
        // 右手A，发球
        if (OVRInput.GetDown(OVRInput.Button.One)) // 如果按下了手柄上的A/X按钮（取决于手柄的布局）
        {
            // 点击按钮
            launch.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }

        // // 右手B，储存
        // if (OVRInput.GetDown(OVRInput.Button.One(OVRInput.Controller.RTouch))) // 如果按下了手柄上的A/X按钮（取决于手柄的布局）
        // {
        //     // 点击按钮
        //     launch.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        // }
    }
}

