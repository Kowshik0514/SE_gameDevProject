using System;
using UnityEngine;

public class enemyBot : MonoBehaviour
{
    public float speed = 1f;
    public int health = 100;
    public float attackRange = 20;
    public string[] targetTags;
    public int Id;
    public bool startFire = false; 
    private float updateTimer = 0f;
    private float updateInterval = 0.2f;

    public GameObject target;

    public GameObject getTarget(){
        return target;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Use the Id to pull the stats from the GameData enemyDataList
        if (Id >= 0 && Id < GameData.enemyDataList.Count)
        {
            // Retrieve the corresponding enemy stats from the GameData enemyDataList
            GameData1 enemyData = GameData.enemyDataList[Id];

            // Assign values to the enemy's stats from the enemyData object
            speed = enemyData.turnSpeed; // Example of assigning speed
            health = Mathf.RoundToInt(enemyData.health); // Enemy health from GameData
            attackRange = enemyData.gunRadius; // Example of attack range from gunRadius
            GetComponentInChildren<TargetHealth>().init(health);
            // Debug.Log("I                --: " + health);
        }
        else
        {
            Debug.LogError("Invalid enemy Id: " + Id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(target == null)
        updateTimer += Time.deltaTime;

        
        // if(target == null)
        if (updateTimer >= updateInterval*5)
        {
            FindNearestTarget();
            updateTimer = 0f;
        }
        if(target != null)
        {
            MoveTowardsTarget();
        }
            
    }
    void FindNearestTarget()
    {
        GameObject[] possibleTargets = GetAllTargets();
        float closeDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        // gameObject.GetComponent<MissileController>().StopMissiles();
        foreach (GameObject targetPos in possibleTargets)
        {
            float distance = Vector3.Distance(transform.position, targetPos.transform.position);
            if (distance < closeDistance)
            {
                closeDistance = distance;
                nearestTarget = targetPos;
            }
        }

        if (nearestTarget != null)
        {
            target = nearestTarget;
            //print("Found target with tag: " + target.tag);
        }
    }
    GameObject[] GetAllTargets()
    {
        var allTargets = new System.Collections.Generic.List<GameObject>();
        foreach (string str in targetTags)
        {
            allTargets.AddRange(GameObject.FindGameObjectsWithTag(str));
        }
        return allTargets.ToArray();
    }
    void MoveTowardsTarget()
    {
        if(target == null)
        {
            return;
        }

        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        //print("Distance to target: " + Vector3.Distance(transform.position, target.transform.position));
        //print("attack range :"+ attackRange);
        if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
        {
            startFire = false;
            //print("hi");
            transform.position += speed * Time.deltaTime * direction;
        }
        else
        {
            startFire = true;
            AttackTarget();
        }
    }
    void AttackTarget()
    {
        if (target == null)
        {
            return;
        }

        //Debug.Log("Attacking target with tag : " + target.tag);
    }
    public void Takedamage_bot(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            int damageAmount = 10;
            //Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            //if (projectile != null)
            //{
            //    damageAmount = projectile.damage;
            //}
            Takedamage_bot(damageAmount);
            Destroy(collision.gameObject);
        }
    }
}
