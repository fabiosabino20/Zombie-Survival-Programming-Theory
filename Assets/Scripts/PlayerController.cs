using Cinemachine;
using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isGrounded = true;
    private Rigidbody playerRb;

    [SerializeField] private float moveSpeed, jumpForce;
    [SerializeField] private Transform cameraTransform;
    
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        Jump();
        PlayerRotation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Left-Right Input
        float verticalInput = Input.GetAxis("Vertical"); // Forward-Back Input

        Vector3 direction = transform.TransformVector(new Vector3(horizontalInput, 0, verticalInput));
        playerRb.MovePosition(playerRb.position + direction * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        float jumpInput = Input.GetAxis("Jump");

        if (isGrounded && jumpInput > 0)
        {
            isGrounded = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void PlayerRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0), 200 * Time.deltaTime);
    }
}
