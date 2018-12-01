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

    //   Transform parent;
    //   Rigidbody rigid;
    //   float forceMultiplier = 100.0f;

    //// Use this for initialization
    //void Start () {
    //       parent = this.GetComponentInParent<Transform>();
    //       rigid = this.GetComponent<Rigidbody>();
    //}

    //// Update is called once per frame
    //void Update () {
    //       rigid.AddForce(parent.forward * -parent.forward.y * forceMultiplier);
    //}
}
