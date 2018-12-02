using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO_Products")]
public class Product : ScriptableObject
{

    public string nameText;
    public int price;
    public AudioClip shakeSound;
    public Mesh boxMesh;

}
