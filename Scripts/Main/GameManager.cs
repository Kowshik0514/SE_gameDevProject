using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyConfig
{
    public int EnemyID;
    public float DamageMultiplier;
    public float SpawnRate;
    public float SpeedMultiplier;
    public float BulletSpeedMultiplier;
    public float RangeMultiplier;
    public float count;
}

[System.Serializable]
public class LevelConfig
{
    public int LevelNumber;
    public List<EnemyConfig> Enemies;
}

public class GameManager : MonoBehaviour
{
    public LevelConfig Levels = null;
    int[] list = { 5, 5, 1, 1, 1 };
    int[] Showed = { 0, 0, 0, 0, 0 };
    public GameObject spawner;

    private int currentLevel = 1;

    private void Start()
    {
        GlobalObject.myGlobalObject.GetComponent<Calling>().set(9);
        GlobalObject.myGlobalObject.GetComponent<Calling>().Notification("Click Me");
        // StartLevel(currentLevel); // Start level 1
        Invoke("start_enemy", 5f);
    }

    void start_enemy(){
         StartLevel(currentLevel);
    }

    private void InitializeLevel(int i)
    {
        LevelConfig level = new LevelConfig
        {
            LevelNumber = i,
            Enemies = new List<EnemyConfig>()
        };

        for (int enemyType = 0; enemyType < 5; enemyType++)
        {
            level.Enemies.Add(new EnemyConfig
            {
                EnemyID = enemyType,
                DamageMultiplier = 100 + i * 2f,
                SpawnRate = Mathf.Min(10, 2 + i * 0.5f),
                SpeedMultiplier = 100 + i * 2f,
                BulletSpeedMultiplier = 100 + i * 3f,
                RangeMultiplier = 100 + i * 0.5f,
                count = Mathf.Max(0, list[enemyType] + i * 10)
            });
        }

        Levels = level;
    }

    public void StartLevel(int level)
    {
        GlobalObject.myGlobalObject.GetComponent<Calling>().set(6);

        GlobalObject.myGlobalObject.GetComponent<Calling>().Notification("Wave Starting");

        InitializeLevel(level);

        if (Levels == null || level != Levels.LevelNumber || Levels.Enemies.Count == 0)
        {
            Debug.LogError("Invalid level configuration or level mismatch!");
            return;
        }

        StartCoroutine(LevelFlowCoroutine());
    }

    private IEnumerator LevelFlowCoroutine()
    {
        // Spawn all enemies in the current level
        yield return StartCoroutine(SpawnEnemiesCoroutine());

        // Wait 30 seconds before starting the next level
        Debug.Log("Level complete! Waiting 30 seconds to start the next level...");
        GlobalObject.myGlobalObject.GetComponent<Calling>().set(6);
        GlobalObject.myGlobalObject.GetComponent<Calling>().Notification("Wave complete! Next wave in 30 seconds");
        yield return new WaitForSeconds(30f);

        // Start the next level
        GlobalObject.myGlobalObject.GetComponent<Calling>().set(7);

        GlobalObject.myGlobalObject.GetComponent<Calling>().Notification("Wave Starting");

        currentLevel++;
        
        StartLevel(currentLevel);
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        List<EnemyConfig> levelEnemies = Levels.Enemies;

        foreach (var enemy in levelEnemies)
        {
            int countToSpawn = Mathf.RoundToInt(enemy.count);

            for (int i = 0; i < countToSpawn; i++)
            {
                Spawn(
                    enemy.EnemyID,
                    enemy.DamageMultiplier,
                    enemy.RangeMultiplier,
                    enemy.SpeedMultiplier
                );

                yield return new WaitForSeconds(20f / enemy.SpawnRate);
            }
        }
    }

    private void Spawn(int id, float DamageMultiplier, float RangeMultiplier, float SpeedMultiplier)
    {
        if(Showed[id] < 1){
            Showed[id]++;
            GlobalObject.myGlobalObject.GetComponent<Calling>().set(id+1);
            GlobalObject.myGlobalObject.GetComponent<Calling>().Notification("New Enemy appeared");
        }
        Debug.Log($"Spawning Enemy ID: {id}, DamageMultiplier: {DamageMultiplier}, RangeMultiplier: {RangeMultiplier}, SpeedMultiplier: {SpeedMultiplier}");
        spawner.GetComponent<EnemySpawner>().ReleaseEnemy(id);
    }
}
