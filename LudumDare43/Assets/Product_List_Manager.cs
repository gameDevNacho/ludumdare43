﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product_List_Manager : MonoBehaviour {

    public static Product_List_Manager Instance { get; private set; }

	public Product_List_Node nodeRef;
    [SerializeField]
    private GameObject Albaran;


	public Product[] products;

	private Dictionary<Product, int> productQuantity;

    [SerializeField]
    private Text totalText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void Initialize(Product[] products)
    {
        Albaran.SetActive(true);

        productQuantity = new Dictionary<Product, int>();

        for (int i = 0; i < products.Length; i++)
        {
            if (productQuantity.ContainsKey(products[i]))
            {
                productQuantity[products[i]]++;
            }
            else
            {
                productQuantity.Add(products[i], 1);
            }
        }

        int total = 0;

        foreach (KeyValuePair<Product, int> item in productQuantity)
        {
            Product_List_Node tempRef = Instantiate(nodeRef, this.transform);
            tempRef.setNameText(item.Key.name);
            tempRef.setAmountText(item.Value.ToString());
            tempRef.setPriceText(item.Key.price.ToString());
            tempRef.setTotalPriceText((item.Key.price * item.Value).ToString());
            total += item.Key.price * item.Value;
        }

        totalText.text = total.ToString();

        Invoke("Resetgame", 7f);
    }

    private void Resetgame()
    {
        PlaneManager.Instance.ResetGame();
    }
}
