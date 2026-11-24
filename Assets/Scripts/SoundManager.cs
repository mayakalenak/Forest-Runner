using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Game Audio")]
    public AudioClip gameMusic;

    [Header("Sound Effects")]
    public AudioClip obstacleHit;
    public AudioClip gemCollect;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && gameMusic != null)
        {
            if (musicSource.clip != gameMusic)
            {
                musicSource.clip = gameMusic;
            }

            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void ToggleMusic()
    {
        if (musicSource != null)
        {
            musicSource.mute = !musicSource.mute;
        }
    }
}