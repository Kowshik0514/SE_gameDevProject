using UnityEngine;

public class exitHandlert : MonoBehaviour
{
    public GameObject exitPanel; // Reference to the ExitPanel

    void Start()
    {
        exitPanel.SetActive(false); // Ensure the panel is hidden initially
    }

   public void ToggleShowExitPanel()
    {
        exitPanel.SetActive(true); // Toggle the active state
    }

    public void ExitApplication()
    {
        Application.Quit(); // Exit the application
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop the editor play mode
        #endif
    }

    public void CancelExit()
    {
        exitPanel.SetActive(false); // Hide the ExitPanel
    }
}
