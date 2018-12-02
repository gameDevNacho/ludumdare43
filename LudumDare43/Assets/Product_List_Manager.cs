using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product_List_Manager : MonoBehaviour {

	public Product_List_Node nodeRef;


	public Product[] products;

	private Dictionary<Product, int> productQuantity;


	private void Start()
	{
		productQuantity = new Dictionary<Product, int>();

		for (int i = 0; i < products.Length; i++)
		{
			if(productQuantity.ContainsKey(products[i]))
			{
				productQuantity[products[i]]++;
			}
			else
			{
				productQuantity.Add(products[i], 1);
			}
		}

		foreach (KeyValuePair<Product, int> item in productQuantity)
		{
			Product_List_Node tempRef = Instantiate(nodeRef, this.transform);
			tempRef.setNameText(item.Key.name);
			tempRef.setAmountText(item.Value.ToString());
			tempRef.setPriceText(item.Key.price.ToString());
			tempRef.setTotalPriceText((item.Key.price * item.Value).ToString());
		}
	}

}
