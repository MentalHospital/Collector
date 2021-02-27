using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManagerInMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject menuPanel;

    public event Action OnExit;
    public event Action ClearHS;


    public TextMeshProUGUI highscoreText;

    public Button playButton;
    public Button optionsBackButton;


    public Slider soundVolume;
    public Slider musicVolume;

    public Button muteSoundButton;
    public Button muteMusicButton;

    private void Awake()
    {
        UpdateHighscore(PlayerPrefs.GetInt("highscore"));
    }

    private void Start()
    {
        playButton.onClick.AddListener(SceneLoader.instance.LoadGameScene);
        optionsBackButton.onClick.AddListener(MusicManager.instance.SaveVolume);
        optionsBackButton.onClick.AddListener(SoundManager.instance.SaveVolume);
        muteSoundButton.onClick.AddListener(SoundManager.instance.Mute);
        muteMusicButton.onClick.AddListener(MusicManager.instance.Mute);
        muteSoundButton.onClick.AddListener(UpdateSoundSlider);
        muteMusicButton.onClick.AddListener(UpdateMusicSlider);

        soundVolume.onValueChanged.AddListener(SoundManager.instance.UpdateVolume);
        musicVolume.onValueChanged.AddListener(MusicManager.instance.UpdateVolume);
        LoadSliderValues();
    }

    public void ExitGame()
    {
        OnExit();
        Application.Quit();
    }

    void LoadSliderValues()
    {
        UpdateMusicSlider();
        UpdateSoundSlider();
    }

    void UpdateSoundSlider()
    {
        soundVolume.value = Mathf.RoundToInt(SoundManager.instance.volume * 100f);
    }

    void UpdateMusicSlider()
    {
        musicVolume.value = Mathf.RoundToInt(MusicManager.instance.volume * 500f);
    }

    public void UpdateHighscore(int value)
    {
        if (value != 0)
            highscoreText.text = "Your highscore\n" + PlayerPrefs.GetInt("highscore");
        else
            highscoreText.text = "Your highscore\n" + 0;
    }

    public void ClearHighscore()
    {
        ClearHS();
    }

    public void ToggleOptions()
    {
        if (optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
        else
        {
            menuPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }
    }
}
