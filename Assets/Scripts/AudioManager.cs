using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip gameMusic;

    public void PlaySound(string soundName)
    {
        AudioClip sound = Resources.Load<AudioClip>("sounds\\" + soundName);
        sfxSource.PlayOneShot(sound);
    }

    void Start()
    {
        Instance = this;

        musicSource.clip = gameMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    // 返回静音状态，用于更新按钮UI
    public bool IsMusicMuted()
    {
        return musicSource.mute;
    }

    public bool IsSfxMuted()
    {
        return sfxSource.mute;
    }

}