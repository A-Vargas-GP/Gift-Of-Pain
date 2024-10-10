using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_Stats : MonoBehaviour
{
    [Header("Santa Scriptable Object Script")]
    [Tooltip("Damage and health values of Santa")]
    public HealthScriptableObject santa;
    public GameObject santaObj;
    private Santa_Stats santaRef;

    [Header("Child Health Value")]
    [Tooltip("Health values of Child")]
    public EnemyDamageScriptableObject childObj;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject child;
    public static int destroyedChildTotal = 0;

    void Awake()
    {
        currentHealth = 3;
        santaObj = GameObject.FindWithTag("Santa");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Child's Health: " + currentHealth);
        Die();
    }

    public void takeDamage()
    {
        currentHealth-=santa.damageValue;
    }

    public void Die()
    {
        if (currentHealth <= 0)
        {
            Destroy(child);
            destroyedChildTotal++;
        }
    }

    public int returnHealth()
    {
        return currentHealth;
    }

    public void DestroySelf(GameObject self)
    {        
        child = self;
    }

    public void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "SantaHand")
        {
            // Debug.Log("Recognizing hand");
            if(santaObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Armature|Punch_R"))
            {
                // Debug.Log("Recognizing punching motion");
                takeDamage();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SantaHand")
        {
            Debug.Log("Recognizing leaving hand");
        }
    }

    // public void OnCollisionEnter(Collision collider)
    // {        
    //     if (collider.gameObject.tag == "Santa")
    //     {
    //         // takeDamage();
    //         Debug.Log("Running into Santa");
    //     }
    // }
}
