using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRig : MonoBehaviour
{
    private StarterAssets.StarterAssetsInputs _input;
    private Rigidbody rig;
    public Transform body;

    [Header("Stats")]
    public float moveSpeed = 2f;
    public float rotateSpeed = 5f;
    public float jumpForce = 3f;


    private Vector3 movement;


    [Header("Head")]
    public Transform head;
    public float sensMultiplier = 1f;
    private float xRotation;



    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssets.StarterAssetsInputs>();
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        
    }

    private void FixedUpdate()
    {
        Movement();
        HeadMovement();
    }

    

    private void LateUpdate()
    {
        
    }

    private void Movement()
    {
        rig.velocity = rig.transform.TransformDirection(movement.x * moveSpeed * Time.fixedDeltaTime, rig.velocity.y, movement.z * moveSpeed * Time.fixedDeltaTime);
    }

    void MovementInput()
    {
        movement.x = _input.move.x;
        movement.z = _input.move.y;
        movement = movement.normalized;
        
    }

    void HeadMovement()
    {
        float deltaTimeMultiplier = 1f / Time.deltaTime;


        float mouseX = _input.look.x * rotateSpeed * sensMultiplier;
        float mouseY = -_input.look.y * rotateSpeed * sensMultiplier;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        body.transform.Rotate(Vector3.up * mouseX);
    }


}
