using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MissileController : MonoBehaviour
{
    public Transform[] leftMissiles;
    public Transform[] rightMissiles;
    public float missileSpeed = 10f;
    public float fireInterval = 1f;
    public float attackRange;
    public float missileLifetime = 5f;
    public GameObject blastEffect;

    public string[] targetTags;
    private enemyBot enemyBot;
    private GameObject target;
    private GameObject previousTarget;
    private bool isFiring = false;
    private Vector3[] initialLeftPositions;
    private Vector3[] initialRightPositions;
    private Quaternion[] initialLeftRotations;
    private Quaternion[] initialRightRotations;
    private float updateTimer = 0f;
    private float updateInterval = 0.2f;
    public int Id = 0;

    void Start()
    {
        enemyBot = GetComponent<enemyBot>();
        attackRange = enemyBot.attackRange;
        if (Id >= 0 && Id < GameData.enemyDataList.Count)
        {
            GameData1 missileData = GameData.enemyDataList[Id];
            missileSpeed = missileData.bulletSpeed;
            fireInterval = 10f / missileData.fireRate;
            attackRange = missileData.gunRadius;
        }
        else
        {
            Debug.LogError("Invalid missile Id: " + Id);
        }

        initialLeftPositions = new Vector3[leftMissiles.Length];
        initialRightPositions = new Vector3[rightMissiles.Length];
        initialLeftRotations = new Quaternion[leftMissiles.Length];
        initialRightRotations = new Quaternion[rightMissiles.Length];

        for (int i = 0; i < leftMissiles.Length; i++)
        {
            initialLeftPositions[i] = leftMissiles[i].localPosition;
            initialLeftRotations[i] = leftMissiles[i].localRotation;
        }
        for (int i = 0; i < rightMissiles.Length; i++)
        {
            initialRightPositions[i] = rightMissiles[i].localPosition;
            initialRightRotations[i] = rightMissiles[i].localRotation;
        }
    }
    void Update()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            FindNearestTarget();
            if (target != previousTarget)
            {
                StopAllCoroutines();
                ResetMissiles();
                StartCoroutine(PauseAndStart());
                previousTarget = target;
            }
            if (target != null && Vector3.Distance(transform.position, target.transform.position) <= attackRange && !isFiring)
            {
                StartCoroutine(FireMissiles());
            }
        }
    }
    IEnumerator PauseAndStart()
    {
        //print(isFiring);
        isFiring = true;
        yield return new WaitForSeconds(2f);
        isFiring = false;
        StartCoroutine(FireMissiles());
    }
    void ResetMissiles()
    {
        for (int i = 0; i < leftMissiles.Length; i++)
        {
            if (leftMissiles[i] != null)
            {
                leftMissiles[i].SetLocalPositionAndRotation(initialLeftPositions[i], initialLeftRotations[i]);
            }
        }
        for (int i = 0; i < rightMissiles.Length; i++)
        {
            if (rightMissiles[i] != null)
            {
                rightMissiles[i].SetLocalPositionAndRotation(initialRightPositions[i], initialRightRotations[i]);
            }
        }
    }

    void FindNearestTarget()
    {
        target = enemyBot.getTarget();
    }

    IEnumerator FireMissiles()
    {
        isFiring = true;

        for (int i = 0; i < Mathf.Min(leftMissiles.Length, rightMissiles.Length); i++)
        {
            FireMissile(leftMissiles[i], initialLeftPositions[i], initialLeftRotations[i]);
            FireMissile(rightMissiles[i], initialRightPositions[i], initialRightRotations[i]);

            yield return new WaitForSeconds(fireInterval);
        }

        isFiring = false;
    }

    void FireMissile(Transform missile, Vector3 initialLocalPosition, Quaternion initialLocalRotation)
    {
        if (missile == null || target == null) return;
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            StartCoroutine(MoveMissile(missile, initialLocalPosition, initialLocalRotation));
        }
    }

    IEnumerator MoveMissile(Transform missile, Vector3 initialLocalPosition, Quaternion initialLocalRotation)
    {
        float timer = 0f;

        while (missile != null && target != null && timer < missileLifetime)
        {
            Vector3 direction = (target.transform.position - missile.position).normalized;

            missile.position += direction * missileSpeed * Time.deltaTime;
            missile.rotation = Quaternion.LookRotation(direction);

            if (Vector3.Distance(missile.position, target.transform.position) < 0.5f)
            {
                //Debug.Log("Missile hit the target!");
                if(blastEffect != null)
                {
                    var end = Instantiate(blastEffect, missile.position, missile.rotation);
                    Destroy(end , 0.5f);
                }
                // SoundManager.Instance.PlaySoundAtPosition(0,transform.position);
                target.transform.GetComponent<Building>().attacked(5);
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        missile.SetLocalPositionAndRotation(initialLocalPosition, initialLocalRotation);
    }
}