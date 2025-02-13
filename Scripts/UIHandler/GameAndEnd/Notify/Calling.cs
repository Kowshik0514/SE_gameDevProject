using UnityEngine;

public class Calling : MonoBehaviour
{
    public NotificationManager notificationManager;
    int id = 0;
    // This method can be called by the button
    public void Notification(string s)
    {
        notificationManager.ShowNotification(s, id);
    }
    public void set(int i){
        id = i;
    }
}