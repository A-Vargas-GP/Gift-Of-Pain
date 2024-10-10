using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa_Stats : MonoBehaviour
{
    [Header("Santa Scriptable Object Script")]
    [Tooltip("Damage and health values of Santa")]
    public HealthScriptableObject santa;

    [Header("Santa's original beginning position")]
    [Tooltip("Santa obj")]
    [SerializeField] private Vector3 originalPosition;

    [Header("Child Scriptable Object Script")]
    [Tooltip("Damage and health values of Child")]
    public EnemyDamageScriptableObject child;

    private CharacterController charController;

    private ActionAbilitiesScript script;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        script = this.GetComponent<ActionAbilitiesScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        santa.currentHealth = 100;
        santa.damageValue = 10;
        originalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Santa's Health: " + santa.currentHealth);

        //**Potential Idea: upon dying, maybe have Santa break apart or something along those lines??
        if (santa.currentHealth <= 0)
        {
            santa.currentHealth = 0;
            resetSantaPosition();
        }
    }

    public void takeDamage()
    {
        santa.currentHealth-=child.damageValue;
    }

    public void resetSantaPosition()
    {
        this.transform.position = originalPosition;
        santa.currentHealth = 25;
    }

    void OnCollisionEnter(Collision other)
    {        
        if ((other.gameObject.tag == "Child"))
        {
            if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Armature|Punch_R"))
            {
                // Debug.Log("Santa swung around child");
            }
            // Debug.Log("Santa is next to the child");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Child"))
        {
            if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Armature|Punch_R"))
            {
                Destroy(other.gameObject);
                // Debug.Log("Santa punches through child"); 
            }
        }
    }

    /*
    public void OnControllerColliderHit(ControllerColliderHit collider)
    {
        if (collider.gameObject.tag == "Child")
        {
            //takeDamage();
            Debug.Log("SMACKING INTO CHILD");
        }
    }
    */
}
