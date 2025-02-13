using UnityEngine;
using System.Collections.Generic;
using System;
public static class GameData
{
    public static float bulletSpeed = 10f;
    public static GameObject buildingToPlace = null; 
    public static GameObject buffToPlace = null; 
    public static int buffId = -1;
    public static int Movement = 1;

    public static List<GameData1> gameDataList = new List<GameData1>();
    public static int[] price = new int[10];
    public static int[] BuffPrice = new int[10];

    public static string[] Notify = new string[10];
    public static int[] BuffTime = new int[10];
    public static int[] BuffEffect = new int[10];


    public static List<GameData1> enemyDataList = new List<GameData1>();
    public static int Survive = 3;
    public static string password = "";
    public static int strength = 1;
    public static int sender = -1;

    public static int Money = 500;
    public static int MaxHealth = 10000;

    public static int Health = 10000;

    // GameDataList for player's guns
    public static void StartData()
    {
        for (int i = 0; i <= 5; i++)
        {
            gameDataList.Add(new GameData1());
        }
        Money = 500;
        Health = MaxHealth;
        password = "";
        strength = 1;
        sender = -1;
        buffId = -1;
        Survive = 3;
        Movement = 1;

        price[0] = 20;
        price[1] = 30;
        price[2] = 40;
        price[3] = 50;
        price[4] = 60;
        price[5] = 30;
        price[6] = 80;
        price[7] = 80;
        price[8] = 80;
        price[9] = 80;

        BuffTime[0] = 20;
        BuffTime[1] = 30;
        BuffTime[2] = 10;
        BuffTime[3] = 20;
        BuffTime[4] = 50;

        BuffPrice[0] = 100;
        BuffPrice[1] = 200;
        BuffPrice[2] = 70;
        BuffPrice[3] = 170;
        BuffPrice[4] = 300;

        BuffEffect[3] = 30;
        BuffEffect[4] = 60;



        Notify[0] = "Not Enough Money To Buy";
        Notify[1] = "Building Destroyed took : ";
        Notify[2] = " A building was destroyed ";
        Notify[3] = " Cannot Place building  ";





        // Player's gun stats (Level 0 to Level 4)
        gameDataList[0].fireRate = 25; 
        gameDataList[0].bulletSpeed = 20;
        gameDataList[0].turnSpeed = 100;
        gameDataList[0].damage = 35;
        gameDataList[0].aimTolerance = 1.5f;
        gameDataList[0].gunRadius = 20;
        gameDataList[0].gunHeight = 20;
        gameDataList[0].health = (10 * 20) * Survive;


        gameDataList[1].fireRate = 30;
        gameDataList[1].bulletSpeed = 22;
        gameDataList[1].turnSpeed = 110;
        gameDataList[1].damage = 40;
        gameDataList[1].aimTolerance = 1.3f;
        gameDataList[1].gunRadius = 25;
        gameDataList[1].gunHeight = 20;
        gameDataList[1].health = (10 * 20) * Survive;


        gameDataList[2].fireRate = 35;
        gameDataList[2].bulletSpeed = 25;
        gameDataList[2].turnSpeed = 120;
        gameDataList[2].damage = 50;
        gameDataList[2].aimTolerance = 1.1f;
        gameDataList[2].gunRadius = 30;
        gameDataList[2].gunHeight = 25;
        gameDataList[2].health = (15 * 20) * Survive;


        gameDataList[3].fireRate = 40;
        gameDataList[3].bulletSpeed = 30;
        gameDataList[3].turnSpeed = 130;
        gameDataList[3].damage = 65;
        gameDataList[3].aimTolerance = 1.0f;
        gameDataList[3].gunRadius = 35;
        gameDataList[3].gunHeight = 30;
        gameDataList[3].health = (20 * 20) * Survive;


        gameDataList[4].fireRate = 45;
        gameDataList[4].bulletSpeed = 35;
        gameDataList[4].turnSpeed = 140;
        gameDataList[4].damage = 80;
        gameDataList[4].aimTolerance = 0.8f;
        gameDataList[4].gunRadius = 40;
        gameDataList[4].gunHeight = 35;
        gameDataList[4].health = (25 * 20) * Survive;;

        Survive = 10;
        // Create enemies (5 enemies, different levels)
        for (int i = 0; i < 5; i++)
        {
            enemyDataList.Add(new GameData1());
        }

        // Enemy data with health and attack stats
        // Calculate health to survive 10 seconds of attack from each player's gun
        enemyDataList[0].fireRate = 25;
        enemyDataList[0].bulletSpeed = 15;
        enemyDataList[0].turnSpeed = 6;
        enemyDataList[0].damage = 100;
        enemyDataList[0].aimTolerance = 1.7f;
        enemyDataList[0].gunRadius = 10;
        enemyDataList[0].gunHeight = 20;
        enemyDataList[0].health = (25 * 30) * Survive; // 25 fire rate * 30 damage = 750 DPS, 750 * 10 = 7500 health

        enemyDataList[1].fireRate = 30;
        enemyDataList[1].bulletSpeed = 20;
        enemyDataList[1].turnSpeed = 7;
        enemyDataList[1].damage = 140;
        enemyDataList[1].aimTolerance = 1.5f;
        enemyDataList[1].gunRadius = 10;
        enemyDataList[1].gunHeight = 20;
        enemyDataList[1].health = (30 * 35) * Survive; // 30 fire rate * 35 damage = 1050 DPS, 1050 * 10 = 10500 health

        enemyDataList[2].fireRate = 35;
        enemyDataList[2].bulletSpeed = 22;
        enemyDataList[2].turnSpeed = 8;
        enemyDataList[2].damage = 160;
        enemyDataList[2].aimTolerance = 1.3f;
        enemyDataList[2].gunRadius = 10;
        enemyDataList[2].gunHeight = 25;
        enemyDataList[2].health = (35 * 45) * Survive; // 35 fire rate * 45 damage = 1575 DPS, 1575 * 10 = 15750 health

        enemyDataList[3].fireRate = 40;
        enemyDataList[3].bulletSpeed = 25;
        enemyDataList[3].turnSpeed = 7;
        enemyDataList[3].damage = 300;
        enemyDataList[3].aimTolerance = 1.2f;
        enemyDataList[3].gunRadius = 12;
        enemyDataList[3].gunHeight = 30;
        enemyDataList[3].health = (40 * 55) * Survive; // 40 fire rate * 55 damage = 2200 DPS, 2200 * 10 = 22000 health

        enemyDataList[4].fireRate = 45;
        enemyDataList[4].bulletSpeed = 30;
        enemyDataList[4].turnSpeed = 9;
        enemyDataList[4].damage = 200;
        enemyDataList[4].aimTolerance = 1.0f;
        enemyDataList[4].gunRadius = 15;
        enemyDataList[4].gunHeight = 35;
        enemyDataList[4].health = (45 * 70) * Survive; // 45 fire rate * 70 damage = 3150 DPS, 3150 * 10 = 31500 health
    }
}





public static class Map
{
    private static Dictionary<string, List<GameObject>> stringToGameObjectMap = new Dictionary<string, List<GameObject>>();

    public static void AddMapping(string key, GameObject obj)
    {
        if (!stringToGameObjectMap.ContainsKey(key))
        {
            stringToGameObjectMap[key] = new List<GameObject> { obj };
        }
        else
        {
            stringToGameObjectMap[key].Add(obj);
        }
    }

    public static GameObject[] GetGameObjectArray(string key)
    {
        if (stringToGameObjectMap.TryGetValue(key, out List<GameObject> objList))
        {
            return objList.ToArray();
        }
        return Array.Empty<GameObject>();
    }

    public static void DestroyAll(string key)
    {
        if (stringToGameObjectMap.TryGetValue(key, out List<GameObject> objList))
        {
            foreach (GameObject obj in objList)
            {
                if (obj == null)
                {
                    Debug.Log("GameObject in 'objList' is null!");
                    continue;
                }

                Building building = obj.GetComponent<Building>();
                if (building == null)
                {
                    Debug.Log("No build");
                    continue;
                }
                building.Destroy_Ani();
            }

            stringToGameObjectMap.Remove(key); // Remove the mapping after destruction
        }
    }

}
