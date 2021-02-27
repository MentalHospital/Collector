using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(GetCurrentSceneIndex());
    }

    public void LoadMenuScene()
    {
        StartCoroutine(nameof(LoadMenuCoroutine));
    }

    IEnumerator LoadMenuCoroutine()
    {
        yield return new WaitForSeconds(MusicManager.instance.fadeTime);
        SceneManager.LoadScene(0);
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
