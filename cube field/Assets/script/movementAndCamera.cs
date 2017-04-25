using UnityEngine;
using System.Collections;

public class movementAndCamera : MonoBehaviour {

    float horizontalTheta, verticalTheta;

    Vector2 moveInput, lookInput;

    [SerializeField]
    float lookSensitivity, moveSpeed;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateInput();
        UpdateCamera();
	}

    private void FixedUpdate()
    {
        UpdateMovement();   
    }

    void UpdateMovement()
    {
        Vector3 movement = Vector3.zero;

        movement += transform.right * moveInput.x * moveSpeed;
        movement += transform.forward * moveInput.y * moveSpeed;

        rb.AddForce(transform.right * moveInput.x * moveSpeed);
        rb.AddForce(transform.forward * moveInput.y * moveSpeed);

        //transform.position += movement * Time.fixedDeltaTime;
    }

    void UpdateCamera()
    {
        lookInput *= lookSensitivity;

        horizontalTheta += lookInput.x;
        verticalTheta += lookInput.y;
        verticalTheta = Mathf.Clamp(verticalTheta, -90, 90);

        transform.rotation = Quaternion.Euler(0, horizontalTheta, 0) * Quaternion.Euler(verticalTheta, 0, 0);
    }

    void UpdateInput()
    {
        lookInput.x = Input.GetAxis("Mouse X");
        lookInput.y = Input.GetAxis("Mouse Y");

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
    }
}
