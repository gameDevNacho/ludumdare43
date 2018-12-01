using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    [SerializeField]
    private Transform parentTransform;

    private Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        myRigidbody.AddForce(parentTransform.forward * 10f * -parentTransform.forward.y, ForceMode.Force);
    }
}
