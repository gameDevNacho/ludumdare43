using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    #region Singleton

    public static AudioManager Instance { get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    #endregion

    public AudioSource[] allSources;

    public void PlayDesiredClip(AudioClip clipToPlay)
    {
        AudioSource sourceToUse;

        for (int i = 0; i < allSources.Length; i++)
        {
            if(allSources[i].clip == null)
            {
                sourceToUse = allSources[i];
                break;
            }
        }
  
        if(sourceToUse != null)
        {
            sourceToUse.clip = clipToPlay;
            sourceToUse.Play();

            StartCorrutine(RemoveClip(sourceToUse, sourceToUse.clip.length));
        }
        
    }


    private IEnumerator RemoveClip(AudioSource sourceToStop, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        sourceToStop.clip = null;

        Debug.Log("DOne");

        
    }


}
