using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public enum Type { Small = 5, Medium = 10, Large = 20 };

    public Type myType;

    public Product myProduct;
}
