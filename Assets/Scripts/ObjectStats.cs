using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    public Enums.ObjectWeight weightClass = Enums.ObjectWeight.medium;
    //Multiply with speed while held
    public float slowFactor = 1;
    //Multiply or smth for damage?
    public float dmgFactor = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        /* Prob not needed for debugging
        //Makes sure ignored objects have a value
        if (slowFactor <= 0)
            slowFactor = 1;
        if (dmgFactor < 0)
            dmgFactor = 0;
        */
    }


}
