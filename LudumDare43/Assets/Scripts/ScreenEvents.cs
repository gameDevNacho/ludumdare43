using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEvents : MonoBehaviour
{
    public enum State { Off, On, Solution }

    [System.Serializable]
    public struct Screen
    {
        public Animator alarm;
        public State state;
    }

    [SerializeField]
    private Screen kgScreen;
    [SerializeField]
    private Screen rotationScreen;
    [SerializeField]
    private AudioClip KGAlarm;
    [SerializeField]
    private AudioClip RotationAlarm;
    [SerializeField]
    private AudioSource kgAudioSource;
    [SerializeField]
    private AudioSource rotationAudioSource;
    [SerializeField]
    private AudioClip solutioned;


    public void SetKGAlarm(State newState)
    {
        kgScreen.alarm.SetTrigger(newState.ToString());
        kgScreen.state = newState;

        switch (newState)
        {
            case State.Off:
                kgAudioSource.Stop();
                kgAudioSource.clip = null;
                break;
            case State.On:
                kgAudioSource.clip = KGAlarm;
                kgAudioSource.Play();
                break;
            case State.Solution:
                kgAudioSource.Stop();
                kgAudioSource.clip = null;
                AudioManager.Instance.PlayDesiredClip(solutioned);
                break;
        }
    }

    public void SetRotationAlarm(State newState)
    {
        rotationScreen.alarm.SetTrigger(newState.ToString());
        rotationScreen.state = newState;

        switch (newState)
        {
            case State.Off:
                rotationAudioSource.Stop();
                rotationAudioSource.clip = null;
                break;
            case State.On:
                rotationAudioSource.clip = RotationAlarm;
                rotationAudioSource.Play();
                break;
            case State.Solution:
                rotationAudioSource.Stop();
                rotationAudioSource.clip = null;
                AudioManager.Instance.PlayDesiredClip(solutioned);
                break;
        }
    }

    public void SetKGAlarm(int index)
    {
        SetKGAlarm((State)index);
    }

    public void SetRotationAlarm(int index)
    {
        SetRotationAlarm((State)index);
    }
}
