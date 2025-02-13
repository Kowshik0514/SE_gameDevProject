using UnityEngine;
using UnityEngine.UI;

public class SettingHandler : MonoBehaviour
{
    public GameObject settingsPanel; // Reference to the settings panel

    void Start()
    {
        settingsPanel.SetActive(false); // Ensure the panel is hidden initially
    }

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); // Toggle the active state
    }
}

