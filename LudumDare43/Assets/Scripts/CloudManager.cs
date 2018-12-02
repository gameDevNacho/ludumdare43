using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField]
    private Transform point;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Cloud>())
        {
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, point.position.z);
        }
    }


}
