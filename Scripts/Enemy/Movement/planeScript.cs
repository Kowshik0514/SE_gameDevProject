using System.Collections;
using UnityEngine;

public class planeScript : MonoBehaviour
{
    public float upwardDistance = 3f;
    public float speed = 2f;
    private bool isMovingUpward = true;
    public bool IsMovingUpwards
    {
        get { return isMovingUpward; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveUpwards());
    }
    

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator MoveUpwards()
    {
        float targetHeight = transform.position.y + upwardDistance;
        while (transform.position.y < targetHeight)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
            yield return null;
        }
        isMovingUpward = false;
    }
}
