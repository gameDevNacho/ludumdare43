using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacaThrower : MonoBehaviour {

    public GameObject[] cacas;
    int index;
    GameObject current;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LaunchObject()
    {
        index = Random.Range(0, cacas.Length);
        current = Instantiate<GameObject>(cacas[index]);
        current.GetComponent<BoxComponent>().AddSuccionAceleration();
    }
}
