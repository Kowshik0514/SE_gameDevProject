using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasury : MonoBehaviour
{
    public List<GameObject> senders = new List<GameObject>();
    public List<GameObject> receivers = new List<GameObject>();
    
    public void AddSender(GameObject sender)
    {
        senders.Add(sender);
    }

    public void AddReceiver(GameObject receiver)
    {
        Debug.Log("Receiver");
        receivers.Add(receiver);
    }

    private IEnumerator GenerateMoney()
    {
        while (true)
        {
            // Remove null (destroyed) objects from senders and receivers
            senders.RemoveAll(sender => sender == null);
            receivers.RemoveAll(receiver => receiver == null);

            // Update money for all senders
            foreach (GameObject sender in senders)
            {
                Sender senderScript = sender.GetComponent<Sender>();
                if (senderScript != null)
                {
                    senderScript.IncreaseMoney();
                }
            }

            // Update money for all receivers
            foreach (GameObject receiver in receivers)
            {
                Receiver receiverScript = receiver.GetComponent<Receiver>();
                if (receiverScript != null)
                {
                    receiverScript.IncreaseMoney();
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void PrintStatus()
    {
        Debug.Log("Senders:");
        foreach (GameObject sender in senders)
        {
            Sender senderScript = sender.GetComponent<Sender>();
            Debug.Log($"Sender {sender.name}: Money = {senderScript.money}");
        }

        Debug.Log("Receivers:");
        foreach (GameObject receiver in receivers)
        {
            Receiver receiverScript = receiver.GetComponent<Receiver>();
            Debug.Log($"Receiver {receiver.name}: Money = {receiverScript.money}");
        }
    }

    void Start()
    {
        StartCoroutine(GenerateMoney());
    }
}
