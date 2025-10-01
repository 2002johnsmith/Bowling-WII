using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; 

    [Header("Música")]
    public AudioClip music1;  
    public AudioClip music2;  

    [Header("Volumen (1-10)")]
    [Range(1, 10)]
    public int volumeLevel = 5; 

    private AudioSource audioSource;
    private bool playFirstMusic = true; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        audioSource.volume = Mathf.Clamp01(volumeLevel / 10f);

        PlayNextMusic();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextMusic();
        }
    }

    private void PlayNextMusic()
    {
        audioSource.clip = playFirstMusic ? music1 : music2;
        audioSource.Play();
        playFirstMusic = !playFirstMusic; 
    }
}
