using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HandlerComponent : MonoBehaviour {

    public GameObject handler;

    private GameObject grabbedObject;

    private FixedJoint joint;

    [SerializeField]
    private float maxDistanceGrab = 10.0f;

    public void PickRelease(Ray ray)
    {
        if(grabbedObject == null)
        {
            Pick(ray);
        }

        else
        {
            Release();
        }
    }

    private void Update()
    {
        if(grabbedObject != null)
        {
            if(joint == null)
            {
                joint = this.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = grabbedObject.GetComponent<Rigidbody>();
                if(joint.connectedBody == null)
                {
                    Object.Destroy(joint);
                }
                this.joint.connectedAnchor = grabbedObject.transform.position;
                this.joint.enableCollision = false;
                this.joint.anchor = this.handler.transform.position;
            } 
        }
    }

    private void Pick(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistanceGrab))
        {
            if (hit.transform.gameObject.GetComponent<IInteractable>() != null)
            {
                hit.transform.gameObject.GetComponent<IInteractable>().Interact(this);
            }
        }
    }

    private void Release()
    {
        grabbedObject = null;
    }

    public Transform GetTransform()
    {
        return handler.transform;
    }

    public void SetGrabbedObject(BoxComponent grabbed)
    {
        grabbedObject = grabbed.gameObject;
    }

    public FixedJoint getJoint()
    {
        if(this.joint == null)
        {
            this.joint = this.gameObject.AddComponent<FixedJoint>();
            joint.transform.position = handler.transform.position;
        }
        return this.joint;
    }
}
