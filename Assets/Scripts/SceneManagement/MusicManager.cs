using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource musicSource;
    public List<AudioClip> musicLib;

    public float volume = .1f;
    public float fadeTime = .25f;

    float tmp = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        volume = PlayerPrefs.GetFloat("Music volume");

        musicSource = GetComponent<AudioSource>();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            musicSource.volume = volume;
            musicSource.clip = musicLib[0];
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
                //musicSource.PlayOneShot(musicLib[Random.Range(0, musicLib.Count)]);
            }
        }
        if (level == 0)
        {
            var menuManager = FindObjectOfType<UIManagerInMenu>();
            menuManager.musicVolume.value = (int)(volume * 500f);
        }
    }

    public void FadeOut()
    {
        StartCoroutine(nameof(FadeOutCoroutine));
    }

    public IEnumerator FadeOutCoroutine()
    {
        float startVolume = musicSource.volume;
        float timeStep = 0.05f;
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * (timeStep / fadeTime);
            musicSource.volume = Mathf.Clamp01(musicSource.volume);
            yield return new WaitForSeconds(timeStep);
        }
        musicSource.Stop();
    }

    public void UpdateVolume(float value)
    {
        volume = value / 500f;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Music volume", volume);
    }

    public void Mute()
    {
        if (volume < 1e-3)
        {
            volume = tmp;
        }
        else
        {
            tmp = volume;
            volume = 0f;
        }
    }

    public void Play(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
}
