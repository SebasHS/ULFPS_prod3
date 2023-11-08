using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunManager : MonoBehaviour
{
    public GameObject[] guns; // Coloca tus GameObjects "Gun1", "Gun2" y "Gun3" en el Inspector.
    private int currentGunIndex = 0; // Índice del arma actual.

    public TextMeshProUGUI bulletsText; 
    

    void Start()
    {
        // Desactiva todas las armas al inicio (asegúrate de que estén desactivadas en el Inspector).
        DeactivateAllGuns();
        
        // Activa el primer arma.
        ActivateGun(currentGunIndex);
        UpdateBulletsText();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            OnChangeWeapon();
        }
        UpdateBulletsText();
    }

    void OnChangeWeapon()
    {
        
        // Desactiva el arma actual.
        DeactivateGun(currentGunIndex);

        // Incrementa el índice del arma actual y asegúrate de que esté en el rango correcto.
        currentGunIndex = (currentGunIndex + 1) % guns.Length;

        // Activa el nuevo arma.
        ActivateGun(currentGunIndex);

        Debug.Log("Quedan "+ guns[currentGunIndex].GetComponent<GunSystem>().bulletsLeft+ 
                 "balitas en "+ guns[currentGunIndex].GetComponent<GunSystem>().gunName);

        UpdateBulletsText();
    }

    void UpdateBulletsText()
{
    GunSystem gunSystem = guns[currentGunIndex].GetComponent<GunSystem>();
    if (bulletsText != null && gunSystem != null)
    {
        bulletsText.text = gunSystem.bulletsLeft.ToString() + "\nAMMO";
    }
}

    void ActivateGun(int index)
    {
        guns[index].SetActive(true);
    }

    void DeactivateGun(int index)
    {
        guns[index].SetActive(false);
    }

    void DeactivateAllGuns()
    {
        foreach (var gun in guns)
        {
            gun.SetActive(false);
        }
    }
}

