using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject[] guns; // Coloca tus GameObjects "Gun1", "Gun2" y "Gun3" en el Inspector.
    private int currentGunIndex = 0; // Índice del arma actual.

    void Start()
    {
        // Desactiva todas las armas al inicio (asegúrate de que estén desactivadas en el Inspector).
        DeactivateAllGuns();
        
        // Activa el primer arma.
        ActivateGun(currentGunIndex);
    }

    void Update()
    {
        // Puedes agregar aquí lógica adicional para cambiar de arma en función de la entrada del jugador, si es necesario.
    }

    void OnChangeWeapon()
    {
        // Desactiva el arma actual.
        DeactivateGun(currentGunIndex);

        // Incrementa el índice del arma actual y asegúrate de que esté en el rango correcto.
        currentGunIndex = (currentGunIndex + 1) % guns.Length;

        // Activa el nuevo arma.
        ActivateGun(currentGunIndex);
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

