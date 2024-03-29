using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Child Object Selection")]
    [Tooltip("Object Selector")]
    [SerializeField] private GameObject childPrefab;
    private GameObject child;

    public bool Spawning;
    public PlayerSettingsScriptableObject user_interface;
    // Start is called before the first frame update
    void Start()
    {
        // Starting in 2 seconds with method triggering every 10 seconds
        InvokeRepeating("ChildSpawn", 2.0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        Spawning = user_interface.enemySpawning;

    }

    //Spawns Child from Door
    void ChildSpawn()
    {
        if (Spawning)
        {
            child = Instantiate(childPrefab) as GameObject;
            child.transform.position = this.transform.position + new Vector3(2, -1, 0);

            float angle = Random.Range(0, 180);
            child.transform.Rotate(0, angle, 0);
        }
/*        else
        {
            Debug.Log("Can't Spawn");
        }
*/
    }
}
