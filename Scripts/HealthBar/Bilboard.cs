using UnityEngine;

public class Bilboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform Camera;
    
    private void LateUpdate() {
        transform.LookAt(transform.position+Camera.forward);
    }

    void Start(){
        GameObject myObject = GameObject.Find("Main Camera");
        Camera = myObject.transform;
    }
     
}
