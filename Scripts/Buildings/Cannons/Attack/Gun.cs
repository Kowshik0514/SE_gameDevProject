using UnityEngine;


public class Gun : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform[] gunPoints;
    public float fireRate;
    public float fireSpeed;
    public int damage;
    int strength;
    public GameObject rangeIndicator;

    public int level = 0;

    bool firing;
    float fireTimer;
    int gunPointIndex;
    public GameObject range;

    void Start(){
        strength = GameData.strength;
        strength += 5;
        fireRate = GameData.gameDataList[level].fireRate * strength/100;
        fireSpeed = GameData.gameDataList[level].bulletSpeed;
        damage = GameData.gameDataList[level].damage * strength/100;
        GetComponentInChildren<Building>().init(GameData.gameDataList[level].health * strength/100);
        range.GetComponent<RangeIndicator>().SetRange((int)GameData.gameDataList[level].gunRadius);
        rangeIndicator.transform.localScale = new Vector3((int)GameData.gameDataList[level].gunRadius/20f, 1f, (int)GameData.gameDataList[level].gunRadius/20f);
        Debug.Log("Setted Radius");

    }
    void Update()
    {
        if (firing)
        {
            while (fireTimer >= 1 / fireRate)
            {
                SpawnShot();
                fireTimer -= 1 / fireRate;
            }

            fireTimer += Time.deltaTime;
            firing = false;
        }
        else
        {
            if (fireTimer < 1 / fireRate)
            {
                fireTimer += Time.deltaTime;
            }
            else
            {
                fireTimer = 1 / fireRate;
            }
        }
    }
 
    void SpawnShot()
    {
        var gunPoint = gunPoints[gunPointIndex++];
        var a = Instantiate(shotPrefab, gunPoint.position, gunPoint.rotation);
        a.GetComponent<Bullet>().init(damage, fireSpeed);
        // a.transform.rotation = Quaternion.Euler(0, 0, 0);
        gunPointIndex %= gunPoints.Length;
    }

    public void Fire()
    {
        firing = true;
    }
}