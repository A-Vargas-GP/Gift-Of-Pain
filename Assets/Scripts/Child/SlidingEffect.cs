using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingEffect : MonoBehaviour
{
    private float slideSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //While the player is in collision with the trigger, move him forward   
    void OnTriggerStay (Collider other) 
    {
        // Debug.Log("Triggering");
        CharacterController cont = other.gameObject.GetComponent<CharacterController>();
        if(cont != null)
        {
            cont.Move(transform.forward * slideSpeed);
            // Debug.Log("Moving");
        }
    }
}
