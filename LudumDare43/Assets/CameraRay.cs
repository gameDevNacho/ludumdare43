using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{

	public Ray GetRay()
    {
        return new Ray(transform.position, transform.forward);
    }
}
