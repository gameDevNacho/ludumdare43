using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBox : MonoBehaviour {

    public GameObject boxPrefab;
    BoxProductSelector boxProduct;
    BoxProductSelector.Str_BoxProduct str_boxProduct;
	// Use this for initialization
	void Start () {
        boxProduct = new BoxProductSelector();
        GameObject box = Instantiate<GameObject>(boxPrefab);
        BoxComponent boxComponent = box.GetComponent<BoxComponent>();
        str_boxProduct = boxProduct.GetSuitableBoxProduct(boxComponent.mySize);
        boxComponent.InitializeBox(str_boxProduct);       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
