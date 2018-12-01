using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public enum Type { Small = 5, Medium = 10, Large = 20 };

    private int weight;

    public Type myType;

    public Product myProduct;

    private AudioSource myAudioSource;

    void Start ()
    {
        weight = (int)myType;

        Debug.Log(weight);
        Debug.Log(myProduct.nameText);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = myProduct.shakeSound;

        myAudioSource.Play();
    }


    void Update ()
    {
		
	}
}
