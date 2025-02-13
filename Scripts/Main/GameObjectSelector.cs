using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DynamicButtonCreator : MonoBehaviour
{
    public GameObject buttonPrefab;  // Drag your button prefab here
    public Transform contentPanel;   // The content panel of your ScrollView
    public int numberOfButtons = 10; // Number of buttons you want to generate

    void Start()
    {
        if (buttonPrefab == null)
        {
            Debug.LogError("Button Prefab is not assigned!");
            return;
        }

        if (contentPanel == null)
        {
            Debug.LogError("Content Panel is not assigned!");
            return;
        }

        CreateButtons();
    }

    void CreateButtons()
{
    // Ensure the content panel is empty before adding new buttons
    foreach (Transform child in contentPanel)
    {
        Destroy(child.gameObject);
    }

    // Create buttons dynamically
    for (int i = 0; i < numberOfButtons; i++)
    {
        GameObject button = Instantiate(buttonPrefab, contentPanel);
        Button btn = button.GetComponent<Button>();

        if (btn == null)
        {
            Debug.LogError("Button prefab does not have a Button component attached!");
            continue;
        }

        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText == null)
        {
            Debug.LogError("Button prefab does not have a TMP_Text component attached!");
            continue;
        }

        // Customize button text or functionality
        buttonText.text = "Button " + (i + 1);
        btn.onClick.AddListener(() => OnButtonClicked(i));  // Attach a click event
    }
}


    // Function to handle button click event
    void OnButtonClicked(int buttonIndex)
    {
        Debug.Log("Button " + buttonIndex + " clicked!");
    }
}
