using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SetText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject a; // Assign the GameObject with TextMeshProUGUI in the Inspector
    public string text = "";
    public int id = 0;

    public void set(string s)
    {
        if (a == null)
        {
            Debug.LogError("GameObject 'a' is not assigned in the Inspector!");
            return;
        }
        text = s;
        // Get the TextMeshProUGUI component
        TextMeshProUGUI notificationText = a.GetComponent<TextMeshProUGUI>();
        if (notificationText == null)
        {
            Debug.LogError("The GameObject 'a' does not have a TextMeshProUGUI component attached!");
            return;
        }

        // Set the text
        notificationText.text = s;
    }

    public void click(){
        GlobalObject.myGlobalObject.GetComponent<Util>().NotifyClick(id);
    }
}