
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KillVolume : MonoBehaviour {



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IKvolume>()!=null)
        {
            other.gameObject.GetComponent<IKvolume>().OnKillVolumeEnter();
        }
    }
}
