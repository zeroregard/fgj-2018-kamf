using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AudioManager : MonoBehaviour
{
    public AudioClip IntroSong;
    public AudioClip ThemeSong;
    public AudioSource AudioPlayerOne;
	public AudioSource AudioPlayerTwo;

    void Start()
    {
        AudioPlayerOne.clip = IntroSong;
        AudioPlayerOne.loop = false;
        AudioPlayerOne.Play();
		AudioPlayerTwo.clip = ThemeSong;
		AudioPlayerTwo.loop = true;
		AudioPlayerTwo.PlayScheduled(AudioSettings.dspTime + IntroSong.length);
    }
}
