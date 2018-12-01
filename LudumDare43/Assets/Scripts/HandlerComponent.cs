using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HandlerComponent : MonoBehaviour {

    public GameObject handler;

    private BoxComponent grabbedObject;

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
                joint = handler.AddComponent<FixedJoint>();
                joint.connectedBody = grabbedObject.GetComponent<Rigidbody>();
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
        grabbedObject.Release(this);
        grabbedObject = null;
        Destroy(joint);
    }

    public void Throw()
    {
        if(grabbedObject != null)
            grabbedObject.Throw(this);
    }

    public Transform GetTransform()
    {
        return handler.transform;
    }

    public void SetGrabbedObject(BoxComponent grabbed)
    {
        grabbedObject = grabbed;
    }

    public FixedJoint getJoint()
    {
        if(this.joint == null)
        {
            this.joint = handler.AddComponent<FixedJoint>();
            joint.transform.position = handler.transform.position;
        }
        return this.joint;
    }
}
