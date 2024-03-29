using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_Stats : MonoBehaviour
{
    [Header("Child Scriptable Object Script")]
    [Tooltip("Damage and health values of Child")]
    public HealthScriptableObject child;

    [Header("Santa Scriptable Object Script")]
    [Tooltip("Damage and health values of Santa")]
    public HealthScriptableObject santa;

    [Header("Santa Health and Damage")]
    [Tooltip("Damage and health values of Santa")]
    public static int santaHealth;
    public static int santaDamage;

    // Start is called before the first frame update
    void Start()
    {
        child.currentHealth = 3;
        child.damageValue = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Child's Health: " + child.currentHealth);

        if (child.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void takeDamage()
    {
        child.currentHealth-=santa.damageValue;
    }

    public void OnCollisionEnter(Collision collider)
    {        
        if (collider.gameObject.tag == "Santa")
        {
            // takeDamage();
            Debug.Log("SMASH SANTA");
        }
    }
}
