using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // public GameObject hitPrefab;
    // public GameObject muzzlePrefab;
    public float speed;
    public GameObject StartAnimation;

    public int damage;
    Rigidbody rb;
    Vector3 velocity;
    public void init(int d, float s){
        damage = d;
        speed = s;
    }

    void Awake()
    {
        TryGetComponent(out rb);
    }

    void Start()
    {
        // var muzzleEffect = Instantiate(muzzlePrefab, transform.position, transform.rotation);
        // Destroy(muzzleEffect, 5f);
        

        // Use the manager to get game data
        speed = GameData.gameDataList[0].bulletSpeed;

        damage = GameData.gameDataList[0].damage;

        velocity = transform.forward * speed;
        var strt = Instantiate(StartAnimation, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(strt, 0.05f);
        // transform.rotation = Quaternion.Euler(;
        Destroy(gameObject,5f);
    }

    
    void FixedUpdate()
    {
        if(rb){
            var displacement = velocity * Time.deltaTime;
            rb.MovePosition(rb.position + displacement);
        }
    }
 
    void OnTriggerEnter(Collider collision)
    {
        // var hitEffect = Instantiate(hitPrefab, other.GetContact(0).point, Quaternion.identity);
        // Destroy(hitEffect, 5f);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<TargetHealth>().Attacked(damage);
            // Debug.Log("Hit Enemy");
            Destroy(gameObject, 0.03f* (float)Math.Sqrt(speed)/10f);
        }

    }
}