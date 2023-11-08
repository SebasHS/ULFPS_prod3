using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


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

    public Transform gunFBX;

    //Graphics
    public GameObject muzzleFlash, bulletHoleBuilding, bulletHoleBody;
    //public CamShake camShake;
    //public float camShakeMagnitude, camShakeDuration;
    //public TextMeshProUGUI text;
    void Start(){
        
    }
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

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) 
        {
            Reload();
            
        }
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

        

        //Graphics
        CameraShake.MyInstance.StartCoroutine(CameraShake.MyInstance.Shake(curve, ShakeTime));
        Quaternion rotation = Quaternion.LookRotation(rayHit.normal);
        
        
        // Instancia el Muzzle Flash
        GameObject muzzleFlashInstance = Instantiate(muzzleFlash, attackPoint.position, rotation);

        // Destruye el Muzzle Flash 
        Destroy(muzzleFlashInstance, 0.3f);

        
        
        
        if (rayHit.collider != null)
        {
            if (rayHit.collider.CompareTag("Enemy")){
                //Instantiate(bulletHoleGraphic, rayHit.point, rotation);
                GameObject bulletHole = Instantiate(bulletHoleBody, rayHit.point, Quaternion.LookRotation(rayHit.normal));

                // Ancla el bullet hole al objeto impactado
                bulletHole.transform.parent = rayHit.collider.transform;
            }else{
                //Instantiate(bulletHoleGraphic, rayHit.point, rotation);
                GameObject bulletHole = Instantiate(bulletHoleBuilding, rayHit.point, Quaternion.LookRotation(rayHit.normal));

                // Ancla el bullet hole al objeto impactado
                bulletHole.transform.parent = rayHit.collider.transform;
            }
            
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
        // ACAAAAAAAAAAAAA
        Vector3 originalPosition = gunFBX.localPosition;
        Vector3 originalRotation = gunFBX.localEulerAngles;
        StartCoroutine(RealoadMagazineAnimation(originalPosition, originalRotation, 0.2f, reloadTime));
        
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;

        reloading = false;
    }
    


    private Vector3 targetPosition;
    private Vector3 targetRotation;

    private IEnumerator RealoadMagazineAnimation(Vector3 originalPosition, Vector3 originalRotation, float animationDuration, float reloadTime)
    {
        
        
        // Define la posición y rotación final local a la que deseas llegar
        targetPosition = new Vector3(gunFBX.localPosition.x, gunFBX.localPosition.y - 0.3f, gunFBX.localPosition.z);
        targetRotation = new Vector3(gunFBX.localEulerAngles.x - 32.0f, gunFBX.localEulerAngles.y, gunFBX.localEulerAngles.z);

        float elapsedTime = 0f;

        Vector3 initialPosition = gunFBX.localPosition;
        Vector3 initialRotation = gunFBX.localEulerAngles;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / animationDuration;
            gunFBX.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
            gunFBX.localEulerAngles = Vector3.Lerp(initialRotation, targetRotation, t);

            yield return null;
        }

        // Asegurarse de que la posición y rotación sean exactamente las de destino al final
        gunFBX.localPosition = targetPosition;
        gunFBX.localEulerAngles = targetRotation;

        // Esperar 5 segundos antes de iniciar la siguiente corutina
        yield return new WaitForSeconds(reloadTime-(animationDuration*2.0f));

        StartCoroutine(ReturnAfterMagazineChange(originalPosition, originalRotation, animationDuration));


    }


    private IEnumerator ReturnAfterMagazineChange(Vector3 originalPosition, Vector3 originalRotation, float animationDuration)
    {
    float elapsedTime = 0f;

    while (elapsedTime < animationDuration)
    {
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / animationDuration;
        gunFBX.localPosition = Vector3.Lerp(gunFBX.localPosition, originalPosition, t);
        gunFBX.localEulerAngles = Vector3.Lerp(gunFBX.localEulerAngles, originalRotation, t);

        yield return null;
    }
    // Asegurarse de que la posición y rotación sean exactamente las de destino al final
        gunFBX.localPosition = originalPosition;
        gunFBX.localEulerAngles = originalRotation;
    }




    
    
}

