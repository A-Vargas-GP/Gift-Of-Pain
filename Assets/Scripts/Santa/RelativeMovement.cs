using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [Header("Santa Animator Selection")]
    [Tooltip("Animator Selection")]
    public Animator santaAnimator;

    [Header("Santa Movement Values")]
    [Tooltip("Values")]
    public bool canMove;
    public float baseSpeed = 4.5f;
    private float moveSpeed;
    public float jumpSpeed = 7.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public float rotSpeed = 3.0f;
    [SerializeField] private Transform target;

    private float vertSpeed;

    private CharacterController charController;

    private Vector3 movement = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();

        vertSpeed = minFall;
        moveSpeed = baseSpeed;
        santaAnimator.SetTrigger("Land");
        santaAnimator.SetBool("Walking", true);
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            if (target == null)
                target = GameObject.FindWithTag("MainCamera").transform;
            MovingSanta();
            JumpingSanta();

            movement.y = vertSpeed; //Change up/down

            movement *= Time.deltaTime;
            charController.Move(movement);
        }

    }

    void MovingSanta()
    {
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        //Movement Mechanic
        if (horInput != 0 || vertInput != 0) //Walking in a direction
        {
            santaAnimator.SetBool("Walking", true);
            movement.x = horInput * moveSpeed; //Change left/right
            movement.z = vertInput * moveSpeed; //Change forward/back
            // movement = Vector3.ClampMagnitude(movement, moveSpeed);

            //Move around in correct direction in Third POV
            Quaternion temp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = temp;

            //Rotate Santa Object in proper direction facing movement
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        else //Standing Idle
        {
            santaAnimator.SetBool("Walking", false);
        }
    }

    void JumpingSanta()
    {
        //Jumping Mechanic
        if (charController.isGrounded) //Santa's touching the floor
        {
            if (Input.GetButtonDown("Jump")) //Move up (positive)
            {
                santaAnimator.SetTrigger("Jump");
                vertSpeed = jumpSpeed;
            }
            else //Move down (negative)
            {
                vertSpeed = minFall;
                //santaAnimator.SetTrigger("Idle");
                santaAnimator.SetTrigger("Land");
            }
        }
        else //Santa isn't touching the floor -- gravity affects
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity) //Move down to standard
            {
                vertSpeed = terminalVelocity;
            }
        }
    }

    //Multiplies the move speed with whatever value is put in
    //can be used to stack move speed changes
    public void ChangeMoveSpeed(float modifier)
    {
        moveSpeed = moveSpeed * modifier;
    }

    //Resets movespeed back to it's original value (baseSpeed)
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
    public void GameStart()
    { 
        
        canMove = true;
    }
}
