using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftMovement : MonoBehaviour
{
    [Header("Gift Values")]
    [Tooltip("Values")]
    public float speed = 4.0f;
    public int damage = 1;
    private Vector3 originalPos;
    public float launchVelocity = 700f;

    // Start is called before the first frame update
    void Start()
    {
        //Component for destroying gift based on distance
        originalPos = transform.position;
    }

    void Update() 
    {   
        //Movement
        // transform.Translate(0, 0, speed * Time.deltaTime);
        // this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, launchVelocity,0));
        DestroyFromDistance();
    }

    //Collision to Santa Player; dissolve
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Santa") 
        {
            // Debug.Log("Player hit");
        }

        Destroy(this.gameObject);
    }

    void DestroyFromDistance()
    {
        float distance = Vector3.Distance(originalPos, this.transform.position);

        if (distance >= 10.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
