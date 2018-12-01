using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BoxComponent : MonoBehaviour, IInteractable {

    public enum Type { Small = 5, Medium = 10, Large = 20 };

    public Type myType;

    public Product myProduct;

    [SerializeField]
    private Transform parentTransform;

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
        rigid.AddForce(-parentTransform.right * 10f * -parentTransform.forward.y, ForceMode.Force);
    }



    public void Interact(HandlerComponent handler)
    {
        handler.SetGrabbedObject(this);
        rigid.useGravity = false;
    }

    public void Release()
    {
        rigid.isKinematic = false;
    }
}
