using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using

[RequireComponent(typeof(BoxCollider))]
public class KillVolume : MonoBehaviour {

    BoxCollider collider;

    private void Awake()
    {
        collider = this.GetComponent<BoxCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }
}
