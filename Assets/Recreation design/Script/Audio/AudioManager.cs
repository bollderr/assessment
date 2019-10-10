using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    
    // Click
    AudioClip cursor { get; set; }

    // Place
    AudioClip balloon { get; set; }

    // Fall
    AudioClip drop { get; set; }

    // Clear
    AudioClip lineclear { get; set; }
   
    // End
    AudioClip gameover { get; set; }

    AudioSource audioSource1 { get; set; }
    AudioSource audioSource2 { get; set; }

    
    // Singleton pattern
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        audioSource1 = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audioSource2 = GameObject.Find("Canvas").GetComponent<AudioSource>();
        cursor = Resources.Load<AudioClip>("Audio/Cursor");
        balloon = Resources.Load<AudioClip>("Audio/Balloon");
        drop = Resources.Load<AudioClip>("Audio/Drop");
        lineclear = Resources.Load<AudioClip>("Audio/Lineclear");
        gameover = Resources.Load<AudioClip>("Audio/Gameover");
    }

    public void SetAudioSource(bool iscan = true) {
        audioSource1.enabled = audioSource2.enabled = iscan;
    }

    
    // Click
    public void PlayCursor()
    {
        PlayAudio2(cursor);
    }

    
    // Fall
    public void PlayDrop()
    {
        PlayAudio1(drop);
    }

    
    // Place
    public void PlayControl()
    {
        PlayAudio2(balloon);
    }

    
    // Clear
    public void PlayLineClear()
    {
        PlayAudio2(lineclear);
    }

    
    // End
    public void PlayGameOver() {
        PlayAudio2(gameover);
    }


    // Play
    void PlayAudio1(AudioClip clip)
    {
        audioSource1.clip = clip;
        audioSource1.Play();
        //audioSource1.Pause();
    }
    void PlayAudio2(AudioClip clip)
    {
        audioSource2.clip = clip;
        audioSource2.Play();
    }
}
