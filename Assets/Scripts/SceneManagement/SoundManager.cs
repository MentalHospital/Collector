using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    PickUpManagerRedux pickUpManager;

    public float volume = .5f;

    float tmp = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        volume = PlayerPrefs.GetFloat("Sound volume");

        audioSource = GetComponent<AudioSource>();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            foreach (var audio in FindObjectsOfType<AudioSource>())
            {
                if (!audio.gameObject.CompareTag("Music"))
                    audio.volume = volume;
            }
        }
        if (level == 0)
        {
            var menuManager = FindObjectOfType<UIManagerInMenu>();
            menuManager.soundVolume.value = (int)(audioSource.volume * 100f);
        }
    }

    public void UpdateVolume(float value)
    {
        volume = value / 100f;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Sound volume", volume);
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
        audioSource.PlayOneShot(clip);
    }
}
