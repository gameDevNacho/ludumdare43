using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BoxComponent : MonoBehaviour, IInteractable {

    public enum Type { Small = 5, Medium = 10, Large = 20 };

    public Type myType;

    public Product myProduct;

    Rigidbody rigid;
    HandlerComponent handler;

    float speed = 10.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        if(handler != null)
        {
            this.transform.position += (handler.GetTransform().position - this.transform.position).normalized * Time.deltaTime * speed;
        }
    }

    public void Interact(HandlerComponent handler)
    {
        this.handler = handler;
        rigid.isKinematic = true;
        handler.SetGrabbedObject(this);
    }

    public void Release()
    {
        handler = null;
        rigid.isKinematic = false;
    }
}
