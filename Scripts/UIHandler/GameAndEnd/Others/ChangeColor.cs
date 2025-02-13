using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public void Hower(){
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
}
