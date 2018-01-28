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
    
    // I couldn't find any List.shuffle method around
    // Sombedy, please get rid of that madness
    private void ShuffleKnockouts() {
        int n = knockouts.Count;
        while (n > 1) {
            int k = (Random.Range(0, n));
            n--;
            AudioClip value = knockouts[k];
            knockouts[k] = knockouts[n];
            knockouts[n] = value;
        }
    }

    void Start()
    {   
        AudioPlayerOne.volume = 1.0f;
        AudioPlayerTwo.volume = 1.0f;
        announcer.volume = 0.7f;
        PlayMusic();
        ShuffleKnockouts();
    }
    
    void PlayMusic() {
        AudioPlayerOne.clip = IntroSong;
        AudioPlayerOne.loop = false;
        AudioPlayerOne.Play();
        AudioPlayerTwo.clip = ThemeSong;
        AudioPlayerTwo.loop = true;
        AudioPlayerTwo.PlayScheduled(AudioSettings.dspTime + IntroSong.length);    
    }
    
    public void AnnounceDeath(int playerId)
    {
        print("YOU'VE GOT KNOCKED OUT");
        announcer.clip = knockouts[playerId];
        announcer.loop = false;
        announcer.Play();
  
    }
}
