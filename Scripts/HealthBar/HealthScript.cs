using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class HealthScript : MonoBehaviour
{
    public Slider moneySlider;        

    void Start(){
        SetMoney(100);
    }

    void Update(){
        SetMoney((GameData.Health * 100 - 1)/GameData.MaxHealth);
    }

    public void IncrementMoney(int val){
        // int currentMoney = int.Parse(moneyValueText.text); 
        // currentMoney += val; 
        // moneyValueText.text = currentMoney.ToString();

    }
    public void SetMoney(int money)
    {
        moneySlider.value = money % moneySlider.maxValue;  
    }

    public void SetMaxMoney(int maxMoney)
    {
        moneySlider.maxValue = maxMoney;        
        moneySlider.value = maxMoney;      
    }

    
    
}
