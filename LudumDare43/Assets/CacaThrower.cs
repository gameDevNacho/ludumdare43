using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacaThrower : MonoBehaviour {

    public GameObject[] cacas;
    int index;
    GameObject current;

    [SerializeField]
    private float minTime = 2.00f;
    [SerializeField]
    private float maxTime = 4.00f;

    private float currentTime = 0.00f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(currentTime <= 0.00f)
        {
            LaunchObject();
            currentTime = Random.Range(minTime, maxTime);
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void LaunchObject()
    {
        index = Random.Range(0, cacas.Length);
        current = Instantiate(cacas[index], this.transform);
        current.transform.position = this.transform.position;
        current.GetComponent<BoxComponent>().AddSuccionAceleration();
    }
}
