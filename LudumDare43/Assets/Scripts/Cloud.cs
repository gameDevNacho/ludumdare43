using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    private float max;
    [SerializeField]
    private float min;

    private float speed;

	void Start ()
    {
        speed = Random.Range(min, max);
	}
	
	void Update ()
    {
        transform.Translate(-transform.forward * speed * Time.deltaTime);	
	}
}
