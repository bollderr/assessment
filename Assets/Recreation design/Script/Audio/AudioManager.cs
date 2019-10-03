using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    /// <summary>
    /// Click
    /// </summary>
    AudioClip cursor { get; set; }
    /// <summary>
    /// Place
    /// </summary>
    AudioClip balloon { get; set; }
    /// <summary>
    /// Fall
    /// </summary>
    AudioClip drop { get; set; }
    /// <summary>
    /// Clear
    /// </summary>
    AudioClip lineclear { get; set; }
    /// <summary>
    /// End
    /// </summary>
    AudioClip gameover { get; set; }

    AudioSource audioSource1 { get; set; }
    AudioSource audioSource2 { get; set; }

    /// <summary>
    /// Singleton pattern
    /// </summary>
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

    /// <summary>
    /// Click
    /// </summary>
    public void PlayCursor()
    {
        PlayAudio2(cursor);
    }

    /// <summary>
    /// Fall
    /// </summary>
    public void PlayDrop()
    {
        PlayAudio1(drop);
    }

    /// <summary>
    /// Place
    /// </summary>
    public void PlayControl()
    {
        PlayAudio2(balloon);
    }

    /// <summary>
    /// Clear
    /// </summary>
    public void PlayLineClear()
    {
        PlayAudio2(lineclear);
    }

    /// <summary>
    /// End
    /// </summary>
    public void PlayGameOver() {
        PlayAudio2(gameover);
    }

    /// <summary>
    /// Play
    /// </summary>
    /// <param name="clip">Audio source</param>
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
