using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;
    public AnimationCurve curve;
    public float ShakeTime;

    public string gunName;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    //public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    //public TextMeshProUGUI text;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        //SetText
        //text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {

            if (rayHit.collider.CompareTag("Enemy")){
                Debug.Log("Entra hit");
                rayHit.collider.GetComponent<EnemyController>().TakeDamage(damage);
            }else
            {
                Debug.Log("No entra hit");
            }
                
        }

        //ShakeCamera
        //camShake.Shake(camShakeDuration, camShakeMagnitude);

        // Debug.DrawRay para visualizar el raycast
        Debug.DrawRay(fpsCam.transform.position, direction * range, Color.red, 1.0f);

        //Graphics
        CameraShake.MyInstance.StartCoroutine(CameraShake.MyInstance.Shake(curve, ShakeTime));
        Quaternion rotation = Quaternion.LookRotation(rayHit.normal);
        //Instantiate(bulletHoleGraphic, rayHit.point, rotation);
        Instantiate(muzzleFlash, attackPoint.position, rotation);
        
        if (rayHit.collider != null)
        {
        GameObject bulletHole = Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));

        // Ancla el bullet hole al objeto impactado
        bulletHole.transform.parent = rayHit.collider.transform;
        }
        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}

