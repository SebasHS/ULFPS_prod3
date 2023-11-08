using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }
    public float health = 100;
    private bool brokenShield = false;
    public float shield = 50;

    public HealthUI healthUI;
    
    public TextMeshProUGUI healthText; 
    public TextMeshProUGUI shieldText; 

    public Transform camera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    public void Start()
    {
        UpdateHealthText(); 
        UpdateShieldText();
    }

    public void TakeDamage(float damage)
    {
        if (!brokenShield)
        {
            shield -= damage;
            if (shield <= 0)
            {
                health += shield;
                brokenShield = true;
                shield = 0f;
            }
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                Debug.Log("Te moriste");
                SceneManager.LoadScene("MainScene 2");

               
                
            }
        }
        UpdateHealthText(); 
        healthUI.UpdateHealthIndicator((int)health);
        UpdateShieldText(); 
    }

  

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString() + "%" + "\nHEALTH ";
        }
    }

    private void UpdateShieldText()
    {
        if (shieldText != null)
        {
            shieldText.text = shield.ToString() + "%" + "\nARMOR ";
        }
    }
}
