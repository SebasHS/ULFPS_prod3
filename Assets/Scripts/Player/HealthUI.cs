using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.ImageEffects;

public class HealthUI : MonoBehaviour
{
    public Sprite health1; 
    public Sprite health2; 
    public Sprite health3; 
    public Sprite health4; 

    public Image healthIndicator; 

    public void UpdateHealthIndicator(int healthValue){
        if (healthValue >= 75){
            healthIndicator.sprite = health1;
        }
        if (healthValue < 75 && healthValue >= 50){
            healthIndicator.sprite = health2;
        }
        if (healthValue < 50 && healthValue > 0){
            healthIndicator.sprite = health3;
        }
        if(healthValue <= 0){
            healthIndicator.sprite = health4;
        }
    }
    
}