using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(FirstPersonController))]
[RequireComponent (typeof(HandlerComponent))]
public class MouseInput : MonoBehaviour {

    private HandlerComponent handler;
    private FirstPersonController playerController;

    private void Awake()
    {
        handler = this.GetComponent<HandlerComponent>();
        playerController = GetComponent<FirstPersonController>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerController.GetCameraRay();
            handler.PickRelease(ray);
        }
	}
}
