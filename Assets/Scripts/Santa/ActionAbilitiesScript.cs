using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAbilitiesScript : MonoBehaviour
{
    [Header("Santa Objects")]
    [Tooltip("Values for abilities")]
    public GameObject Santa31;
    public Transform holdPos;
    public Animator santaAnimator;
    public CapsuleCollider PunchHitboxL;
    public CapsuleCollider PunchHitboxR;

    [Header("Holding Ability")]
    [Tooltip("Holding Positions")]
    public bool canPickUpChild;
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement

    [Header("Object Held")]
    [Tooltip("Values for holding")]
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private  float objSpe;
    private Enums.ObjectWeight objClass;
    private float objDmg;
    public RelativeMovement RelMove;



    // [Header("Punching Ability")]
    // [Tooltip("Punching Positions")]

    //Reference to script which includes mouse movement of player (looking around)
    //we want to disable the player looking around when rotating the object

    //MouseLookScript mouseLookScript;
    void Start()
    {
        PunchHitboxL.enabled = false;
        //mouseLookScript = player.GetComponent<MouseLookScript>();
    }

    void Update()
    {
        //Draws the Ray in scene view
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.green);

        PickAndThrowKeyBinds();
        PunchKeyBind();
        if (santaAnimator.GetCurrentAnimatorStateInfo(1).IsName("Armature|Punch_R"))
        {
           // PunchHitboxL.enabled = true;
            PunchHitboxR.enabled = true;
        }
        else
        {
            //PunchHitboxL.enabled = false;
            PunchHitboxR.enabled = false;
        }
    }

    void PunchKeyBind()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            santaAnimator.SetTrigger("Attack");
        }

    }

    void PickAndThrowKeyBinds()
    {
        if (Input.GetKeyDown(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            if (heldObj == null) //if currently not holding anything
            {
                //perform raycast to check if player is looking at object within pickuprange
                RaycastHit hit;
                
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        //pass in object hit into the PickUpObject function
                        santaAnimator.SetTrigger("PickUp");
                        santaAnimator.SetBool("Holding", true);
                        PickUpObject(hit.transform.gameObject);
                    }
                    else if (hit.transform.gameObject.tag == "Child" && canPickUpChild)
                    {
                        //pass in object hit into the PickUpObject function
                        santaAnimator.SetTrigger("PickUp");
                        santaAnimator.SetBool("Holding", true);
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping(); //prevents object from clipping through walls
                    DropObject();
                }
            }
        }
        if (heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                StopClipping();
                ThrowObject();
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), Santa31.GetComponent<Collider>(), true);
            //get object's stats
            objSpe = heldObj.GetComponent<ObjectStats>().slowFactor;
            objDmg = heldObj.GetComponent<ObjectStats>().dmgFactor;
            objClass = heldObj.GetComponent<ObjectStats>().weightClass;
            RelMove.ChangeMoveSpeed(objSpe);
        }
    }

    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), Santa31.GetComponent<Collider>(), false);
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
        
        //AnimationReset & speed Reset
        santaAnimator.SetBool("Holding", false);
        RelMove.ResetSpeed();
    }

    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }

    void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), Santa31.GetComponent<Collider>(), false);
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;

        //AnimationReset & speed Reset
        santaAnimator.SetBool("Holding", false);
        RelMove.ResetSpeed();
    }

    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }
}
