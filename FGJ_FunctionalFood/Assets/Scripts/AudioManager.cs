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
    
    public List<AudioClip> knockouts;
    
    public AudioSource announcer;

    void Start()
    {   
        AudioPlayerOne.volume = 1.0f;
        AudioPlayerTwo.volume = 1.0f;
        announcer.volume = 0.7f;
        PlayMusic();
    }
    
    void PlayMusic() {
        AudioPlayerOne.clip = IntroSong;
        AudioPlayerOne.loop = false;
        AudioPlayerOne.Play();
        AudioPlayerTwo.clip = ThemeSong;
        AudioPlayerTwo.loop = true;
        AudioPlayerTwo.PlayScheduled(AudioSettings.dspTime + IntroSong.length);    
    }
    
    private AudioClip randomKnockout()
    { 
        return knockouts[Random.Range(0, 4)];
    }
    
    public void AnnounceDeath()
    {
        print("YOU'VE GOT KNOCKED OUT");
        announcer.clip = randomKnockout();
        announcer.loop = false;
        announcer.Play();
  
    }
}
