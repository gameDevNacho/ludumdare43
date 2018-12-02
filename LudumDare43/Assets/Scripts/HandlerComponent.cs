using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerComponent : MonoBehaviour {

    public GameObject handler;

    private BoxComponent grabbedObject;

    private FixedJoint joint;

    private BoxComponent currentFocus;


    [SerializeField]
    private Transform shootingPoint;
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

        RaycastHit hit;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red);

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistanceGrab))
        {
            if (currentFocus == null && hit.transform.gameObject.GetComponent<IInteractable>() != null && hit.transform.gameObject.GetComponent<IInteractable>() is BoxComponent)
            {
                BoxComponent box = (BoxComponent)hit.transform.gameObject.GetComponent<IInteractable>();
                currentFocus = box;
                currentFocus.ChangeMaterial(true);
            }

            if(currentFocus != null && currentFocus == grabbedObject)
            {
                currentFocus.ChangeMaterial(false);
            }
        }

        else
        {
            if (currentFocus != null && currentFocus is BoxComponent)
            {
                currentFocus.ChangeMaterial(false);
                currentFocus = null;
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
        currentFocus = null;
        Destroy(joint);
    }

    public void Throw()
    {
        if(grabbedObject != null)
        {
            Destroy(joint);
            grabbedObject.Throw(this);
        }        
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

    public void ShakeBox()
    {
        if(grabbedObject != null)
        {
            grabbedObject.PlayProductSound();
        }
    }
}
