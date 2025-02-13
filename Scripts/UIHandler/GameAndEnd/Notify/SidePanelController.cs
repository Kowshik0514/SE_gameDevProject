



using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    public GameObject panel; // Reference to the panel
    private bool isVisible = false; // Tracks the visibility state

    // Toggle the panel visibility
    public void TogglePanel()
    {
        if (panel == null)
        {
            Debug.LogError("Panel reference is not assigned!");
            return;
        }

        isVisible = !isVisible; // Toggle the visibility state
        panel.SetActive(isVisible); // Enable or disable the panel based on the state
    }
}
