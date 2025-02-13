using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class MoneyScript : MonoBehaviour
{
    public Slider moneySlider;        
    public TMP_Text moneyValueText;   

    private void Start() {
        SetMaxMoney(20);
    }

    void Update(){
        SetMoney(GameData.Money);
    }

    public void IncrementMoney(int val){
        int currentMoney = int.Parse(moneyValueText.text); 
        currentMoney += val; 
        moneyValueText.text = currentMoney.ToString();

    }
    public void SetMoney(int money)
    {
        moneySlider.value = money % moneySlider.maxValue;
        UpdateMoneyValueText(money);                     
    }

    public void SetMaxMoney(int maxMoney)
    {
        moneySlider.maxValue = maxMoney;        
        moneySlider.value = maxMoney;      
        UpdateMoneyValueText(maxMoney);    
    }

    
    private void UpdateMoneyValueText(int money)
    {
        if (moneyValueText != null) 
        {
            moneyValueText.text = money.ToString(); 
        }
        else
        {
            Debug.LogWarning("Money Value Text is not assigned in the inspector.");
        }
    }
}
