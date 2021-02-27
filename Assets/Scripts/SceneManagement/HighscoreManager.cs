using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager instance;
    public static int highscore;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        Init();
        DontDestroyOnLoad(this.gameObject);
        highscore = PlayerPrefs.GetInt("highscore");
    }
    
    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            Init();
        }
    }

    void Init()
    {
        var menuManager = FindObjectOfType<UIManagerInMenu>();
        menuManager.UpdateHighscore(highscore);
        // сохранять на этом моменте?
        menuManager.OnExit +=
            () => {
                SaveHighscore();
                menuManager.UpdateHighscore(highscore);
            };
        menuManager.ClearHS += 
            () => {
                PlayerPrefs.SetInt("highscore", 0);
                highscore = 0;
                menuManager.UpdateHighscore(highscore);
            };
    }

    public static void SaveHighscore()
    {
        if (PlayerPrefs.GetInt("highscore") < highscore)
            PlayerPrefs.SetInt("highscore", highscore);
    }


    private void OnDestroy()
    {
        SaveHighscore();
    }
}
