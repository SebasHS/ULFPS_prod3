using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 3;
    [SerializeField] private float RotationSpeed = 2f;

    private Vector3 direction = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
public float gravityForce = 9.18f;

    private CharacterController characterController;
    private Transform myCamera;
    private Rigidbody playerRigidbody; 


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        myCamera = transform.Find("Main Camera");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerRigidbody = GetComponent<Rigidbody>();

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
            rotation.y * RotationSpeed * Time.deltaTime,
            0f
        );

        // Rotacion vertical (camara)
        var rotationAngle = -rotation.x * RotationSpeed * Time.deltaTime;

        /*var desiredRotationQuat = Quaternion.Euler(transform.rotation.x + rotationAngle,
            0f,
            0f);
        

        Vector3 desiredRotation = desiredRotationQuat.eulerAngles;
        desiredRotation.x = desiredRotation.x > 180f 
            ? desiredRotation.x - 360f
            : desiredRotation.x ;

        desiredRotation.x = Mathf.Clamp(desiredRotation.x, -20f, 20f);
        /*myCamera.rotation = Quaternion.Euler(desiredRotation); */
        myCamera.Rotate(
            rotationAngle, //TODO: Clamp
            0f,
            0f
        );
    }
    private void FixedUpdate()
    {
        //playerRigidbody.AddForce(transform.down * gravityForce, ForceMode.Force);
    }

    private void OnMove(InputValue value)
    {
        var data = value.Get<Vector2>();
        direction = new Vector3(
            data.x,
            0f,
            data.y
        );
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            myCamera.GetComponent<PlayerFire>().Fire();
        }
    }

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
