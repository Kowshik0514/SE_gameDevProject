using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    // Array of sounds
    public AudioClip[] audioClips;
    public AudioClip[] SoundEffect;
    public AudioClip[] Music;

    public float Audiovolume = 1.0f;
    public float EffectVolume = 1.0f;
    public float MusicVolume = 1.0f;

    private AudioSource musicSource;

    private void Start()
    {
        if(volumeManager.Instance != null)
        {
            Audiovolume = volumeManager.Instance.soundVolume;
            EffectVolume = volumeManager.Instance.effectsVolume;
            MusicVolume = volumeManager.Instance.musicVolume;
            musicSource.volume = MusicVolume;
        }
        PlayGlobalMusic(0);
    }
    private void Update()
    {
        if (volumeManager.Instance != null)
        {
            Audiovolume = volumeManager.Instance.soundVolume;
            EffectVolume = volumeManager.Instance.effectsVolume;
            MusicVolume = volumeManager.Instance.musicVolume;
            musicSource.volume = MusicVolume;
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep across scenes
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
            musicSource.volume = MusicVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayGlobalMusic(int index)
    {
        if(index >= 0 && index < Music.Length)
        {
            musicSource.clip = Music[index];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Invalid sound index: {index}");
        }
    }
    public void stopGlobalMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
    public void PlayAudioByIndex(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            AudioSource.PlayClipAtPoint(audioClips[index], Camera.main.transform.position, Audiovolume);
        }
        else
        {
            Debug.LogWarning($"Invalid sound index: {index}");
        }
    }

    public void EffectSoundByIndex(int index)
    {
        if (index >= 0 && index < SoundEffect.Length)
        {
            AudioSource.PlayClipAtPoint(SoundEffect[index], Camera.main.transform.position, EffectVolume);
        }
        else
        {
            Debug.LogWarning($"Invalid sound index: {index}");
        }
    }
    public void MusicSoundByIndex(int index)
    {
        if (index >= 0 && index < Music.Length)
        {
            AudioSource.PlayClipAtPoint(Music[index], Camera.main.transform.position, MusicVolume);
        }
        else
        {
            Debug.LogWarning($"Invalid sound index: {index}");
        }
    }

    public void PlaySoundAtPosition(int index, Vector3 position)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            // temp obj
            GameObject tempAudio = new GameObject("TempAudio");
            tempAudio.transform.position = position;

            AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
            audioSource.clip = audioClips[index];
            audioSource.spatialBlend = 1.0f; // Enable 3D sound
            audioSource.Play();

            Destroy(tempAudio, audioClips[index].length); // Destroy after playing
        }
        else
        {
            Debug.LogWarning($"Invalid sound index: {index}");
        }
    }
}
