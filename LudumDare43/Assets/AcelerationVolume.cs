using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcelerationVolume : MonoBehaviour {


    private void OnTriggerExit(Collider other)
    {
        BoxComponent box = other.GetComponent<BoxComponent>();
        if (box != null)
        {
            box.AddSuccionAceleration();
        }
    }
}
