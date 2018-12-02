
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            handler.PickRelease(ray);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            handler.Throw();
        }
    }
}
