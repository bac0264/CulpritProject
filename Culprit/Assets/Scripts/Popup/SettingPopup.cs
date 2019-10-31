﻿using UnityEngine;
using System.Collections;

public class SettingPopup : BasePopup
{
    public static SettingPopup instance;
    public GameObject turnOnMusic;
    public GameObject turnOffMusic;
    public GameObject turnOnSound;
    public GameObject turnOffSound;
    public Animator ani;
    private void Awake()
    {
        if (instance == null) instance = this;
        ani = GetComponent<Animator>();
        if (PlayerPrefs.GetInt(KeySave.SOUND, 0) == 0)
        {
            TurnOnSound();
        }
        else
        {
            TurnOffSound();
        }
        if (PlayerPrefs.GetInt(KeySave.MUSIC, 0) == 0)
        {
            TurnOnMusic();
        }
        else
        {
            TurnOffMusic();
        }
    }
    public void RunAniFadeOut()
    {
        ani.Play("FadeOut");
    }
    public override void ShowPopup()
    {
        base.ShowPopup();
        ani.Play("FadeIn");
    }
    public void SoundBtn()
    {
        if(PlayerPrefs.GetInt(KeySave.SOUND) == 1)
        {
            TurnOnSound();
            PlayerPrefs.SetInt(KeySave.SOUND, 0);
        }
        else
        {
            TurnOffSound();
            PlayerPrefs.SetInt(KeySave.SOUND, 1);
        }
    }
    public void MusicBtn()
    {
        if (PlayerPrefs.GetInt(KeySave.MUSIC) == 1)
        {
            TurnOnMusic();
            PlayerPrefs.SetInt(KeySave.MUSIC, 0);
        }
        else
        {
            TurnOffMusic();
            PlayerPrefs.SetInt(KeySave.MUSIC, 1);
        }
    }
    public void TurnOnSound()
    {
        turnOnSound.SetActive(true);
        turnOffSound.SetActive(false);
    }
    public void TurnOffSound()
    {
        turnOnSound.SetActive(false);
        turnOffSound.SetActive(true);
    }
    public void TurnOnMusic()
    {
        turnOnMusic.SetActive(true);
        turnOffMusic.SetActive(false);
    }
    public void TurnOffMusic()
    {
        turnOnMusic.SetActive(false);
        turnOffMusic.SetActive(true);
    }
    public void Language()
    {

    }
}
