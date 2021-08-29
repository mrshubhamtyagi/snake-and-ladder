using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource music;
    public AudioSource sound;

    [Header("Sounds")]
    public AudioClip pictureSound;
    public AudioClip bubbleSound;
    public AudioClip diceSound;
    public AudioClip congratsSound;


    public bool isSoundOn = true;

    void Start()
    {
        sound.playOnAwake = false;
        sound.loop = false;
        isSoundOn = true;
    }

    public void PlayBubbleSound()
    {
        if (sound.clip == null || !isSoundOn)
            return;

        sound.clip = bubbleSound;
        sound.Play();
    }

    public void PlayDiceSound()
    {
        if (sound.clip == null || !isSoundOn)
            return;

        sound.clip = diceSound;
        sound.Play();
    }

    public void PlayCongratsSound()
    {
        if (sound.clip == null || !isSoundOn)
            return;

        sound.clip = congratsSound;
        sound.Play();
    }

    public void PlayPictureSound()
    {
        if (sound.clip == null || !isSoundOn)
            return;

        sound.clip = pictureSound;
        sound.Play();
    }


    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
    }

    public void ToggleMusic()
    {
        if (music.clip == null)
            return;

        if (music.isPlaying)
            music.Pause();
        else
            music.Play();
    }
}
