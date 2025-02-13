using System;
using UnityEngine;

public class BulletAnimation : MonoBehaviour
{
    // public GameObject hitPrefab;
    // public GameObject muzzlePrefab;
    private static LevelManager manager;
    public float speed;
    public GameObject StartAnimation;

    public int damage;
    Rigidbody rb;
    Vector3 velocity; 
    

    void Awake()
    {
        TryGetComponent(out rb);
    }

    void Start()
    {
        speed = GameData.gameDataList[0].bulletSpeed;

        damage = GameData.gameDataList[0].damage;

        velocity = transform.forward * speed;
        var strt = Instantiate(StartAnimation, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(strt, 0.05f);
        Destroy(gameObject,5f);
    }

    
    void FixedUpdate()
    {
        if(rb){
            var displacement = velocity * Time.deltaTime * 100;
            rb.MovePosition(rb.position + displacement);
        }
    }
 
    
}