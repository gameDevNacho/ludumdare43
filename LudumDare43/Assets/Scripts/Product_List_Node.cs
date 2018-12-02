using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product_List_Node : MonoBehaviour {

	public Text name;
	public Text amount;
	public Text price;
	public Text totalPrice;


	public void setNameText(string newText)
	{
		name.text = newText;
	}

	public void setAmountText(string newText)
	{
		amount.text = newText;
	}

	public void setPriceText(string newText)
	{
		price.text = newText;
	}

	public void setTotalPriceText(string newText)
	{
		totalPrice.text = newText;
	}
}
