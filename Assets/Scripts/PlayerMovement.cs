using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float hozInput, vertInput;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 10;
    private bool isJumpButtonPressed;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // d = 1, a = -1
        hozInput = Input.GetAxis("Horizontal");
        // w = 1, s = -1
        vertInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpButtonPressed = true;
        }

    }
    private void FixedUpdate()
    {
        Vector3 playerMovement = new Vector3(hozInput, 0, vertInput);
        playerMovement *= speed;
        rb.AddForce(playerMovement, ForceMode.Acceleration);

        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, transform.localScale.x / 2 + 0.01f))
            isGrounded = true;
        else
            isGrounded= false;

        if (isJumpButtonPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumpButtonPressed = false;
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    isGrounded = true;   
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    isGrounded = false;
    //}
}
