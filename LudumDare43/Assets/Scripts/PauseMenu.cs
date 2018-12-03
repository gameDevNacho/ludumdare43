using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool GamePaused = false;

    [SerializeField]
    FirstPersonController fps;

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
    }

	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GamePaused = true;
        fps.ToggleLock();
    }


	public void Quit()
	{
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
	}

}
