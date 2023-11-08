using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed = 3;
    [SerializeField]
    private float RotationSpeed = 2f;
    [SerializeField]
    private float RotationScale=2f;
    [SerializeField]
    private float gravityForce = 9.81f;
    [SerializeField]
    private float jumpForce = 7f;
    public AvatarController avatarController;
    private Vector3 direction = Vector3.zero;
    private Vector3 rotation = Vector3.zero;


    private CharacterController characterController;
    private Transform myCamera;

    private float lastX;

    private void Awake() 
    {
        characterController = GetComponent<CharacterController>();   
        myCamera = transform.Find("Main Camera");
    }

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        //myCamera.transform.localEulerAngles = new Vector3(357f, 0f, 0f);
    }
    private void Update() 
    {
        
        // Movimiento
        characterController.Move(
            transform.forward * direction.normalized.z * Time.deltaTime * MovementSpeed
            + transform.right * direction.normalized.x * Time.deltaTime * MovementSpeed
        );
        characterController.Move(Vector3.down * gravityForce * Time.deltaTime);

        // Rotacion Horizontal
        transform.Rotate(
            0f,
            rotation.y * RotationSpeed * Time.deltaTime* RotationScale,
            0f
        );


        // Rotacion vertical (camara)
        var rotationAngle = -rotation.x * RotationSpeed * Time.deltaTime * RotationScale;
        float currentX = myCamera.eulerAngles.x;

        if ((currentX >= 0 && currentX <= 80) || (currentX >= 288 && currentX <= 360))
        {
            Debug.Log("si" + currentX);
            myCamera.Rotate(rotationAngle, 0f, 0f);
            lastX = currentX; // Actualiza lastX con el valor actual de currentX antes de la rotación.
        }
        else
        {
            myCamera.localEulerAngles = new Vector3(lastX, 0, 0);
        }
        
    }






    private void OnMove(InputValue value)
    {
        var data = value.Get<Vector2>();
        direction = new Vector3(
            data.x,
            0f,
            data.y
        );
        if (Mathf.Abs(data.x) > Mathf.Epsilon || 
            Mathf.Abs(data.y) > Mathf.Epsilon)
        {
            avatarController.IsWalking(true);
        }else
        {
            avatarController.IsWalking(false);
        }
    }

    /*private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            myCamera.GetComponent<PlayerFire>().Fire();
        }
    }*/

    private void OnLook(InputValue value)
    {
        var data = value.Get<Vector2>();
        rotation = new Vector3(
            data.y,
            data.x, // rotacion horizontal (sobre eje Y)
            0f
        );
    }
}
