using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5;

    [SerializeField]
    private float runSpeed = 8;

    [SerializeField]
    private float rotationSpeed = 720;

    [SerializeField]
    private float jumpSpeed = 5;

    [SerializeField]
    private float jumpButtonGracePeriod = 0.2f;

    [SerializeField]
    private Transform cameraTransform;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float ySpeedBefore;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        float speed =  inputMagnitude * walkSpeed;
        if(Input.GetKey(KeyCode.LeftShift)){
            speed =  inputMagnitude * runSpeed;
        } else {
            inputMagnitude  /= 2;
        }

        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        
        ySpeed += -9.8f * Time.deltaTime;

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if(characterController.isGrounded){
            lastGroundedTime = Time.time;
            animator.SetBool("isGrounded", true);
            isGrounded = true;
        }

        if(Input.GetButtonDown("Jump")){
            jumpButtonPressedTime = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.C)){
            animator.SetTrigger("doRoll");
        } 


        if(Time.time - lastGroundedTime <= jumpButtonGracePeriod) {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("isJumping", false);
            isJumping = false;
            animator.SetBool("isFalling", false);

            if(Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod){
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }

        } else {
            characterController.stepOffset = 0;
            animator.SetBool("isGrounded", false);
            isGrounded = false;

            if((isJumping && ySpeed < 0) || ySpeed < -2){
                animator.SetBool("isFalling", true);
            }
            // if(ySpeed < ySpeedBefore){
            //     animator.SetBool("isFalling", true);
            // }
        }

        ySpeedBefore = ySpeed;

        if(movementDirection != Vector3.zero){
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        } else {
            animator.SetBool("isMoving", false);

        }
    }

    private void OnApplicationFocus(bool focus) {
        if(focus){
            Cursor.lockState = CursorLockMode.Locked;
        } else{
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
