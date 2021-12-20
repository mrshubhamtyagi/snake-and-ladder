using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource music;
    public AudioSource sound;

    [Header("Sounds")]
    public AudioClip bubbleSound;
    public AudioClip diceSound;
    public AudioClip congratsSound;

    [Header("Picture Sound")]
    public AudioClip[] pictureSound;

    public bool isSoundOn = true;
    public bool isMusicOn = true;

    void Start()
    {
        sound.playOnAwake = false;
        sound.loop = false;
        isSoundOn = true;
        isMusicOn = true;
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

    public void PlayPictureSound(int _index)
    {
        if (sound.clip == null || !isSoundOn || pictureSound[_index] == null || _index > pictureSound.Length)
            return;

        sound.clip = pictureSound[_index];
        sound.Play();
    }

    public void StopPictureSound()
    {
        sound.Stop();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
    }

    public void ToggleMusic()
    {
        if (music.clip == null)
            return;

        isMusicOn = !isMusicOn;

        if (music.isPlaying)
            music.Pause();
        else
            music.Play();
    }

    public void PauseAndPlayMusic()
    {
        if (music.clip == null || !isMusicOn)
            return;

        if (music.isPlaying)
            music.Pause();
        else
            music.Play();
    }


    public float GetPictureClipLength(int _index)
    {
        if (pictureSound[_index] == null || _index > pictureSound.Length)
            return 3;
        else
            return pictureSound[_index].length;
    }
}
