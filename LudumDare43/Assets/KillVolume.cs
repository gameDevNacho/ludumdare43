
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(BoxCollider))]
public class KillVolume : MonoBehaviour {



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<BoxComponent>() != null)
        {
            other.gameObject.GetComponent<BoxComponent>().OnKillVolumeEnter();
        }
        else if(other.gameObject.GetComponent<FirstPersonController>())
        {
            PlaneManager.Instance.ResetGame();
        }
    }
}
