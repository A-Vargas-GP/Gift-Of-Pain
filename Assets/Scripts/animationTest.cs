using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationTest : MonoBehaviour
{
    public Animator santaAnimator;
    //private bool holding;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            santaAnimator.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
//            santaAnimator.ResetTrigger("Jump");
            santaAnimator.SetTrigger("Land");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            santaAnimator.SetBool("Walking", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            santaAnimator.SetBool("Walking", false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q");
            //holding = true;
            santaAnimator.SetTrigger("PickUp");
            santaAnimator.SetBool("Holding", true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            santaAnimator.SetBool("Holding", false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            santaAnimator.SetBool("Attack", true);
        }
        else
        {
            santaAnimator.SetBool("Attack", false);
        }

    }
}
