using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 
[RequireComponent(typeof(CharacterController))] 
public class Move : MonoBehaviour
{
    [Header("Character Inputs")]
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed = 100.0f;

    [SerializeField] private CharacterController charController;

    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;

    [Header("Interaction Inputs")]
    [SerializeField] private KeyCode waveKey;

    [Header("Animator Checking")]
    public  Animator anim;
    [SerializeField] private bool isBreath;
    [SerializeField] private bool isWalk;
    [SerializeField] private bool isTurn;
    [SerializeField] private bool isRun;
    [SerializeField] private bool isJump;
    [SerializeField] private bool isWave;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterMovement();
        Characterjump();
        CharacterInteraction();
        CharacterEmote();
    }

    void CharacterMovement()
    {
        //Horizontal & Vertical --Movement
        float horizontalInput = Input.GetAxis(horizontalInputName) * rotationSpeed;
        float verticalInput   = Input.GetAxis(verticalInputName) * movementSpeed;
        //Move Forward & Move Right --Movement
        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 rightMovement = transform.right * horizontalInput;
        //CharacterController make a Simple move by having ForwardMove + RightMove
        charController.SimpleMove(forwardMovement);

        
        //Moving Forward Backward
         if(verticalInput != 0)
         {
            anim.SetBool("IsWalking", true);
            isWalk = true;
            anim.SetBool("IsIdle", false);
            isBreath = false;
            anim.SetBool("IsWaving", false);
            isWave = false;
         }
         else
         {
            anim.SetBool("IsWalking", false);
            isWalk = false;
            anim.SetBool("IsIdle", true);
            isBreath = true;
            anim.SetBool("IsWaving", false);
            isWave = false;
         }
        
        //Turning Left Right check
         if(horizontalInput != 0)
         {
            horizontalInput *= Time.deltaTime;
            transform.Rotate(0,horizontalInput,0);
            isTurn = true;
         }
         else
         {
             isTurn = false;
         }
    }

    void Characterjump()
    {
        //jump
        if (!isJump && Input.GetKeyDown(jumpKey))
        {
            isJump = true;
            anim.SetBool("IsJumping", true);
            StartCoroutine(JumpEvent());
            print(jumpKey);
        }
    }

    IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } 
        
        while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        anim.SetBool("IsJumping", false);
        isJump = false;
    }

    void CharacterInteraction()
    {
        //Interaction --WavingAction
        //if(Input.GetKeyDown(waveKey))
        //{
           // if(!isWave)
           // {
            //    isWave = true;
                //anim.SetBool("IsWaving");
             //   isBreath = false;
             //   Debug.Log("Check");
           // }
           // else
           // {
            //    isWave = false;
                //anim.SetBool("IsWaving");
          //      isBreath = false;
          //      Debug.Log("unCheck");
           // }
        //}
    }

    void CharacterEmote()
    {
        //Emotes
    }

    //TODO Toggle Run
    //TODO Change Camera View from FP to TP and back and Forward
    //https://www.youtube.com/watch?v=e9cKL0neSJs&t=8s
}
