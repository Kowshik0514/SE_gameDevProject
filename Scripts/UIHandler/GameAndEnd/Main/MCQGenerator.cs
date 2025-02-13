using UnityEngine;

public class MCQGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Mcq;
    void Select(){
        Mcq.SetActive(true);
    }
    void close(){
        Mcq.SetActive(false);
    }
}
