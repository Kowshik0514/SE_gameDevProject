using UnityEngine;

public class Receiver : MonoBehaviour
{
    public GameObject moneyPrefab;  // Reference to the money prefab
    public int money = 0;           // Current money value
    public int incrementAmount = 3; // Amount to increment each second
    public int Max = 1000;  

    // Start is called before the first frame update
    void Start()
    {
        // Initially, we hide the moneyPrefab
        moneyPrefab.SetActive(false);
    }

    // This function is called to increase money
    public void IncreaseMoney()
    {
        if(money < Max)
        money += incrementAmount;
        
        // When money reaches 100, show the prefab
        if (money >= 100 && !moneyPrefab.activeSelf)
        {
            moneyPrefab.SetActive(true); // Show money prefab
        }
    }

    // This function is called to reset the money and hide the prefab
    public void Select()
    {
        GameData.Money += money;
        money = 0; // Reset money
        moneyPrefab.SetActive(false); // Hide the prefab
    }
}
