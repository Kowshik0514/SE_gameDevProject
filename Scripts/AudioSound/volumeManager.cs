using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class volumeManager : MonoBehaviour
{
    public static volumeManager Instance;

    [Header("UI Sliders")]
    public Slider soundSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    [Header("Audio Settings")]
    [Range(0, 1)] public float soundVolume = 1.0f;
    [Range(0, 1)] public float musicVolume = 1.0f;
    [Range(0, 1)] public float effectsVolume = 1.0f;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize sliders with default values
        soundSlider.value = soundVolume;
        musicSlider.value = musicVolume;
        effectsSlider.value = effectsVolume;

    }
    private void Update()
    {
        soundVolume = soundSlider.value;
        musicVolume = musicSlider.value;
        effectsVolume = effectsSlider.value;
    }

}
