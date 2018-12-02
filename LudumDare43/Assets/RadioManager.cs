using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
	public AudioClip[] allmusic;

	private AudioSource source;
	private int currentSongIndex = 0;
	private int indexLimit;

	private void Start()
	{
		source = GetComponent<AudioSource>();
		source.clip = allmusic[currentSongIndex];
		source.Play();

		indexLimit = allmusic.Length;
	}

	private void Update()
	{
		CheckEndMusic();
	}

	void CheckEndMusic()
	{
		if (!source.isPlaying)
		{
			UpdateAndCheckIndexOutOfBounds();
			source.clip = allmusic[currentSongIndex];
			source.Play();
		}
	}

	void UpdateAndCheckIndexOutOfBounds()
	{
		currentSongIndex++;

		if (currentSongIndex >= indexLimit)
		{
			currentSongIndex = 0;
		}
	}
}
