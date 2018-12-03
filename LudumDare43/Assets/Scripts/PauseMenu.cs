using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {

	public static bool GamePaused = false;

    [SerializeField]
    FirstPersonController fps;
    [SerializeField]
    private AudioMixer audioMixer;

	public GameObject pauseMenuUI;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GamePaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}


	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GamePaused = false;
        fps.ToggleLock();
        Debug.Log("hola");
        audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[1] { audioMixer.FindSnapshot("Snapshot") }, new float[1] { 1f }, 0f);
    }

	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GamePaused = true;
        fps.ToggleLock();
        audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[1] { audioMixer.FindSnapshot("MuteEverythingButMusic") }, new float[1] { 1f }, 0f);
    }


	public void Quit()
	{
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
	}

}
