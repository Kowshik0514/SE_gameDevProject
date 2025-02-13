using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar: MonoBehaviour
{
    public Slider healthslider;

    public Slider easeHealthSlider;
    TargetHealth targetHealthScript;
    int flag = 0;
    Building buildingScript;
    public GameObject h_h;
    HelthHolder health_holder;

    
    private readonly float LerpSpeed = .05f;
    public int maxhealth;
    public int health;
    
    
    private void Start() {
        health_holder = h_h.GetComponent<HelthHolder>();
        maxhealth = 100;
        health = 100; 
        easeHealthSlider.maxValue = maxhealth;
        healthslider.maxValue = maxhealth;
        easeHealthSlider.value = health;
        healthslider.value = health;
    }

    private void Update() {
        health = health_holder.health;
        if(healthslider.value != health)
        {
            healthslider.value = health;
        }
        if(healthslider.value != easeHealthSlider.value){
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value,healthslider.value-10f,LerpSpeed);
        }
    }
    
}

internal interface IHasHealth
{
}