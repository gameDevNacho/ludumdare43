using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

    bool on = false;

	public void ToggleLights()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<AlarmLight>().ToggleLight();
        }
    }

    public void SwitchOnLights()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<AlarmLight>().SwitchOn();
        }
    }

    public void SwitchOffLights()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<AlarmLight>().SwitchOff();
        }
    }
}
