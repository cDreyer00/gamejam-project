using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [Header("Permissions")]
    public bool canWalk;
    public bool canMoveCamera;

    [Header("Parameters")]
    public float movementSpeed = 400f;
    public float cameraSpeed = 100f;

    GameObject CameraObject;
    Rigidbody rb;


    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        CameraObject = transform.GetChild(0).gameObject;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;     //lock cursor in the center
    }

    void Update()
    {

        if (canWalk)
            Movement();

        if(canMoveCamera)
            MoveCamera();

    }

    #region Movement Control

    void Movement()
    {
        float fowardMove = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        float sideMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        Vector3 MoveVector = transform.right * sideMove + transform.forward * fowardMove;

        rb.velocity = MoveVector.normalized * movementSpeed * Time.deltaTime + new Vector3(0, rb.velocity.y, 0);
    
    }

    #endregion


    #region Player Camera Controll

    float mouseX, mouseY;
    void MoveCamera()
    {
        mouseX += Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -50, 60);
        transform.rotation = Quaternion.Euler(0, mouseX, 0);

        CameraObject.transform.localRotation = Quaternion.Euler(mouseY, 0, 0);

    }

    #endregion

    
}
