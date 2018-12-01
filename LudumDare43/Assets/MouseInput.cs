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
        Ray rayDebug = playerController.GetCameraRay();
        Debug.DrawRay(rayDebug.origin, rayDebug.direction);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerController.GetCameraRay();
            Debug.DrawRay(ray.origin, ray.direction);
            handler.PickRelease(ray);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            handler.Throw();
        }
	}
}
