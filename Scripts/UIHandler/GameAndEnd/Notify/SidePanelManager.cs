using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SidePanelManager : MonoBehaviour
{
    public GameObject itemPrefab; // Assign your item prefab in the inspector
    public Transform content; // Assign the content transform of your Scroll View

    private void Start()
    {
        StartCoroutine(AddItemEvery10Seconds());
    }

    private IEnumerator AddItemEvery10Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            AddNewItem();
        }
    }

   private void AddNewItem()
{
    // Instantiate the new item as a child of the content transform
    GameObject newItem = Instantiate(itemPrefab, content);
    Debug.Log("New item instantiated: " + newItem.name);

    // Optionally set up the new item here (e.g., set text)
    Text itemText = newItem.GetComponentInChildren<Text>();
    if (itemText != null)
    {
        itemText.text = "New Item " + (content.childCount); // Set text to indicate item's order
        Debug.Log("Setting text for new item: " + itemText.text);
    }
    else
    {
        Debug.LogError("No Text component found in the instantiated prefab: " + newItem.name);
    }

    // Log the total number of items in content
    Debug.Log("Total items in content: " + content.childCount);
}

}
