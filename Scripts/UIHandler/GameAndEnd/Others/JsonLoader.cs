using System.Collections.Generic;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    public static JsonLoader Instance { get; private set; } // Singleton instance

    public string fileName = "CyberAttackTypesandDefenseSystems"; // JSON file name (without extension)
    private CyberSecurityData data;
    private Dictionary<int, CyberAttack> cyberAttackDictById;
    private Dictionary<string, CyberAttack> cyberAttackDictByName;
    private Dictionary<int, DefenseSystem> defenseDictById;
    private Dictionary<string, DefenseSystem> defenseDictByName;

    private void Awake()
    {
        // Ensure there's only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        // Load JSON when the singleton is initialized
        LoadJson();
    }

    private void LoadJson()
    {
        // Load JSON from Resources folder
        TextAsset jsonAsset = Resources.Load<TextAsset>(fileName);
        if (jsonAsset != null)
        {
            string json = jsonAsset.text;

            // Deserialize JSON into CyberSecurityData object
            data = JsonUtility.FromJson<CyberSecurityData>(json);

            // Initialize dictionaries for fast lookup
            cyberAttackDictById = new Dictionary<int, CyberAttack>();
            cyberAttackDictByName = new Dictionary<string, CyberAttack>();
            defenseDictById = new Dictionary<int, DefenseSystem>();
            defenseDictByName = new Dictionary<string, DefenseSystem>();

            foreach (var attack in data.cyber_attacks)
            {
                cyberAttackDictById[attack.id] = attack;
                cyberAttackDictByName[attack.name] = attack;
            }

            foreach (var defense in data.defense_systems)
            {
                defenseDictById[defense.id] = defense;
                defenseDictByName[defense.name] = defense;
            }

            Debug.Log("JSON loaded and dictionaries initialized successfully!");
        }
        else
        {
            Debug.LogError("JSON file not found in Resources folder with name: " + fileName);
        }
    }

    public string GetDescriptionById(int id, string type)
    {
        Debug.Log(id);
        Debug.Log(type);
        Debug.Log("==================");

        if (type == "attack" && cyberAttackDictById.ContainsKey(id))
        {
            return cyberAttackDictById[id].description;
        }

        if (type == "defense" && defenseDictById.ContainsKey(id))
        {
            return defenseDictById[id].description;
        }

        return "Item not found for the given ID.";
    }
    public string GetNameById(int id, string type)
    {
        if (type == "attack" && cyberAttackDictById.ContainsKey(id))
        {
            return cyberAttackDictById[id].name;
        }

        if (type == "defense" && defenseDictById.ContainsKey(id))
        {
            return defenseDictById[id].name;
        }

        return "Item not found for the given ID.";
    }

    public string GetDescriptionByName(string name, string type)
    {
        if (type == "attack" && cyberAttackDictByName.ContainsKey(name))
        {
            return cyberAttackDictByName[name].description;
        }

        if (type == "defense" && defenseDictByName.ContainsKey(name))
        {
            return defenseDictByName[name].description;
        }

        return "Item not found for the given name.";
    }

    public string GetDefenseForAttack(int attackId)
    {
        if (data.attack_defense_mapping != null)
        {
            foreach (var mapping in data.attack_defense_mapping)
            {
                if (mapping.attack_id == attackId && defenseDictById.ContainsKey(mapping.defense_id))
                {
                    return defenseDictById[mapping.defense_id].description;
                }
            }
        }

        return "No defense system found for the given attack.";
    }
}

[System.Serializable]
public class CyberAttack
{
    public int id;
    public string name;
    public string description;
}

[System.Serializable]
public class DefenseSystem
{
    public int id;
    public string name;
    public string description;
}

[System.Serializable]
public class AttackDefenseMapping
{
    public int attack_id;
    public int defense_id;
}

[System.Serializable]
public class CyberSecurityData
{
    public CyberAttack[] cyber_attacks;
    public DefenseSystem[] defense_systems;
    public AttackDefenseMapping[] attack_defense_mapping;
}
