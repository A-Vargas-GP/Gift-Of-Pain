using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Child Object Selection")]
    [Tooltip("Object Selector")]
    [SerializeField] private GameObject childPrefab;
    private GameObject child;

    [Header("Child Spawn Selection")]
    [Tooltip("Enable Child Spawning")]
    public bool enableSpawn;

    [Header("Child Object Stats")]
    [Tooltip("Child UI Script")]
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
        enableSpawn = user_interface.enemySpawning;

    }

    //Spawns Child from Door
    void ChildSpawn()
    {
        if (enableSpawn)
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
