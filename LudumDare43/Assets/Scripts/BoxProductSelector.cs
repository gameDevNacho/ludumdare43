using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxProductSelector {

    [SerializeField]
    private string boxesPath = "Crates/";
    [SerializeField]
    private string productsPath = "Products/";


    public struct Str_BoxProduct
    {
        public Product product;
        public Mesh boxMesh;
    }

	public Str_BoxProduct GetSuitableBoxProduct(BoxComponent.BoxSize size)
    {
        Str_BoxProduct boxProduct;

        Product[] products = Resources.LoadAll<Product>(productsPath + size.ToString());
        int n = Random.Range(0, products.Length);

        boxProduct.product = products[n];
        boxProduct.boxMesh = boxProduct.product.boxMesh;

        return boxProduct;
    }
}
