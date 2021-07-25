using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [Header("Permissions")]
    public bool canWalk = true;
    public bool canMoveCamera = true;

    [Header("Parameters")]
    public float normalMovementSpeed;
    public float runMovementSpeed;
    public float jumpForce;
    public float cameraSpeed = 100f;

    bool IsGrounded;
    float CurrMovmentSpeed;
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
        // permissões de andar e mexer camera
        if (canWalk)
        {
            Movement();
            Jump();
        }
        if (canMoveCamera)
        {
            MoveCamera();
        }

        // sistema de corrida
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CurrMovmentSpeed = runMovementSpeed;
        }
        else
        {
            CurrMovmentSpeed = normalMovementSpeed;

        }


    }

    #region Movement Control

    void Movement()
    {
        float fowardMove = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        float sideMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        Vector3 MoveVector = transform.right * sideMove + transform.forward * fowardMove;

        rb.velocity = MoveVector.normalized * CurrMovmentSpeed * Time.deltaTime + new Vector3(0, rb.velocity.y, 0);
    
    }

    //sistema de pulo
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsGrounded = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true
;        }
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
