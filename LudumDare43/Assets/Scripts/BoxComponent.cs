using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BoxComponent : MonoBehaviour, IInteractable {

    public enum Type { Small = 5, Medium = 10, Large = 20 };

    public Type myType;

    public Product myProduct;

    Rigidbody rigid;

    float speed = 10.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }



    public void Interact(HandlerComponent handler)
    {
        this.transform.SetPositionAndRotation(handler.GetTransform().position, handler.GetTransform().rotation);
        handler.SetGrabbedObject(this);
    }

    public void Release(HandlerComponent handler)
    {
        handler.SetGrabbedObject(null);
    }
}
