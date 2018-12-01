using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerComponent : MonoBehaviour {

    public Transform handler;

    private BoxComponent grabbedObject;

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
        grabbedObject.Release();
        grabbedObject = null;
    }

    public Transform GetTransform()
    {
        return handler;
    }

    public void SetGrabbedObject(BoxComponent grabbed)
    {
        grabbedObject = grabbed;
    }
}
