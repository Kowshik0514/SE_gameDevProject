using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour
{
    public GameObject notificationPrefab; // Assign your notification prefab here
    public Transform panel; // Assign your notification panel's transform here
    public string s;

    private Queue<GameObject> notificationQueue = new Queue<GameObject>(); // Queue to manage notifications
    private int maxNotifications = 5; // Maximum number of notifications visible

    public void ShowNotification(string message, int id)
    {

        // Create a new notification from the prefab
        GameObject newNotification = Instantiate(notificationPrefab, panel);
        newNotification.GetComponent<SetText>().id = id;
        newNotification.GetComponent<SetText>().set(message);

        // Add the new notification to the queue
        notificationQueue.Enqueue(newNotification);

        // Remove the oldest notification if the queue exceeds the limit
        if (notificationQueue.Count > maxNotifications)
        {
            GameObject oldestNotification = notificationQueue.Dequeue();
            Destroy(oldestNotification); // Destroy the oldest notification
        }

        // Optional: Adjust notification positions if needed
        UpdateNotificationPositions();
    }
    public void ShowNotification(string message){}

    private void UpdateNotificationPositions()
    {
        int index = 0;
        foreach (GameObject notification in notificationQueue)
        {
            // Adjust the position of each notification in the panel
            RectTransform rectTransform = notification.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(0, -index * 50); // Adjust 50 for vertical spacing
            }
            index++;
        }
    }
}
